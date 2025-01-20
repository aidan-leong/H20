#pragma strict

public var speed : int = 10;
public var gridSize : int = 1;
private var canMove : boolean = true; // Check if player can move
private var targetPosition : Vector3; // Temporary value for moving (used in coroutines)
private var xInt : int;
private var yInt : int;
private var zInt : int;
public static var steps : int = 0;
	
function Update() {
	xInt = parseInt(transform.position.x);
	yInt = parseInt(transform.position.y);
	zInt = parseInt(transform.position.z);
	
	if(Input.GetKeyDown(KeyCode.W) == true && canMove == true) {
		canMove = false;
		StartCoroutine(MoveInGrid(xInt, yInt, zInt + gridSize));
		AddStepCount();
	}
	if(Input.GetKeyDown(KeyCode.D) == true && canMove == true) {
		canMove = false;
		StartCoroutine(MoveInGrid(xInt + gridSize, yInt, zInt));
		AddStepCount();
	}
	if (Input.GetKeyDown(KeyCode.A) == true && canMove == true) {
		canMove = false;
		StartCoroutine(MoveInGrid(xInt - gridSize, yInt, zInt));
		AddStepCount();
	}
	if (Input.GetKeyDown(KeyCode.S) == true && canMove == true) {
		canMove = false;
		StartCoroutine(MoveInGrid(xInt, yInt, zInt - gridSize));
		AddStepCount();
	}
	print("Steps: " + steps);
}

function MoveInGrid(x : int, y : int, z : int) {
	while (transform.position.x != x || transform.position.y != y || transform.position.z != z) {
		if (transform.position.x < x) { // Moving right along x
			// Move player along x
			targetPosition.x = speed * Time.deltaTime;
			// Check if player goes more than it should go and clamp it if yes
			if (targetPosition.x + transform.position.x > x) {
				targetPosition.x = x - transform.position.x;
			}
		} else if (transform.position.x > x) { // Moving left along x
			// Move player
			targetPosition.x = -speed * Time.deltaTime;
			// Check if player goes more than it should and clamp it if true
			if (targetPosition.x + transform.position.x < x) {
				targetPosition.x = -(transform.position.x - x);
			}
		} else { // Set to 0 when x is unchanged
			targetPosition.x = 0;
		}
		
		// Moving y forward
		if (transform.position.y < y) { // Moving up along y
			// Move player along y
			targetPosition.y = speed * Time.deltaTime;
			// Check if player goes more than it should and clamp it if true
			if (targetPosition.y + transform.position.y > y) {
				targetPosition.y = y - transform.position.y;
			}
		} else if(transform.position.y > y) { // Moving down along y
			// Move player by speed
			targetPosition.y = -speed * Time.deltaTime;
			// Check if player goes more than it should and clamp it if true
			if (targetPosition.y + transform.position.y < y) {
				targetPosition.y = -(transform.position.y - y);
			}
		} else { // Set to 0 when y is unchanged
			targetPosition.y = 0;
		}
		
		// Moving z forward
		if (transform.position.z < z) { // Moving forward along z
			// Move player along z
			targetPosition.z = speed * Time.deltaTime;
			// Check if player goes more than it should and clamp it if true
			if (targetPosition.z + transform.position.z > z) {
				targetPosition.z = z - transform.position.z;
			}
		} else if (transform.position.z > z) { // Moving backward along z
			// Move player by speed
			targetPosition.z = -speed * Time.deltaTime;
			// Check if player goes more than it should and clamp it if yes
			if (targetPosition.z + transform.position.z < z) {
				targetPosition.z = -(transform.position.z - z);
			}
		} else { // Set to 0 when z is unchanged
			targetPosition.z = 0;
		}
		
		transform.Translate(targetPosition);
		yield;
	}
	canMove = true;
}

function AddStepCount() {
	steps -= 1;
	if(steps <= 0) {
		steps = 0;
	}
}