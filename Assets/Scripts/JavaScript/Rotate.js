private var rotationSpeed : float = 100;

function Update() {
	transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
}