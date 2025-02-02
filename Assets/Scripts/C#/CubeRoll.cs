using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeRoll : MonoBehaviour {
	public Transform cubeMesh;
	public bool rollForever = false;
	private float rollSpeed = 400;
	private bool isMoving = false;
	private RaycastHit hit;
	public Vector3 pivot;
	private float cubeSize = 1; // Block cube size
	private bool flipping = false;
	public static int steps;
	
	public enum CubeDirection {none, left, up, right, down};
	public CubeDirection direction = CubeDirection.none;
	
	void Start() {
		steps = 100;
	}

	void Update() {
		if(direction == CubeDirection.none) {
			if (Input.GetKeyDown(KeyCode.D)) {
				direction = CubeDirection.right;
				//DeductStepCount();
			}
			if (Input.GetKeyDown(KeyCode.A)) {
				direction = CubeDirection.left;
				//DeductStepCount();
			}
			if (Input.GetKeyDown(KeyCode.W)) {
				direction = CubeDirection.up;
				//DeductStepCount();
			}
			if (Input.GetKeyDown(KeyCode.S)) {
				direction = CubeDirection.down;
				//DeductStepCount();
			}
		}
		else {
			if(!isMoving) {
				Flip();
				if(CheckCollision(direction)) {
					isMoving = false;
					direction = CubeDirection.none;
				} else {
					DeductStepCount();
					isMoving = true;
				}
			}

			switch(direction) {
				case CubeDirection.right:
					cubeMesh.transform.RotateAround(pivot, -Vector3.forward, rollSpeed * Time.deltaTime);
					if (cubeMesh.transform.rotation.z <= -0.7)
						ResetPosition();
					break;
				case CubeDirection.left:
					cubeMesh.transform.RotateAround(pivot, Vector3.forward, rollSpeed * Time.deltaTime);
					if (cubeMesh.transform.rotation.z >= 0.7)
						ResetPosition();
					break;
				case CubeDirection.up:
					cubeMesh.transform.RotateAround(pivot, Vector3.right, rollSpeed * Time.deltaTime);
					if (cubeMesh.transform.rotation.x >= 0.7)
						ResetPosition();
					break;
				case CubeDirection.down:
					cubeMesh.transform.RotateAround(pivot, -Vector3.right, rollSpeed * Time.deltaTime);
					if (cubeMesh.transform.rotation.x <= -0.7) {
						ResetPosition();
					}
					break;
			}
		}

		if(transform.position.y <= -10) {
			SceneManager.LoadScene("Lose");
		}
	}

	void ResetPosition() {
		cubeMesh.transform.rotation = Quaternion.Euler(Vector3.zero);
		transform.position = new Vector3(Mathf.Ceil(cubeMesh.transform.position.x) - 0.5f, transform.position.y, Mathf.Ceil(cubeMesh.transform.position.z) - 0.5f);
		cubeMesh.transform.localPosition = Vector3.zero;
		isMoving = false;

		if(CheckCollision(direction) && hit.collider != null) {
			if(hit.collider.gameObject.GetComponent<PushBlock>()) {
				hit.collider.gameObject.GetComponent<PushBlock>().Move((transform.position - hit.collider.transform.position).normalized, 1);
			}
		}

		if (!rollForever)
			direction = CubeDirection.none;
	}

	void Flip() {
		if(flipping)
			return; // Ignore other commands while flipping
		flipping = true; // Signal that it is currently flipping

		switch(direction) {
			case CubeDirection.right:
				pivot = new Vector3(1, -1, 0);
				break;
			case CubeDirection.left:
				pivot = new Vector3(-1, -1, 0);
				break;
			case CubeDirection.up:
				pivot = new Vector3(0, -1, 1);
				break;
			case CubeDirection.down:
				pivot = new Vector3(0, -1, -1);
				break;
		}

		// Calculates the point around which the block will flop
		pivot = transform.position + (pivot * cubeSize * 0.5f);
		if(GetComponent<AudioSource>())
			GetComponent<AudioSource>().Play(); // Play the flop sound 
		flipping = false;
	}

	bool CheckCollision(CubeDirection direction) {
		switch(direction) {
			case CubeDirection.right:
				Physics.Linecast(transform.position, transform.position + transform.right* 1, out hit);
				Debug.DrawLine(transform.position, transform.position + transform.right* 1, Color.black);
				break;
			case CubeDirection.left:
				Physics.Linecast(transform.position, transform.position + transform.right* -1, out hit);
				Debug.DrawLine(transform.position, transform.position + transform.right* -1, Color.black);
				break;
			case CubeDirection.up:
				Physics.Linecast(transform.position, transform.position + transform.forward* 1, out hit);
				Debug.DrawLine(transform.position, transform.position + transform.forward* 1, Color.black);
				break;
			case CubeDirection.down:
				Physics.Linecast(transform.position, transform.position + transform.forward* -1, out hit);
				Debug.DrawLine(transform.position, transform.position + transform.forward* -1, Color.black);
				break;
		}

		if(hit.collider == null || (hit.collider != null && hit.collider.isTrigger && !hit.collider.GetComponent("Player"))) {
			return false;
		} else {
			return true;
		}
	}

	void DeductStepCount() {
		steps -= 1;

		if(steps <= 0) {
			steps = 0;
		}
	}
}