using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]

public class TitleMenu : MonoBehaviour {
	public GUIStyle playGame;
	public GUIStyle instructions;
	public GUIStyle credits;
	public GUIStyle exit;

	void OnGUI() {
		if(GUI.Button(new Rect((Screen.width - 182) * 0.5f, 350, 182, 64), "PLAY GAME", playGame))
			SceneManager.LoadScene("Game");

		if(GUI.Button(new Rect((Screen.width - 182) * 0.5f, 450, 182, 64), "INSTRUCTIONS", instructions))
			SceneManager.LoadScene("Instructions");

		if(GUI.Button(new Rect((Screen.width - 182) * 0.5f, 550, 182, 64), "CREDITS", credits))
			SceneManager.LoadScene("Credits");

		if(GUI.Button(new Rect((Screen.width - 182) * 0.5f, 650, 182, 64), "EXIT", exit))
			Application.Quit();
	}
}