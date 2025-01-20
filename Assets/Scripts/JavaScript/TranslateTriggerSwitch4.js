#pragma strict

private var playerCube : GameObject;
public static var moveStatus2 : boolean = false;

function Start() {
	playerCube = gameObject.Find("Player Cube");
}

function OnTriggerEnter(collider : Collider) {
//	print(collider.name);
	playerCube.GetComponent.<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
	moveStatus2 = true;
}