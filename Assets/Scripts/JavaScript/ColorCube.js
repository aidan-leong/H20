#pragma strict

public var color : Color[];
//public var texture : Texture[];

function Update () {
	if(ColorTriggerSwitch2.colorStatus == true) {
		GetComponent.<Renderer>().material.color = color[0];
//		renderer.material.mainTexture = texture[0];
	} else {
		GetComponent.<Renderer>().material.color = color[1];
//		renderer.material.mainTexture = texture[1];
	}
}