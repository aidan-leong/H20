using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpColor : MonoBehaviour {
	public Color colorStart = Color.red;
	public Color colorEnd = Color.blue;
	public float duration = 1.0f;

	void Update () {
		float lerp = Mathf.PingPong(Time.time, duration) / duration;
		GetComponent<Renderer>().material.color = Color.Lerp(colorStart, colorEnd, lerp);
	}
}
