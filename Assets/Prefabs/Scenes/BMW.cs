using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BMW : MonoBehaviour {
	[SerializeField] Animator animator;
    public void PlayAnimation() {
    	animator.enabled = true;
    }

   public virtual void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
        	PlayAnimation();
        	// Character.Instance.isSitting = true;
        }
    }


    public void PlaySound(string soundId) {

    }

    public void OnHit(){
        AudioController.Play("Naezd");
        Character.Instance.transform.position = Vector3.down * 100f;
        Character.Instance.StartRestartCoroutine();
    }
}
