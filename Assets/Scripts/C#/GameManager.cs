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
    public GameObject Level06Exit;
    public GameObject Level07Exit;
    public GameObject Level08Exit;
    public GameObject Level09Exit;
    public bool Level01Cleared = false;
    public bool Level02Cleared = false;
    public bool Level03Cleared = false;
    public bool Level04Cleared = false;
    public bool Level05Cleared = false;
    public bool Level06Cleared = false;
    public bool Level07Cleared = false;
    public bool Level08Cleared = false;
    public bool Level09Cleared = false;

    private bool isDoor01Opened = false;
    private bool isDoor02Opened = false;
    private bool isDoor03Opened = false;
    private bool isDoor04Opened = false;
    private bool isDoor05Opened = false;
    private bool isDoor06Opened = false;
    private bool isDoor07Opened = false;
    private bool isDoor08Opened = false;
    private bool isDoor09Opened = false;
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
            Level06Cleared = true;
        }
        if (level == 7)
        {
            Level07Cleared = true;
        }
        if (level == 8)
        {
            Level08Cleared = true;
        }
        if (level == 9)
        {
            Level09Cleared = true;
        }
    }

    private void OpenDoor01()
    {
        isDoor01Opened = true;

        //REPLACE DOOR ANIM

        // //trigger camera to move to the door position
        // cameraFollow.MoveCameraToDoor(Level01Exit.transform.position, 8.0f, new Vector3(60, 0, 0));

        // Vector3 targetPosition = Level01Exit.transform.position + new Vector3(0, -1, 0); //set the targeted pos to be -1y of og pos

        // while (Vector3.Distance(Level01Exit.transform.position, targetPosition) > 0.01f)
        // {
        //     Level01Exit.transform.position = Vector3.MoveTowards(
        //         Level01Exit.transform.position,
        //         targetPosition,
        //         1f * Time.deltaTime
        //     ); //move door towards target position

        //     yield return null;
        // }
        // Level01Exit.transform.position = targetPosition; //ensures door is exactly moved down by 1 so player can move across smoothly

        Destroy(Level01Exit);
    }

    private void OpenDoor02()
    {
        isDoor02Opened = true;

        // cameraFollow.MoveCameraToDoor(Level02Exit.transform.position, 5.0f, new Vector3(60, 45, 0));

        // Vector3 targetPosition = Level02Exit.transform.position + new Vector3(0, -1, 0);

        // while (Vector3.Distance(Level02Exit.transform.position, targetPosition) > 0.01f)
        // {
        //     Level02Exit.transform.position = Vector3.MoveTowards(
        //         Level02Exit.transform.position,
        //         targetPosition,
        //         1f * Time.deltaTime
        //     );

        //     yield return null;
        // }
        // Level02Exit.transform.position = targetPosition;

        Level02Exit.SetActive(false);
    }

    private void OpenDoor03()
    {
        isDoor03Opened = true;

        // cameraFollow.MoveCameraToDoor(Level03Exit.transform.position, 5.0f, new Vector3(60, 45, 0));

        // Destroy(Level03MovableBarrier); //remove the movable object so the beam stays even when player isn't on pressure plate

        // Vector3 targetPosition = Level03Exit.transform.position + new Vector3(0, -1, 0);

        // while (Vector3.Distance(Level03Exit.transform.position, targetPosition) > 0.01f)
        // {
        //     Level03Exit.transform.position = Vector3.MoveTowards(
        //         Level03Exit.transform.position,
        //         targetPosition,
        //         1f * Time.deltaTime
        //     );

        //     yield return null;
        // }
        // Level03Exit.transform.position = targetPosition;

        Destroy(Level03Exit);
    }

    private void OpenDoor04()
    {
        isDoor04Opened = true;

        // cameraFollow.MoveCameraToDoor(Level04Exit.transform.position, 5.0f, new Vector3(60, 45, 0));

        // Vector3 targetPosition = Level04Exit.transform.position + new Vector3(0, -1, 0);

        // while (Vector3.Distance(Level04Exit.transform.position, targetPosition) > 0.01f)
        // {
        //     Level04Exit.transform.position = Vector3.MoveTowards(
        //         Level04Exit.transform.position,
        //         targetPosition,
        //         1f * Time.deltaTime
        //     );

        //     yield return null;
        // }
        // Level04Exit.transform.position = targetPosition;

        Destroy(Level04Exit);
    }

    private void OpenDoor05()
    {
        isDoor05Opened = true;

        // cameraFollow.MoveCameraToDoor(Level05Exit.transform.position, 5.0f, new Vector3(60, 45, 0));

        // Vector3 targetPosition = Level05Exit.transform.position + new Vector3(0, -1, 0);

        // while (Vector3.Distance(Level05Exit.transform.position, targetPosition) > 0.01f)
        // {
        //     Level05Exit.transform.position = Vector3.MoveTowards(
        //         Level05Exit.transform.position,
        //         targetPosition,
        //         1f * Time.deltaTime
        //     );

        //     yield return null;
        // }
        // Level05Exit.transform.position = targetPosition;

        Destroy(Level05Exit);
    }

    private void OpenDoor06()
    {
        isDoor05Opened = true;

        // cameraFollow.MoveCameraToDoor(Level06Exit.transform.position, 5.0f, new Vector3(60, 45, 0));

        // Vector3 targetPosition = Level06Exit.transform.position + new Vector3(0, -1, 0);

        // while (Vector3.Distance(Level06Exit.transform.position, targetPosition) > 0.01f)
        // {
        //     Level06Exit.transform.position = Vector3.MoveTowards(
        //         Level06Exit.transform.position,
        //         targetPosition,
        //         1f * Time.deltaTime
        //     );

        //     yield return null;
        // }
        // Level06Exit.transform.position = targetPosition;

        Destroy(Level06Exit);
    }

    private void OpenDoor07()
    {
        isDoor07Opened = true;

        // cameraFollow.MoveCameraToDoor(Level07Exit.transform.position, 5.0f, new Vector3(60, 45, 0));

        // Vector3 targetPosition = Level07Exit.transform.position + new Vector3(0, -1, 0);

        // while (Vector3.Distance(Level07Exit.transform.position, targetPosition) > 0.01f)
        // {
        //     Level07Exit.transform.position = Vector3.MoveTowards(
        //         Level07Exit.transform.position,
        //         targetPosition,
        //         1f * Time.deltaTime
        //     );

        //     yield return null;
        // }
        // Level07Exit.transform.position = targetPosition;

        Destroy(Level07Exit);
    }

    private void OpenDoor08()
    {
        isDoor08Opened = true;

        // cameraFollow.MoveCameraToDoor(Level08Exit.transform.position, 5.0f, new Vector3(60, 45, 0));

        // Vector3 targetPosition = Level08Exit.transform.position + new Vector3(0, -1, 0);

        // while (Vector3.Distance(Level08Exit.transform.position, targetPosition) > 0.01f)
        // {
        //     Level08Exit.transform.position = Vector3.MoveTowards(
        //         Level08Exit.transform.position,
        //         targetPosition,
        //         1f * Time.deltaTime
        //     );

        //     yield return null;
        // }
        // Level08Exit.transform.position = targetPosition;

        Destroy(Level08Exit);
    }

    private void OpenDoor09()
    {
        isDoor09Opened = true;

        // cameraFollow.MoveCameraToDoor(Level09Exit.transform.position, 5.0f, new Vector3(60, 45, 0));

        // Vector3 targetPosition = Level09Exit.transform.position + new Vector3(0, -1, 0);

        // while (Vector3.Distance(Level09Exit.transform.position, targetPosition) > 0.01f)
        // {
        //     Level09Exit.transform.position = Vector3.MoveTowards(
        //         Level09Exit.transform.position,
        //         targetPosition,
        //         1f * Time.deltaTime
        //     );

        //     yield return null;
        // }
        // Level09Exit.transform.position = targetPosition;

        Destroy(Level09Exit);
    }

    private void Update() //check for level clearance
    {
        if (Level01Cleared && !isDoor01Opened)
        {
            OpenDoor01();
        }

        if (Level02Cleared && !isDoor02Opened)
        {
            OpenDoor02();
        }

        if (Level03Cleared && !isDoor03Opened)
        {
            OpenDoor03();
        }

        if (Level04Cleared && !isDoor04Opened)
        {
            OpenDoor04();
        }

        if (Level05Cleared && !isDoor05Opened)
        {
            OpenDoor05();
        }

        if (Level06Cleared && !isDoor06Opened)
        {
            OpenDoor06();
        }
        if (Level07Cleared && !isDoor07Opened)
        {
            OpenDoor07();
        }
        if (Level08Cleared && !isDoor08Opened)
        {
            OpenDoor08();
        }
        if (Level09Cleared && !isDoor09Opened)
        {
            OpenDoor09();
        }
    }
}
