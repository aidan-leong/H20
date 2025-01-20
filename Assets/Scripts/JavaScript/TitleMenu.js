import UnityEngine.SceneManagement;

@script ExecuteInEditMode()

var playGame : GUIStyle;
var instructions : GUIStyle;
var credits : GUIStyle;
var exit : GUIStyle;

function OnGUI () {
	if(GUI.Button(Rect((Screen.width - 182) * 0.5, 350, 182, 64), "PLAY GAME", playGame))
		SceneManager.LoadScene("Game");

	if(GUI.Button(Rect((Screen.width - 182) * 0.5, 450, 182, 64), "INSTRUCTIONS", instructions))
		SceneManager.LoadScene("Instructions");

	if(GUI.Button(Rect((Screen.width - 182) * 0.5, 550, 182, 64), "CREDITS", credits))
		SceneManager.LoadScene("Credits");

	if(GUI.Button(Rect((Screen.width - 182) * 0.5, 650, 182, 64), "EXIT", exit))
		Application.Quit();
}