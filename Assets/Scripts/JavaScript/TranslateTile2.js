#pragma strict

var targetC : Transform;
var targetD : Transform;
private var currentTarget : Transform;
private var proximity : float = 0.0001;
private var speed : float = 2.0;

function Start() {
	currentTarget = targetC;
}

function Update() {
	var distance : Vector3 = transform.position - currentTarget.transform.position;
//	print(TranslateTriggerSwitch.moveStatus);
	
	// Change current target to next one when distance is less than or equal to defined proximity
	if(distance.magnitude <= proximity && TranslateTriggerSwitch3.moveStatus2 == true) {
		currentTarget = targetD;
	}

	if(distance.magnitude <= proximity && TranslateTriggerSwitch3.moveStatus2 == false) {
		currentTarget = targetC;
	}
	
	transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, Time.deltaTime * speed); // Object move towards current target
}