using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Item : MonoBehaviour {
	public static Item highlightedItem;
    [SerializeField] string actionSound;
    [SerializeField] string actionEndSound;
    [SerializeField] public string[] msg;
    [SerializeField] public Animator controller;
	[SerializeField] SpriteRenderer view;
    [SerializeField] protected float interactionTime;
    [SerializeField] protected bool checkFlood;
    [SerializeField] protected bool checkLight;
    protected Character ch;
    float interactionProgress;
    bool isCharacterNearby;

    public virtual void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
        	isCharacterNearby = true;
        	highlightedItem = this;
        	Dehighlight();
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
        	isCharacterNearby = false;
            if(highlightedItem == this){
        	   highlightedItem = null;
            }
        	Highlight();
        }
    }

    public virtual void OnActivation(Character character){
        if(!string.IsNullOrEmpty(actionSound)){
            AudioController.Play(actionSound);
        }
        ch = character;
        interactionProgress = interactionTime;
        if(controller!=null){
            controller.SetBool("isOn", true);
        }
    }


    public virtual void Update(){
         if(interactionProgress>0f){
            interactionProgress-=Time.deltaTime;
            if(interactionProgress<=0f){
                OnEnd();
            }
        }
    }

    public virtual void OnEnd(){
        if(!string.IsNullOrEmpty(actionEndSound)){
            AudioController.Play(actionEndSound);
        }
        if(controller!=null){
            controller.SetBool("isOn", false);
        }
    }

    void Highlight(float time = 0.4f, float phase = 0.5f) {
        Sequence mySequence = DOTween.Sequence().SetAutoKill();
        mySequence.Append(DOTween.To(() => view.color, x => view.color = x, new Color(1f, 1f, 1f, 1f), time*phase));
    }

    void Dehighlight(float time = 0.4f, float phase = 0.5f){
    	Sequence mySequence = DOTween.Sequence().SetAutoKill();
    	mySequence.Append(DOTween.To(() => view.color, x => view.color = x, new Color(0.8f, 0.8f, 0.8f, 1f), time*phase));
    }
}
