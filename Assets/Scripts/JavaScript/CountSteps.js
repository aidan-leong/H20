#pragma strict

function Update () {
	GetComponent.<GUIText>().text = "STEPS LEFT > " + CubeRoll.steps.ToString();
}