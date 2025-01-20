using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBlock : MonoBehaviour {
	private Vector3 targetPosition;
	private bool isMoving = false;
	private float yOrigin;

	void Start() {
		targetPosition = transform.position;
		yOrigin = transform.position.y;
	}

	void Update() {
		if(Vector3.Distance(transform.position, targetPosition) < 0.01 && isMoving) {
			isMoving = false;
		} else {
			// Fix position
			transform.position = Vector3.Lerp(transform.position, targetPosition, 8 * Time.deltaTime);
		}
	}

	public void Move(Vector3 direction, int distance) {
		RaycastHit hit;
		Physics.Linecast(transform.position, transform.position + direction * -1, out hit);
		Debug.DrawLine(transform.position, transform.position + direction * -1, Color.red);
		if (hit.collider != null && !hit.collider.isTrigger) {
			Debug.Log(hit.collider.name);
		} else {
			transform.position = new Vector3(Mathf.Ceil(transform.position.x) - 0.5f, yOrigin, Mathf.Ceil(transform.position.z) - 0.5f);
			targetPosition = transform.position + (direction * -distance);
			isMoving = true;
			targetPosition = new Vector3(targetPosition.x, yOrigin, targetPosition.z);
		}
	}
}