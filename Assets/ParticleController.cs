using UnityEngine;
using System.Collections;

public class ParticleController : MonoBehaviour {
	public ParticleSystem[] parts;

	void Awake (){
	//	OnOffParts (false);
	}

	// Update is called once per frame
	public void OnOffParts (bool isOn) {
		int i = 0;
		if (isOn){
			while (i < parts.Length){
				parts[i].enableEmission = true;
				i++;
			}
		}
		else if (!isOn){
			while (i < parts.Length){
				parts[i].enableEmission = false;
				i++;
			}
		}
	}
}
