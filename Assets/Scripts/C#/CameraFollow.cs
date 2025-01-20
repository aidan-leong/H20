using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 camPivot = Vector3.zero;
    public Vector3 camRotation = new Vector3(45, 35, 0);

    public float camSpeed = 5.0f;
    public float camDistance = 5.0f;
    public float camOffset = 0f;

    public GameObject target;
    private Vector3 newPos;

    private Vector3 originalPosition;
    private bool isPanning = false;
    private Vector3 targetPosition;

    void Start()
    {
        originalPosition = transform.position; //store the original camera position when the game starts
    }

    void Update()
    {
        if (!isPanning)
        {
            camPivot = target.transform.position; //follow player
            newPos = camPivot;

            transform.eulerAngles = camRotation;
            if (GetComponent<Camera>().orthographic)
            {
                newPos += -transform.forward * camDistance * 4F;
                GetComponent<Camera>().orthographicSize = camDistance;
            }
            else
            {
                newPos += -transform.forward * camDistance;
            }
            newPos += transform.right * camOffset;
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * camSpeed);
        }
    }
    public void MoveCameraToDoor(Vector3 doorPosition)
    {
        if (isPanning)
            return;

        StartCoroutine(PanCameraToDoor(doorPosition));
    }

    private IEnumerator PanCameraToDoor(Vector3 doorPosition)
    {
        isPanning = true;
        targetPosition = doorPosition + new Vector3(0, 10, -2); 

        float duration = 0.6f;
        float timeElapsed = 0f;

        Vector3 startingPos = transform.position;
        while (timeElapsed < duration)
        {
            transform.position = Vector3.Lerp(startingPos, targetPosition, timeElapsed / duration); //move camera smoothly
            timeElapsed += Time.unscaledDeltaTime; //unscaledDeltaTime = WaitForSecondsRealTime = use real seconds instead of in game time to ignore the game time freezing
            yield return null;
        }
        transform.position = targetPosition; 

        yield return new WaitForSeconds(.6f); 

        isPanning = false; //move back
    }
}
