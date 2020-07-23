using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObject : Item {
	[SerializeField] GameObject obj;
	[SerializeField] bool selfDisable;
	public override void OnActivation(Character character){
		if(WorldManager.noLight && checkLight){
			AudioController.Play("Toilet");
			character.ShowMessage("I need to fix electricity");
			return;
		}
		base.OnActivation(character);
		obj.SetActive(!obj.activeSelf);
    }

	public override void OnEnd() {
		if(selfDisable){
			obj.SetActive(false);
		}
  		ch.ShowMessage(msg.Random<string>());
    }
}
