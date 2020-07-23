using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI : MonoBehaviour {
    [SerializeField] GameObject view;
    [SerializeField] Animator controller;
    [SerializeField] float speed;

    [SerializeField] GameObject phrase;
    [SerializeField] Text text;
    float phraseTimer;
    public bool isSitting;

    public void OnEnable(){
        StartCoroutine(AttackOnAwakeCoroutine());
    }

    IEnumerator AttackOnAwakeCoroutine(){

        Character.Instance.isSitting = true;
        Character.Instance.OnStopMovement();
        controller.SetInteger("dir", 2);
        controller.SetTrigger("reset");
        ShowMessage("SUKA BLYAD! ZAEBAL");
        yield return new WaitForSeconds(2f);
        controller.SetTrigger("attack");
        AudioController.Play("Knife");
        yield return new WaitForSeconds(0.5f);
        Character.Instance.Die();
        yield return null;
    }

    public void Attack(){
        controller.SetTrigger("attack");
    }

    void Update() {
        if(phraseTimer>0f){
        	phraseTimer-=Time.deltaTime;
        	if(phraseTimer<=0f){
        		phrase.gameObject.SetActive(false);
        	}
        }
    }

    public void ShowMessage(string msg){
    	text.text = msg;
    	phrase.gameObject.SetActive(true);
    	phraseTimer = 2f;
    }
}
