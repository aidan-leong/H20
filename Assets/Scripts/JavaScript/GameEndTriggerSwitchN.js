#pragma strict

function OnTriggerEnter(collider : Collider) {
	yield WaitForSeconds(5);
	SceneManager.LoadScene("Win");
}