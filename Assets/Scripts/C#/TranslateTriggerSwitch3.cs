using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateTriggerSwitch3 : MonoBehaviour {
	private GameObject playerCube;
	public static bool moveStatus2 = false;

	void Start() {
		playerCube = GameObject.Find("Player Cube");
	}

	void OnTriggerEnter(Collider collider) {
		//print(collider.name);
		playerCube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
		moveStatus2 = true;
	}
}