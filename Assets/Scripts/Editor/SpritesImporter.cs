using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;


public class SpritesImporter : AssetPostprocessor {

    private readonly TextureImporterSettings importSettings = new TextureImporterSettings();

    [UsedImplicitly]
    private void OnPreprocessTexture() {
        if ( !assetPath.Contains( ProjectsPaths.SPRITES ) ||
             assetPath.Contains( ProjectsPaths.OTHER_SPRITES ) ||
             assetPath.Contains( ProjectsPaths.VFX_SPRITES ) ) {
            return;
        }

        TextureImporter textureImporter = (TextureImporter) assetImporter;
        textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
        textureImporter.ReadTextureSettings( importSettings );

        importSettings.alphaIsTransparency = true;
        importSettings.aniso = 0;
        importSettings.filterMode = FilterMode.Point;
        importSettings.mipmapEnabled = false;
        importSettings.spriteExtrude = 0;
        importSettings.spriteMeshType = SpriteMeshType.Tight;
        importSettings.spritePixelsPerUnit = 100;
        importSettings.wrapMode = TextureWrapMode.Clamp;
        importSettings.textureType = TextureImporterType.Sprite;

        if ( assetPath.StartsWith( ProjectsPaths.APP_ICON_SPRITES ) ) {
            textureImporter.spritePackingTag = string.Empty;
            importSettings.filterMode = FilterMode.Bilinear;
        } else if ( assetPath.StartsWith( ProjectsPaths.AVATAR_SPRITES ) ) {
            textureImporter.spritePackingTag = "UI";
        } else if ( assetPath.StartsWith( ProjectsPaths.BACKGROUND_SPRITES ) ) {
            textureImporter.spritePackingTag = "Background";
        } else if ( assetPath.StartsWith( ProjectsPaths.CHARACTER_SPRITES ) ) {
            textureImporter.spritePackingTag = "Characters";
            importSettings.spriteMode = (int) SpriteImportMode.Multiple;
            importSettings.spriteAlignment = (int) SpriteAlignment.BottomCenter;
        } else if ( assetPath.StartsWith( ProjectsPaths.CITY_SPRITES ) ) {
            textureImporter.SetTextureSettings( importSettings );
        } else if ( assetPath.StartsWith( ProjectsPaths.CURSOR_SPRITES ) ) {
            importSettings.textureType = TextureImporterType.Cursor;
        } else if ( assetPath.StartsWith( ProjectsPaths.PRODUCT_SPRITES ) ) {
            importSettings.spriteAlignment = (int) SpriteAlignment.BottomLeft;
        } else if ( assetPath.StartsWith( ProjectsPaths.ROOM_SPRITES ) ) {
            textureImporter.spritePackingTag = "Rooms";
        } else if ( assetPath.StartsWith( ProjectsPaths.TRUCK_SPRITES ) ) {
            textureImporter.spritePackingTag = "Trucks";
            importSettings.spriteAlignment = (int) SpriteAlignment.BottomCenter;
        } else if ( assetPath.StartsWith( ProjectsPaths.UI_SPRITES ) ) {
            textureImporter.spritePackingTag = "UI";
        }
        textureImporter.SetTextureSettings( importSettings );
    }

}
