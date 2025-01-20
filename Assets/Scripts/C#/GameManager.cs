using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //optimise if have time cuz wtf is this
    public GameObject Level01Exit;
    public GameObject Level02Exit;
    public GameObject Level03Exit;
    public GameObject Level04Exit;
    public GameObject Level05Exit;
    public bool Level01Cleared = false;
    public bool Level02Cleared = false;
    public bool Level03Cleared = false;
    public bool Level04Cleared = false;
    public bool Level05Cleared = false;

    private bool isDoor01Opened = false;
    private bool isDoor02Opened = false;
    private bool isDoor03Opened = false;
    private bool isDoor04Opened = false;
    private bool isDoor05Opened = false;
    public GameObject LevelClearer01;
    public GameObject LevelClearer02;
    public GameObject LevelClearer03;
    public GameObject LevelClearer04;
    public GameObject LevelClearer05;

    public GameObject Level03MovableBarrier;

    public CameraFollow cameraFollow;



    public void ClearLevel(int level)
    {
        if (level == 1)
        {
            Level01Cleared = true;
        }
        if (level == 2)
        {
            Level02Cleared = true;
        }
        if (level == 3)
        {
            Level03Cleared = true;
        }
        if (level == 4)
        {
            Level04Cleared = true;
        }
        if (level == 5)
        {
            Level05Cleared = true;
        }
        if (level == 6)
        {
            SceneManager.LoadScene("Win");
        }
    }

    private IEnumerator OpenDoor01()
    {
        isDoor01Opened = true;

        //trigger camera to move to the door position
        cameraFollow.MoveCameraToDoor(Level01Exit.transform.position);

        RotatingObject script = LevelClearer01.GetComponent<RotatingObject>(); //small section to prevent furthur movement after level clearance
        if (script != null)
        {
            Destroy(script);
        }

        Vector3 targetPosition = Level01Exit.transform.position + new Vector3(0, -1, 0); //set the targeted pos to be -1y of og pos

        while (Vector3.Distance(Level01Exit.transform.position, targetPosition) > 0.01f)
        {
            Level01Exit.transform.position = Vector3.MoveTowards(
                Level01Exit.transform.position,
                targetPosition,
                1f * Time.deltaTime
            ); //move door towards target position

            yield return null;
        }
        Level01Exit.transform.position = targetPosition; //ensures door is exactly moved down by 1 so player can move across smoothly
    }

    private IEnumerator OpenDoor02()
    {
        isDoor02Opened = true;

        cameraFollow.MoveCameraToDoor(Level02Exit.transform.position);

        RotatingObject script = LevelClearer02.GetComponent<RotatingObject>(); //small section to prevent furthur movement after level clearance
        if (script != null)
        {
            Destroy(script);
        }

        Vector3 targetPosition = Level02Exit.transform.position + new Vector3(0, -1, 0);

        while (Vector3.Distance(Level02Exit.transform.position, targetPosition) > 0.01f)
        {
            Level02Exit.transform.position = Vector3.MoveTowards(
                Level02Exit.transform.position,
                targetPosition,
                1f * Time.deltaTime
            );

            yield return null;
        }
        Level02Exit.transform.position = targetPosition;
    }

    private IEnumerator OpenDoor03()
    {
        isDoor03Opened = true;

        cameraFollow.MoveCameraToDoor(Level03Exit.transform.position);

        RotatingObject script = LevelClearer03.GetComponent<RotatingObject>(); //small section to prevent furthur movement after level clearance; qol changes
        if (script != null)
        {
            Destroy(script);
        }

        Destroy(Level03MovableBarrier); //remove the movable object so the beam stays even when player isn't on pressure plate

        Vector3 targetPosition = Level03Exit.transform.position + new Vector3(0, -1, 0);

        while (Vector3.Distance(Level03Exit.transform.position, targetPosition) > 0.01f)
        {
            Level03Exit.transform.position = Vector3.MoveTowards(
                Level03Exit.transform.position,
                targetPosition,
                1f * Time.deltaTime
            );

            yield return null;
        }
        Level03Exit.transform.position = targetPosition;
    }

    private IEnumerator OpenDoor04()
    {
        isDoor04Opened = true;

        cameraFollow.MoveCameraToDoor(Level04Exit.transform.position);

        RotatingObject script = LevelClearer04.GetComponent<RotatingObject>(); //small section to prevent furthur movement after level clearance
        if (script != null)
        {
            Destroy(script);
        }

        Vector3 targetPosition = Level04Exit.transform.position + new Vector3(0, -1, 0);

        while (Vector3.Distance(Level04Exit.transform.position, targetPosition) > 0.01f)
        {
            Level04Exit.transform.position = Vector3.MoveTowards(
                Level04Exit.transform.position,
                targetPosition,
                1f * Time.deltaTime
            );

            yield return null;
        }
        Level04Exit.transform.position = targetPosition;
    }

    private IEnumerator OpenDoor05()
    {
        isDoor05Opened = true;

        cameraFollow.MoveCameraToDoor(Level05Exit.transform.position);

        RotatingObject script = LevelClearer05.GetComponent<RotatingObject>(); //small section to prevent furthur movement after level clearance
        if (script != null)
        {
            Destroy(script);
        }

        Vector3 targetPosition = Level05Exit.transform.position + new Vector3(0, -1, 0);

        while (Vector3.Distance(Level05Exit.transform.position, targetPosition) > 0.01f)
        {
            Level05Exit.transform.position = Vector3.MoveTowards(
                Level05Exit.transform.position,
                targetPosition,
                1f * Time.deltaTime
            );

            yield return null;
        }
        Level05Exit.transform.position = targetPosition;

    }

    private void Update() //check for level clearance
    {
        if (Level01Cleared && !isDoor01Opened)
        {
            StartCoroutine(OpenDoor01());
        }

        if (Level02Cleared && !isDoor02Opened)
        {
            StartCoroutine(OpenDoor02());
        }

        if (Level03Cleared && !isDoor03Opened)
        {
            StartCoroutine(OpenDoor03());
        }

        if (Level04Cleared && !isDoor04Opened)
        {
            StartCoroutine(OpenDoor04());
        }

        if (Level05Cleared && !isDoor05Opened)
        {
            StartCoroutine(OpenDoor05());
        }
    }
}
