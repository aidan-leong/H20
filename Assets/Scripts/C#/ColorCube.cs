using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCube : MonoBehaviour {
	public Color[] color;
	//public Texture[] texture;

	// Update is called once per frame
	void Update () {
		if(ColorTriggerSwitch2.colorStatus == true) {
			GetComponent<Renderer>().material.color = color[0];
			//renderer.material.mainTexture = texture[0];
		} else {
			GetComponent<Renderer>().material.color = color[1];
			//renderer.material.mainTexture = texture[1];
		}
	}
}
