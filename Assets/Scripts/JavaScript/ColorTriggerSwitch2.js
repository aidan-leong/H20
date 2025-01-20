#pragma strict

var colorTile : GameObject;
static var colorStatus : boolean = false;

function OnTriggerEnter(collider : Collider) {
//	print(collider.name);
	colorStatus = true;
}

function OnTriggerExit(collider : Collider) {
//	print(collider.name);
	colorStatus = false;
}