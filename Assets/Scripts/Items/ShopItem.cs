using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : Item {
	[SerializeField] AI aiToToggle;
	public override void OnActivation(Character character){
		base.OnActivation(character);
		aiToToggle.enabled = true;
	}
}
