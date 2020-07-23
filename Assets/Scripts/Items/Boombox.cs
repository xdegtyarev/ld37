using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boombox : Item {
	bool isPlaying;

	void Awake(){
		isPlaying = true;
		AudioController.PlayMusicPlaylist("Default");
	}

	public override void OnActivation(Character character){
		if(WorldManager.noLight && checkLight){
			character.ShowMessage("I need to fix electricity");
			return;
		}
		base.OnActivation(character);
		if(isPlaying){
			character.ShowMessage("I'm tired of this noise");
			isPlaying = false;
			AudioController.StopMusic();
		}else{
			character.ShowMessage(msg.Random<string>());
			isPlaying = true;
			AudioController.PlayMusicPlaylist("Default");
		}
    }
}
