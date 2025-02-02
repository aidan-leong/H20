using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountSteps : MonoBehaviour {
	void Update() {
		GetComponent<Text>().text = "STEPS > " + CubeRoll.steps.ToString();
	}
}