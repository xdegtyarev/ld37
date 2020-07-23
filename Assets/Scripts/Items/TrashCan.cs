using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : Item {
	[SerializeField] AI aiToToggle;
	public override void OnActivation(Character character){
		base.OnActivation(character);
		character.ShowMessage("Good doggo");
		character.isSitting = true;
    }

	public override void OnEnd() {
		base.OnEnd();
		aiToToggle.enabled = true;
    }
}
