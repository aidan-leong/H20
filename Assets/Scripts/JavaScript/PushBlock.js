#pragma strict

private var targetPosition : Vector3;
private var isMoving : boolean = false;
private var yOrigin : float;

function Start() {
	targetPosition = transform.position;
	yOrigin = transform.position.y;
}

function Update() {
	if (Vector3.Distance(transform.position, targetPosition) < 0.01 && isMoving) {
		isMoving = false;
	} else {
		// Fix position
		transform.position = Vector3.Lerp(transform.position, targetPosition, 8 * Time.deltaTime);
	}
}

public function Move(direction : Vector3, distance : int) {
	var hit : RaycastHit;
	Physics.Linecast(transform.position, transform.position + direction * -1, hit);
	Debug.DrawLine(transform.position, transform.position + direction * -1, Color.red);
	if (hit.collider != null && !hit.collider.isTrigger) {
		Debug.Log(hit.collider.name);
	} else {
		transform.position = Vector3(Mathf.Ceil(transform.position.x) - 0.5, yOrigin, Mathf.Ceil(transform.position.z) - 0.5);
		targetPosition = transform.position + (direction * -distance);
		isMoving = true;
		targetPosition = Vector3(targetPosition.x, yOrigin, targetPosition.z);
	}
}