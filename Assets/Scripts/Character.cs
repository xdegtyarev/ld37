using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {
    public static Character Instance;
    [SerializeField] GameObject fade;
    [SerializeField] GameObject view;
    [SerializeField] public Animator controller;
    [SerializeField] float speed;

    [SerializeField] GameObject phrase;
    [SerializeField] Text text;
    float phraseTimer;
    public bool isSitting;

    Vector3 nextPos;

    void Awake(){
        Instance = this;
    }


    public void OnUp() {
    	nextPos = transform.position + Vector3.up * Time.timeScale * speed;
    	if(IsOnFloor(nextPos)){
            controller.SetInteger("dir", 0);
            controller.SetBool("isMoving", true);
            transform.position = nextPos;
        }
    }

    public void OnRight() {
    	nextPos = transform.position + Vector3.right * Time.timeScale * speed;
    	if(IsOnFloor(nextPos)){
            controller.SetInteger("dir", 1);
            controller.SetBool("isMoving", true);
            transform.position = nextPos;
        }
    }

    public void OnDown() {
    	nextPos = transform.position + Vector3.down * Time.timeScale * speed;
    	if(IsOnFloor(nextPos)){
            controller.SetInteger("dir", 2);
            controller.SetBool("isMoving", true);
            transform.position = nextPos;
        }
    }

    public void OnLeft() {
    	nextPos = transform.position + Vector3.left * Time.timeScale * speed;
    	if(IsOnFloor(nextPos)){
            controller.SetInteger("dir", 3);
            controller.SetBool("isMoving", true);
            transform.position = nextPos;
        }
    }

    public void OnStopMovement() {
        controller.SetBool("isMoving", false);
    }

    public void OnUse() {
        OnStopMovement();
        controller.Update(Time.deltaTime);
    	if(!isSitting && Item.highlightedItem!=null){
            controller.SetInteger("dir", 0);
            controller.SetTrigger("reset");
            controller.Update(Time.deltaTime);
    		Item.highlightedItem.OnActivation(this);
    	}
    }

    public void Die(){
        OnStopMovement();
        AudioController.Play("Naezd");
        controller.SetTrigger("die");
        StartRestartCoroutine();
    }

    public void StartRestartCoroutine(){
        StartCoroutine(RestartCoroutine());
    }

    IEnumerator RestartCoroutine(){
        WorldManager.instance.MarkQuestAsComplete();
        yield return new WaitForSeconds(3f);
        fade.SetActive(true);
        yield return new WaitForSeconds(1f);
        isSitting = false;
        transform.parent = WorldManager.defaultScene.transform;
        transform.localPosition = Vector3.zero;
        controller.SetInteger("dir", 2);
        controller.SetTrigger("reset");
        Item.highlightedItem = null;
        WorldManager.currentScene.SetActive(false);
        WorldManager.defaultScene.SetActive(true);
        WorldManager.instance.PickRandom();
        yield return new WaitForSeconds(1f);
        ShowMessage("Such a bad day");
    }

    bool IsOnFloor(Vector3 pos){
    	return Physics2D.OverlapPointAll(pos, LayerMask.GetMask("Floor")).Length>0;
    }

    bool inputBlocked;
    float inputBlockedPhase;

    void Update() {
        if(phraseTimer>0f){
        	phraseTimer-=Time.deltaTime;
        	if(phraseTimer<=0f){
        		phrase.gameObject.SetActive(false);
        	}
        }

        if(inputBlockedPhase>0f){
            inputBlockedPhase-=Time.deltaTime;
            if(inputBlockedPhase<=0f){
                inputBlocked = false;
            }
        }

        if(!isSitting && !inputBlocked){
            if (Input.GetKeyDown(KeyCode.Space)) {
                inputBlocked = true;
                inputBlockedPhase = 0.5f;
                OnUse();
                return;
            }

            if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow)) {
                OnStopMovement();
            }
            if (Input.GetKey(KeyCode.UpArrow)) {
                OnUp();
            }
            if (Input.GetKey(KeyCode.RightArrow)) {
                OnRight();
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                OnDown();
            }
            if (Input.GetKey(KeyCode.LeftArrow)) {
                OnLeft();
            }

        }

    }

    public void ShowMessage(string msg){
    	text.text = msg;
    	phrase.gameObject.SetActive(true);
    	phraseTimer = 3f;
    }
}
