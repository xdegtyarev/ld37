using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCharacter : Item {
	public override void OnActivation(Character character){
		base.OnActivation(character);
		character.gameObject.SetActive(false);
		character.ShowMessage(msg.Random<string>());
    }

	public override void OnEnd() {
        base.OnEnd();
        ch.gameObject.SetActive(true);
    }
}
