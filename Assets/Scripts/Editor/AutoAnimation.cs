using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public class AutoAnimation : MonoBehaviour {

    [MenuItem("Halfbus/Animations/Fill Overrides")]
    private static void FillAutoOverride() {
        foreach (var sel in Selection.objects) {
            if(sel is RuntimeAnimatorController){
                FillOverrideAtPath(AssetDatabase.GetAssetPath(sel));
            }
        }
    }

    static void FillOverrideAtPath(string assetPath) {
        var path = assetPath.Remove(assetPath.LastIndexOf("/"));
        var pathToAnimations = path + "/Animations";
        var pathToOverrides = path + "/Overrides";

        foreach (var animationSubfolders in AssetDatabase.GetSubFolders( pathToAnimations )) {
            // Debug.Log(animationSubfolders);
            var assetName = animationSubfolders.Substring(animationSubfolders.LastIndexOf("/") + 1);
            // Debug.Log(assetName);
            var controller = new AnimatorOverrideController();
            controller.runtimeAnimatorController = AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>(assetPath);
            string commonSubstring = "default_";
            var list = new List<AnimationClipPair>();
            foreach (var clip in controller.clips) {
                var searchClipName = clip.originalClip.name.Replace(commonSubstring, assetName + "_");
                // Debug.Log("Search clipname is:" + searchClipName);
                var newclip = AssetDatabase.LoadAssetAtPath<AnimationClip>(animationSubfolders + "/" + searchClipName + ".anim");
                var clippair = new AnimationClipPair();
                clippair.originalClip = clip.originalClip;
                clippair.overrideClip = newclip;
                list.Add(clippair);
            }
            var prevContr =
                AssetDatabase.LoadAssetAtPath<AnimatorOverrideController>(pathToOverrides + "/" + assetName +
                    ".overrideController");
            if (prevContr != null) {
                prevContr.clips = list.ToArray();
            } else {
                controller.clips = list.ToArray();
                AssetDatabase.CreateAsset(controller, pathToOverrides + "/" + assetName + ".overrideController");
            }
        }
        Debug.Log("Overrides creation done");
    }


    [MenuItem("Halfbus/Animations/FromFolder")]
    private static void CreateAutoAnimations() {
        Debug.Log("NotYetImplemented");
        foreach (var sel in Selection.objects) {
            var assetPath = AssetDatabase.GetAssetPath(sel);
            var headName = assetPath.Substring(assetPath.LastIndexOf("/") + 1);
            var assets = AssetDatabase.FindAssets("t:Sprite", new[] { assetPath });
            var spriteList = new List<Sprite>();
            foreach (var o in assets) {
                spriteList.Add(AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GUIDToAssetPath(o)));
            }
            var animationNames = new List<string>();
            var animationNameRegEx = new Regex(@"([\d_]+)$");
            foreach (var o in spriteList) {
                var regExpedString = animationNameRegEx.Replace(o.name, "");
                Debug.Log(regExpedString);
                if (!animationNames.Contains(regExpedString)) {
                    animationNames.Add(regExpedString);
                }
            }

            AssetDatabase.CreateFolder(assetPath, headName);

            var nameCheckRegEx = new Regex(@"[a-zA-Z]");
            foreach (var o in animationNames) {
                Debug.Log("Creating animation: " + o);
                var animationSprites = new List<Sprite>();
                foreach (var s in spriteList) {
                    if (!nameCheckRegEx.IsMatch(s.name.Replace(o, ""))) {
                        animationSprites.Add(s);
                    }
                }
                var clip = CreateSpriteAnimationClip(o, animationSprites, 5, false);
                AssetDatabase.CreateAsset(clip, assetPath + "/" + headName + "/" + o + ".anim");
            }
        }
    }
    //headname_0_sub-animation_0
    [MenuItem("Halfbus/Animations/FromSpritesheet")]
    private static void CreateCharacterAutoAnimations() {
        foreach (var sel in Selection.objects) {
            var assetPath = AssetDatabase.GetAssetPath(sel);
            var savePath = assetPath.Remove(assetPath.LastIndexOf("."));
            var head = savePath.Substring(savePath.LastIndexOf("/") + 1);
            savePath = savePath.Remove(savePath.LastIndexOf("/"));

            // Debug.Log("head " + head);
            // Debug.Log("assetPath " + assetPath);
            Debug.Log("savePath " + savePath);

            SpriteSheetImportSettings importSettings = GetCutSettings(assetPath);
            var importRowsData = new Dictionary<string, SpriteSheetRow>();

            foreach (var o in importSettings.single) {
                importRowsData.Add(o.key, o);
            }

            foreach (var o in importSettings.repeating) {

                importRowsData.Add(o.key, o);
            }

            var spriteList = new List<Sprite>();
            foreach (var o in AssetDatabase.LoadAllAssetsAtPath( assetPath )) {
                if (o is Sprite) {
                    if (!string.IsNullOrEmpty(o.name)) {
                        spriteList.Add(o as Sprite);
                    }
                }
            }

            var animationNames = new List<string>();
            var animationNameRegEx = new Regex(@"([\d_]+)$");
            Debug.Log("___AnimationNames___");
            foreach (var o in spriteList) {
                var regExpedString = animationNameRegEx.Replace(o.name, "");
                if (!animationNames.Contains(regExpedString)) {
                    Debug.Log(regExpedString);
                    animationNames.Add(regExpedString);
                }
            }
            Debug.Log("_____________________");

            var animationDirectory = ProjectsPaths.ANIMATIONS_FOLDER + importSettings.animationSubfolder + "/Animations";
            var controllerDirectory = ProjectsPaths.ANIMATIONS_FOLDER + importSettings.animationSubfolder + "/Controller.controller";
            Debug.Log("animationDirectory is " + animationDirectory);

            var nameCheckRegEx = new Regex(@"[a-zA-Z]");

            foreach (var o in animationNames) {
                SpriteSheetRow rowData = null;
                rowData = importRowsData[o.Substring(o.LastIndexOf("_") + 1)];

                if (rowData != null && rowData.isAnimation) {
                    var animationSprites = new List<Sprite>();
                    foreach (var s in spriteList) {
                        if (!nameCheckRegEx.IsMatch(s.name.Replace(o, ""))) {
                            animationSprites.Add(s);
                        }
                    }

                    var foldlerName = o.Remove(o.LastIndexOf("_"));

                    if (!Directory.Exists(animationDirectory + "/" + foldlerName)) {
                        AssetDatabase.CreateFolder(animationDirectory, foldlerName);
                    }

                    var clip = CreateSpriteAnimationClip(o, animationSprites, 5, rowData.isLooped);
                    AssetDatabase.CreateAsset(clip, animationDirectory + "/" + foldlerName + "/" + o + ".anim");
                }
            }
            Debug.Log("Done");
            FillOverrideAtPath(controllerDirectory);
        }
    }

    static SpriteSheetImportSettings GetCutSettings(string assetPath) {
        var jsonSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        var fileInfo = new FileInfo(assetPath);
        var directoryInfo = fileInfo.Directory;
        do {
            if (directoryInfo.GetFiles("_ImportSettings.json", SearchOption.TopDirectoryOnly).Length > 0) {
                return JsonConvert.DeserializeObject<SpriteSheetImportSettings>(File.ReadAllText(directoryInfo.GetFiles("_ImportSettings.json", SearchOption.TopDirectoryOnly)[0].FullName), jsonSettings);
            }
            directoryInfo = directoryInfo.Parent;
        } while(directoryInfo != null);
        return null;
    }


    private static AnimationClip CreateSpriteAnimationClip(string name, List<Sprite> sprites, int fps, bool loop, bool raiseEvent = false) {
        int framecount = sprites.Count;
        float frameLength = 1f / 5f;
        var clip = new AnimationClip();
        clip.frameRate = fps;
        var sets = AnimationUtility.GetAnimationClipSettings(clip);
        sets.loopTime = loop;
        AnimationUtility.SetAnimationClipSettings(clip, sets);
        var curveBinding = new EditorCurveBinding();
        curveBinding.type = typeof(SpriteRenderer);
        curveBinding.propertyName = "m_Sprite";
        var keyFrames = new ObjectReferenceKeyframe[framecount];

        for (int i = 0; i < framecount; i++) {
            var kf = new ObjectReferenceKeyframe();
            kf.time = i * frameLength;
            kf.value = sprites[i];
            keyFrames[i] = kf;
        }

        clip.name = name;

        AnimationUtility.SetObjectReferenceCurve(clip, curveBinding, keyFrames);
        return clip;
    }

}
