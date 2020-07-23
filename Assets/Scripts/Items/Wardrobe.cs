using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrobe : Item {
	[SerializeField] AnimatorOverrideController girl;
	public override void OnActivation(Character character){
		base.OnActivation(character);
		var temp = character.GetComponent<Animator>().runtimeAnimatorController as AnimatorOverrideController;
		character.GetComponent<Animator>().runtimeAnimatorController = girl;
		girl = temp;
		if(temp.name == "hikka"){
			character.ShowMessage(msg.Random<string>());
		}
    }
}
