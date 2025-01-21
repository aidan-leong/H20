using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour {
	public Camera camera1;
	public Camera camera2;
	public Camera camera3;

	void Start() {
		camera1.enabled = true;
		camera2.enabled = false;
		camera3.enabled = false;
	}

	void Update() {
		if(Input.GetKeyDown("1")) {
			camera1.enabled = true;
			camera2.enabled = false;
			camera3.enabled = false;
		} else if(Input.GetKeyDown("2")) {
			camera1.enabled = false;
			camera2.enabled = true;
			camera3.enabled = false;
		} else if(Input.GetKeyDown("3")) {
			camera1.enabled = false;
			camera2.enabled = false;
			camera3.enabled = true;
		}
	}
}