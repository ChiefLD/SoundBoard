using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using UnityEditor;

public class NewGM_Script : MonoBehaviour {
	public NewRecordAudio[] nRA;
	public Toggle toggleLoop;
	public InputField renameInput; //the box to click and rename a sound
	string newSoundName; //the value of renameInput given by the player
	string soundTitle; //temp placeholder for getting and setting playerprefs "SoundTitle"

	public Text inputTextTitle; //changes depending on the lastButtonPressed
	public int lastButtonPressed = 1; //the number of the sound that was last pressed, assigned by recordAudio_Script

	public Toggle recButton; //the record Button
	public bool recording; //is the player currently recording
	bool recLock; //keeps recording whether or not they are holding down
	public int maxRecTime; //able to be set by the player, uses memory

	public Text[] soundTexts; //all the sound names
	int textDir; //soundTexts directory

	//only used in the Update to dtermin how long /since/ the player has pressed a button
	public float pressedRecTimer; 

	void Awake (){
		UpdateTexts (); //start off by making sure all the sound names are updated
	}
	void Update (){
		inputTextTitle.text = ("Rename Sound: " +lastButtonPressed);//change the input title relative to the last button pressed

		if (recording) {
			recButton.image.color = Color.red;
		}
		else if (!recording){
			recButton.image.color = Color.white;
		}
		if (pressedRecTimer >= 2){
			pressedRecTimer = 2; 
		}
	}
	void FixedUpdate (){
		//Sets both timers to false if 0
		if (pressedRecTimer <= 0) { 
			pressedRecTimer = 0; 
		}
		//else they are true if above 0 and should count down
		if (pressedRecTimer > 0) {
			pressedRecTimer -= 2 * Time.deltaTime;
		}
	}
	//Updates all of the sound blocks titles
	public void UpdateTexts (){
		textDir = 0;
		while (textDir < soundTexts.Length) {
			soundTitle = PlayerPrefs.GetString ("SoundTitle" +textDir);
			soundTexts[textDir].text = soundTitle;
			textDir++;
		}
	}
	//used when the player puts something in the inputField
	public void RenameSound (){
		int i = lastButtonPressed - 1;
		soundTitle = renameInput.text;
		PlayerPrefs.SetString ("SoundTitle"+i, soundTitle);
		PlayerPrefs.Save ();
		UpdateTexts (); //after renaming and saving a soundName update the names again
	}
	//when the player is holding or the rec button is locked
	public void RecModeStart (){
		if (recording){
			recButton.isOn = false;
			recording = false;
			recLock = false;
		}
		else if (!recording){
			recButton.isOn = true;
			recording = true;
			recLock = true;
		}
	}
	//when the player releases the rec button
	public void RecModeEnd (){
		if (!recLock){
			recording = false;
		}
	}
	public void CheckLoopToggle (bool isLooping){ //set by recAudio display toggle check if sound[] islooping
		if (isLooping) {
			toggleLoop.isOn = true;
		}
		else if (!isLooping){
			toggleLoop.isOn = false;
		}
	}
	public void OnToggleClick (){
		int i = lastButtonPressed - 1;
		if (toggleLoop.isOn){
			nRA[i].SetLoop(true);
		}
		else if (!toggleLoop.isOn){
			nRA[i].SetLoop (false);
		}
	}
}



















