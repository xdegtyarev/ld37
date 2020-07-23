using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fade : MonoBehaviour {
	void OnEnable(){

	}

	public void ToggleOff(){
		gameObject.SetActive(false);
	}
}
