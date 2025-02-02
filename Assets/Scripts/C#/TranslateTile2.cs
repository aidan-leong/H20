using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateTile2 : MonoBehaviour {
	public Transform targetC;
	public Transform targetD;
	private Transform currentTarget;
	private float proximity = 0.0001f;
	private float speed = 2.0f;

	void Start() {
		currentTarget = targetC;
	}

	void Update() {
		Vector3 distance = transform.position - currentTarget.transform.position;
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
}