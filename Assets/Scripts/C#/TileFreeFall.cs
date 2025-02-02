using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFreeFall : MonoBehaviour {
	public GameObject fallingTile;

	void Update() {
		if(fallingTile.transform.position.y < -10) {
			print(GetComponent<Collider>().transform.parent.gameObject);
			Destroy(GetComponent<Collider>().transform.parent.gameObject);
		}
	}

	void OnTriggerExit(Collider collider) {
		fallingTile.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation; // Can be rewritten as fallingTile.rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionY;
	}
}