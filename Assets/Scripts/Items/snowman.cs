using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowman : MonoBehaviour {
	[SerializeField] List<string> msg;
	[SerializeField] GameObject phrase;
	[SerializeField] UnityEngine.UI.Text text;
    [SerializeField] GameObject sosulka;
    [SerializeField] Animator animator;
    bool once;
    float phraseTimer;


    public virtual void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            if (!once) {
                once = true;
                animator.enabled = true;
                Character.Instance.isSitting = true;
            }
        }
    }

    public void OnFall() {
        sosulka.SetActive(false);
        animator.enabled = false;
        Character.Instance.Die();
        ShowMessage(msg.Random());
    }

    public void Update() {
        if (phraseTimer > 0f) {
            phraseTimer -= Time.deltaTime;
            if (phraseTimer <= 0f) {
                phrase.gameObject.SetActive(false);
            }
        }
    }

    void ShowMessage(string msg) {
        text.text = msg;
        phrase.gameObject.SetActive(true);
        phraseTimer = 2f;
    }

}
