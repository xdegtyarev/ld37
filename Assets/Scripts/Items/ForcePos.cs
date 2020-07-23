using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePos : Item {
	[SerializeField] Vector3 prevPos;
	[SerializeField] Transform sitPos;
	[SerializeField] int dir;

	public override void OnActivation(Character character){
		if(WorldManager.noLight && checkLight){
			character.ShowMessage("I need to fix electricity");
			AudioController.Play("Toilet");
			return;
		}else{
			character.ShowMessage(msg.Random<string>());
		}
		base.OnActivation(character);
		character.isSitting = true;
		prevPos = character.transform.position;
		character.transform.position = sitPos.position;
		character.OnStopMovement();
		character.GetComponent<Animator>().SetInteger("dir", dir);
		character.GetComponent<Animator>().SetTrigger("sit");
    }

    public override void OnEnd(){
    	base.OnEnd();
    	ch.transform.position = prevPos;
    	ch.isSitting = false;
    	ch.GetComponent<Animator>().SetInteger("dir", 2);
    	ch.GetComponent<Animator>().SetTrigger("reset");
    }
}
