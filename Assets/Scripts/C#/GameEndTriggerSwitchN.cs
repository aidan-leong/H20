using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndTriggerSwitchN : MonoBehaviour {
	IEnumerator OnTriggerEnter(Collider collider) {
		yield return new WaitForSeconds(0.1f);
		SceneManager.LoadScene("Win");
	}
}