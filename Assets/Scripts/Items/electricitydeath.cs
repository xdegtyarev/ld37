using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class electricitydeath : Item {
	public override void OnActivation(Character character){
		base.OnActivation(character);
		character.isSitting = true;
		character.controller.SetTrigger("die-electric");
    }

	public override void OnEnd() {
		base.OnEnd();
		ch.ShowMessage("Am I Dead?");
		ch.StartRestartCoroutine();
    }
}
