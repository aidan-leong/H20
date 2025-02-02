using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1GameManager : MonoBehaviour
{
    public static bool trigger1;
    public static bool trigger2;
    public static bool trigger3;
    public static bool trigger4;
    public static bool trigger5;

    void Start()
    {
        trigger1 = false;
        trigger2 = false;
        trigger3 = false;
        trigger4 = false;
        trigger5 = false;
    }
  
    void Update()
    {
        if (trigger1 && trigger2 && trigger3 && trigger4 && trigger5)
        {
            StartCoroutine(routine:EndGame());
        }
        
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Level 2");
    }
}