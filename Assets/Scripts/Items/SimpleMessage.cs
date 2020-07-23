using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMessage : Item {
	public override void OnActivation(Character character){
		if(WorldManager.noLight && checkLight){
			AudioController.Play("Toilet");
			character.ShowMessage("I need to fix electricity");
			return;
		}
		if(WorldManager.flooded && checkFlood){
			AudioController.Play("Toilet");
			character.ShowMessage("My neighbour is an asshole");
			return;
		}
		base.OnActivation(character);
		character.ShowMessage(msg.Random<string>());
    }
}
