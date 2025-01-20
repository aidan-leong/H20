#pragma strict

var colorStart : Color = Color.red;
var colorEnd : Color = Color.blue;
var duration : float = 1.0;

function Update () {
	var lerp : float = Mathf.PingPong (Time.time, duration) / duration;
	GetComponent.<Renderer>().material.color = Color.Lerp(colorStart, colorEnd, lerp);
}