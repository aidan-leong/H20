#pragma strict

private var startTime : float = 300; // Time given to complete game
private var timeRemaining : float;

function Start() {
	GetComponent.<GUIText>().material.color = Color.white; // GUI text color
}

function Update() {
	CountDown();
}

function CountDown() {
	timeRemaining = startTime - Time.timeSinceLevelLoad;
	ShowTime();
	
	if(timeRemaining < 0) {
		timeRemaining = 0;
		TimeIsUp();
	}
}

function ShowTime() {
	var minutes : int;
	var seconds : int;
	var timeString : String;
	
	minutes = timeRemaining / 60; // Derive minutes by dividing seconds by 60 seconds
	seconds = timeRemaining % 60; // Derive remainder after dividing by 60 seconds
	timeString = "Time Left > " + minutes.ToString() + ":" + seconds.ToString("d2");
	GetComponent.<GUIText>().text = timeString;
}

function TimeIsUp() {
	SceneManager.LoadScene("Lose");
}