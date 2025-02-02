using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
	private float rotationSpeed = 100;
	
	void Update() {
		transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
	}
}
