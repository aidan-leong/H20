#pragma strict

public var dynamicTile : GameObject;
public static var moveStatus1 : boolean = false;

function OnTriggerEnter(collider : Collider) {
//	print(collider.name);
	moveStatus1 = true;
}

function OnTriggerExit(collider : Collider) {
//	print(collider.name);
	moveStatus1 = false;
}