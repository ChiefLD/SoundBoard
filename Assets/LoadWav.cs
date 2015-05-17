using UnityEngine;
using System.Collections;

public class LoadWav : MonoBehaviour {
	NewGM_Script GM;

	public int soundNumber;
	public AudioSource source;
	void Start() {
		GM = GameObject.FindObjectOfType (typeof (NewGM_Script)) as NewGM_Script;
		WWW www = new WWW("File://" +Application.persistentDataPath +"/MyAudFile" +soundNumber +".wav");
		source = GetComponent<AudioSource>();
		source.clip = www.GetAudioClip (false);
	}
	public void Play() {
		if (GM.recording == false){
			if (!source.isPlaying && source.clip.isReadyToPlay)
				source.Play();
		}
	}
}