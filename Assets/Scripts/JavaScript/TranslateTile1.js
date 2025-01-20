#pragma strict

var targetA : Transform;
var targetB : Transform;
private var currentTarget : Transform;
private var proximity : float = 0.0001;
private var speed : float = 3.0;

function Start () {
	currentTarget = targetA;
}

function Update () {
	var distance : Vector3 = transform.position - currentTarget.transform.position;
//	print(TranslateTriggerSwitch1.moveStatus1);
	
	// Change current target to next one when distance is less than or equal to defined proximity
	if(distance.magnitude <= proximity && TranslateTriggerSwitch1.moveStatus1 == true) {
		currentTarget = targetB;
	}
	if(distance.magnitude <= proximity && TranslateTriggerSwitch1.moveStatus1 == false) {
		currentTarget = targetA;
	}
	
	transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, Time.deltaTime * speed); // Object move towards current target
}