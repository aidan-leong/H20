using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateTriggerSwitch1 : MonoBehaviour {
	public GameObject dynamicTile;
	public static bool moveStatus1 = false;

	void OnTriggerEnter(Collider collider) {
		//	print(collider.name);
		moveStatus1 = true;
	}

	void OnTriggerExit(Collider collider) {
		//	print(collider.name);
		moveStatus1 = false;
	}
}