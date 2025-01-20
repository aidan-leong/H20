#pragma strict

var camPivot : Vector3 = Vector3.zero;
var camRotation : Vector3 = Vector3.zero;

var camSpeed : float = 5.0;
var camDistance : float = 0F; 
var camOffset : float = 0F;

var target : GameObject;

private var newPos : Vector3;

function Update () {
	camPivot = target.transform.position;
	newPos = camPivot;
	
	transform.eulerAngles = camRotation;
	if(GetComponent.<Camera>().orthographic) {
		newPos += -transform.forward * camDistance * 4F;
		GetComponent.<Camera>().orthographicSize = camDistance;
	}
	else newPos += -transform.forward * camDistance;
	newPos += transform.right * camOffset;
	transform.position = Vector3.Lerp(transform.position,newPos,Time.deltaTime * camSpeed);
}