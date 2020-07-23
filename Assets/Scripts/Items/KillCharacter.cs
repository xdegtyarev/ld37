using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCharacter : Item {
	public override void OnActivation(Character character){
		base.OnActivation(character);
		character.Die();
    }

	public override void OnEnd() {
		base.OnEnd();
    }
}
