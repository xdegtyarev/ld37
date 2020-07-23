using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : Item {
    [SerializeField] List<string> love;
    [SerializeField] List<string> shop;
    [SerializeField] List<string> trash;
    [SerializeField] List<string> winter;
    [SerializeField] List<string> pad;
    [SerializeField] GameObject windowUI;
    bool isActive = false;
    public override void OnActivation(Character character) {
        base.OnActivation(character);
        if (!isActive) {
            isActive = true;
            WorldManager.defaultScene.transform.position = -Vector3.right * 1000f;
            WorldManager.currentScene.SetActive(true);
            windowUI.SetActive(true);
        } else {
            OnEnd();
        }
    }

    public override void OnEnd() {
        if (isActive) {
        	base.OnEnd();
            isActive = false;
            WorldManager.currentScene.SetActive(false);
            WorldManager.defaultScene.transform.position = Vector3.zero;
            windowUI.SetActive(false);
            if (WorldManager.currentScene.name == "Street_road") {
                ch.ShowMessage(love.Random());
            }

            if (WorldManager.currentScene.name == "Street_shop") {
                ch.ShowMessage(shop.Random());
            }

            if (WorldManager.currentScene.name == "Street_trashcans") {
                ch.ShowMessage(trash.Random());
            }

            if (WorldManager.currentScene.name == "Winter") {
                ch.ShowMessage(winter.Random());
            }

            if (WorldManager.currentScene.name.Contains("Podjezd")) {
                ch.ShowMessage(pad.Random());
            }
        }
    }
}
