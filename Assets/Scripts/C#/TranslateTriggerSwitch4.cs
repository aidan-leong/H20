using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateTriggerSwitch4 : MonoBehaviour {
	private GameObject playerCube;
	public static bool moveStatus2 = false;

	void Start() {
		playerCube = GameObject.Find("Player Cube");
	}

	void OnTriggerEnter(Collider collider) {
		//print(collider.name);
		playerCube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
		moveStatus2 = true;
	}
}