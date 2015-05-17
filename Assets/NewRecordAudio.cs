using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NewRecordAudio : MonoBehaviour {
	NewGM_Script GM;
//	public ParticleController PC;
	AudioSource source;
	AudioClip aud_1;	//this buttons sound
	Button thisButton; //the button this script is attached to

	public Toggle loopToggle; //the loop toggle
	public int soundNumber;	//this buttons number, set in the inspector
	public Text buttonText;	//the buttons name
	public Image infinityLoop; // displayed on the button if isLooping
	
	bool isLooping;	//if the player has set the button to loop;
	bool recording;	//is the player recording a new sound
	
	float actualTimeLength; // the time of recording not maxlength

	// Use this for initialization
	void Awake () {
		GM = GameObject.FindObjectOfType (typeof(NewGM_Script)) as NewGM_Script; //find the GM object
		source = GetComponent<AudioSource>();	//get the AudioSource this script is on
		thisButton = GetComponent<Button> ();	//get the Button this script is on

		WWW www = new WWW ("File://" +Application.persistentDataPath +"/SoundBoardFile" +soundNumber +".wav");//directory to the sound file
		source.clip = www.GetAudioClip (false);	//set the clip from the path
	}
	void Update () {
		if (recording){
			actualTimeLength += Time.deltaTime;//find the actual length the player is recording for
			thisButton.image.color = Color.red;
		}
	}
	public void StartRec_Play (){	//when the player presses down on the button
		GM.lastButtonPressed = soundNumber;
		GM.CheckLoopToggle (isLooping);

		if (GM.recording){//should record a sound
			actualTimeLength = 0;	//reset the actualtime
			recording = true;
			source.clip = Microphone.Start (null, false, GM.maxRecTime, 44100);//begin recording
		}
		else if (!GM.recording){//should play a sound 
			if (source.clip.isReadyToPlay){//sound must be loaded
				//check if it set to loop, handle acordingly
				source.Play ();
				StartCoroutine (Looper (actualTimeLength));//start loopTimer
			}
		}
	}
	public void StopRec_Play (){
		recording = false;
		thisButton.image.color = Color.white;

		if (GM.recording){
			Microphone.End (null);
			aud_1 = source.clip;
			SavWav.Save ("SoundBoardFile"+soundNumber, aud_1);
		}
	}
	public void SetLoop (bool setLoop){//set in GM by toggle
		if (setLoop){
			isLooping = true;
			infinityLoop.enabled = true;
		}
		else if (!setLoop){
			isLooping = false;
			infinityLoop.enabled = false;
		}
	}
	IEnumerator Looper (float waitTime){
	//	PC.OnOffParts (true);
		yield return new WaitForSeconds (actualTimeLength);//wait the length of recording
		if (isLooping){//if its still set to loop play again
			source.Play ();
			StartCoroutine (Looper (actualTimeLength)); //start loop again
		}
		else if (!isLooping){
		//	PC.OnOffParts (false);
		}
	}
}





















