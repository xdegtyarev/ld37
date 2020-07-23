using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {
	public static WorldManager instance;
    [SerializeField]List<GameObject> scenes;

    [SerializeField]GameObject nightspr;
    [SerializeField]GameObject flood;
    [SerializeField]GameObject trash;
    [SerializeField]GameObject defaultSc;

    public static bool noLight;
    public static bool flooded;

    public static GameObject currentScene;
    public static GameObject defaultScene;

    void Awake() {
    	instance = this;
        defaultScene = defaultSc;
        PickRandom();
    }

    public void PickRandom() {
    	if(scenes.Count==0){
    		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            Character.Instance.ShowMessage("You don't get it. Don't leave the room!");
    	}else{
	        currentScene = scenes[0];
	        if (currentScene.name == "Podjezd2") {
	            noLight = true;
	            nightspr.SetActive(true);
                Character.Instance.ShowMessage("Why it's dark here");
	        }

	        if (currentScene.name == "Podjezd1") {
	            flooded = true;
	            flood.SetActive(true);
                Character.Instance.ShowMessage("That neighbour is always flooding me");
	        }
	        if (currentScene.name == "Street_trashcans") {
	            trash.SetActive(true);
	        }
    	}
    }

    public void MarkQuestAsComplete() {
    	noLight = false;
    	flooded = false;
    	nightspr.SetActive(false);
    	flood.SetActive(false);
    	trash.SetActive(false);
        scenes.Remove(currentScene);
    }
}
