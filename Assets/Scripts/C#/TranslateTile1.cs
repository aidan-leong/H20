using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateTile1 : MonoBehaviour {
	public Transform targetA;
	public Transform targetB;
	private Transform currentTarget;
	private float proximity = 0.0001f;
	private float speed = 3.0f;

	void Start() {
		currentTarget = targetA;
	}

	void Update() {
		Vector3 distance = transform.position - currentTarget.transform.position;
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
}