using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetIdle : Item {
    [SerializeField] GameObject phrase;
    [SerializeField] UnityEngine.UI.Text text;
    [SerializeField] Animator animator;
    [SerializeField] int dir;
    float phraseTimer;

    void OnEnable() {
        animator.SetInteger("dir", dir);
        animator.SetTrigger("reset");
    }

    public override void OnActivation(Character character) {
        base.OnActivation(character);
        ShowMessage(msg.Random<string>());
    }

    public override void OnEnd() {

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
