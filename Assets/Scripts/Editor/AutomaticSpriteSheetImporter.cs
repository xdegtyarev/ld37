using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public class Settings{
    public string key;
}


public class SpriteSheetImportSettings:Settings {
    public string animationSubfolder;

    public int defaultRectWidth = 0;
    public int defaultRectHeight = 0;
    public int alignment = 0;

    public SpriteSheetRow[] single = new SpriteSheetRow[0];
    public SpriteSheetRow[] repeating = new SpriteSheetRow[0];
}

public class SpriteSheetRow:Settings{
    public bool isAnimation = false;
    public bool isLooped = false;
    public bool isOverride = false;
    public int rectWidth = 0;
    public int rectHeight = 0;
}

public class AutomaticSpriteSheetImporter : AssetPostprocessor {
    const string settingsName = "_ImportSettings.json";
    private SpriteSheetImportSettings cutSettings;
    JsonSerializerSettings jsonSettings;
    public AutomaticSpriteSheetImporter() {
    	jsonSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
    }

    private void OnPostprocessTexture(Texture2D texture) {
        if (texture != null) {
            if (HasCutSettings()) {
            	var textureImporter = (assetImporter as TextureImporter);
            	var file = new FileInfo(assetPath);
                var newSpritesheet = new List<SpriteMetaData>();
                var spriteSheetName = file.Name.Replace(file.Extension,"");

                textureImporter.spriteImportMode = SpriteImportMode.Multiple;

                int currentX = 0;
                int currentY = 0;
                int row = 0;
                int column = 0;
                SpriteSheetRow currentRow;

                while(currentY < texture.height && (cutSettings.repeating.Length > 0 || row < cutSettings.single.Length)){
                    currentRow = (row >= cutSettings.single.Length) ? cutSettings.repeating[(row - cutSettings.single.Length) % cutSettings.repeating.Length] : cutSettings.single[row];
                    column = 0;
                    currentX = 0;
                    while(currentX < texture.width){
                        var spriteRect = new Rect(currentX,
                                              texture.height - currentY - (currentRow.isOverride ? currentRow.rectHeight : cutSettings.defaultRectHeight),
                                              currentRow.isOverride ? currentRow.rectWidth : cutSettings.defaultRectWidth,
                                              currentRow.isOverride ? currentRow.rectHeight : cutSettings.defaultRectHeight);
                        if (CheckSquare(spriteRect, texture)) {
                            var data = new SpriteMetaData();
                            data.rect = spriteRect;
                            data.alignment = cutSettings.alignment;
                            data.name = spriteSheetName + "_" + (row >= cutSettings.single.Length ? (((row - cutSettings.single.Length)/cutSettings.repeating.Length) + "_") : "" )  + currentRow.key + "_" + column;
                            newSpritesheet.Add(data);
                        }
                        currentX += currentRow.isOverride ? currentRow.rectWidth : cutSettings.defaultRectWidth;
                        column++;
                    }
                    currentY += currentRow.isOverride ? currentRow.rectHeight : cutSettings.defaultRectHeight;
                    row++;
                }
                textureImporter.spritesheet = newSpritesheet.ToArray();
                Debug.Log("SpriteSheet imported:" + spriteSheetName);
                //try import sprites
            }
        }
    }

    bool HasCutSettings() {
    	var fileInfo = new FileInfo(assetPath);
        var directoryInfo = fileInfo.Directory;
        do{
	        if (directoryInfo.GetFiles(settingsName, SearchOption.TopDirectoryOnly).Length > 0) {
				cutSettings = JsonConvert.DeserializeObject<SpriteSheetImportSettings>(File.ReadAllText(directoryInfo.GetFiles(settingsName, SearchOption.TopDirectoryOnly)[0].FullName), jsonSettings);
	            return true;
	        }
	        directoryInfo = directoryInfo.Parent;
    	}while(directoryInfo!=null);
        return false;
    }
    // Need for checking empty rows in sprite sheet
    private bool CheckSquare(Rect spriteRect, Texture2D texture) {
        Color32[] pixels = texture.GetPixels32();
        Vector2 pixelPosition;
        for (int i = 0; i < pixels.Length; i++) {
            pixelPosition.x = i % texture.width;
            pixelPosition.y = i / texture.width;
            if (spriteRect.Contains(pixelPosition) && pixels[i].a > 0.0001f) {
                return true;
            }
        }
        return false;
    }

}
