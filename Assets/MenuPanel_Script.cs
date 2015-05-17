using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuPanel_Script : MonoBehaviour {
	public Canvas menuPanel;
	public InputField soundName;

	public void MenuPanelOpen (){
		menuPanel.enabled = true;
	}
	public void MenuPanelClose (){
		menuPanel.enabled = false;
	}
/*	public void RenameSound (){
		soundName.onEndEdit = ;
	}*/
}
