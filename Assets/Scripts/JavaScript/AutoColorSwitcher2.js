#pragma strict

private var state : boolean = true;

function Start() {
	while(true) {
		yield WaitForSeconds(1.5); // Delay for 1.5 seconds
		if(state) {
			GreenColor();
			state = false;
		} else {
			OrangeColor();
			state = true;
		}
	}
}

function GreenColor() {
	GetComponent.<Renderer>().material.color = Color(0.847, 1, 0, 1);
}

function OrangeColor() {
	GetComponent.<Renderer>().material.color = Color(1, 0.816, 0, 1);
}