@script ExecuteInEditMode()

var backToMain : GUIStyle;

function OnGUI () {
	// Buttons with custom style defined in GUI skin
	if(GUI.Button(Rect((Screen.width - 182) * 0.5 + 350, (Screen.height - 64) * 0.5 + 300, 182, 64), "BACK TO MAIN", backToMain))
		SceneManager.LoadScene("Title");
}