#pragma strict

var fallingTile : GameObject;

function Update() {
	if(fallingTile.transform.position.y < -10) {
		print(GetComponent.<Collider>().transform.parent.gameObject);
		Destroy(GetComponent.<Collider>().transform.parent.gameObject);
	}
}

function OnTriggerExit(collider : Collider) {
	fallingTile.GetComponent.<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation; // Can be rewritten as fallingTile.rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionY;
}