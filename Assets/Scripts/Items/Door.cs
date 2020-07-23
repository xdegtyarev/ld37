using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Item {
    [SerializeField] GameObject fadeScene;
    public override void OnActivation(Character character) {
        base.OnActivation(character);
        character.ShowMessage(msg.Random<string>());
        fadeScene.SetActive(true);
    }

    public override void OnEnd() {
        base.OnEnd();
        ch.transform.parent = WorldManager.currentScene.transform;
        ch.transform.position = WorldManager.currentScene.GetComponent<GameScene>().spawnPoint.transform.position;
        WorldManager.currentScene.SetActive(true);
        WorldManager.defaultScene.SetActive(false);
        if (highlightedItem == this) {
            highlightedItem = null;
        }
    }
}
