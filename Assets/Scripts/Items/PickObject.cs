using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObject : Item {
	public override void OnActivation(Character character){
		base.OnActivation(character);
		character.ShowMessage(msg.Random<string>());
		gameObject.SetActive(false);
    }
}
