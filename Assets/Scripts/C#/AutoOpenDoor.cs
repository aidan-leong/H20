using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoOpenDoor : MonoBehaviour
{
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.ClearLevel(5);
            StartCoroutine(GoNextStage());
        }
    }

    private IEnumerator GoNextStage()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Stage 1");
    }
}
