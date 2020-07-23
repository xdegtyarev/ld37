public class ProjectsPaths {

	// Path to specific settings
	public const string CUT_SETTINGS = "Assets/Sprites/Characters/CutSettings.json";

	// Paths to specific asset folders
	public const string SPRITES = "Assets/Sprites";
	public const string ANIMATIONS_FOLDER = "Assets/Animations/";
	public const string CHARACTER_SPRITES = "Assets/Sprites/Characters";
	public const string CHARACTER_ANIMATIONS = "Assets/Animations/Characters/Animations";
	public const string CHARACTER_OVERRIDES = "Assets/Animations/Characters/Overrides";
	public const string ROOM_SPRITES = "Assets/Sprites/Rooms";
	public const string BACKGROUND_SPRITES = "Assets/Sprites/Backgrounds";
	public const string CITY_SPRITES = "Assets/Sprites/City";
	public const string TRUCK_SPRITES = "Assets/Sprites/Trucks";
	public const string UI_SPRITES = "Assets/Sprites/UI";
	public const string AVATAR_SPRITES = "Assets/Sprites/Avatars";
	public const string PRODUCT_SPRITES = "Assets/Sprites/Products";
	public const string APP_ICON_SPRITES = "Assets/Sprites/AppIcon";
	public const string CURSOR_SPRITES = "Assets/Sprites/Cursor";
	public const string VFX_SPRITES = "Assets/Sprites/FX";
	public const string OTHER_SPRITES = "Assets/Sprites/Other";

	public static string STREAMING_ASSETS {
		get {
			#if UNITY_IOS
			return Application.dataPath + "/Raw";
			#elif UNITY_ANDROID
			return "jar:file://" + Application.dataPath + "!/assets/";
			#else
			return UnityEngine.Application.dataPath + "/StreamingAssets";
			#endif
		}
	}

}
