using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CubeSlide : MonoBehaviour
{
    int speed = 10;
    int gridSize = 1;
    private bool canMove = true;
    private Vector3 targetPosition;
    private int xInt;
    private int yInt;
    private int zInt;

    [SerializeField] private LayerMask opaqueLayer;

    void Update()
    {
        xInt = Mathf.RoundToInt(transform.position.x);
        yInt = Mathf.RoundToInt(transform.position.y);
        zInt = Mathf.RoundToInt(transform.position.z);

        if (Input.GetKeyDown(KeyCode.W) && canMove)
        {
            if (!IsBarrierInDirection(Vector3.forward)) 
            {
                canMove = false;
                StartCoroutine(MoveInGrid(xInt, yInt, zInt + gridSize));
            }
        }
        if (Input.GetKeyDown(KeyCode.D) && canMove)
        {
            if (!IsBarrierInDirection(Vector3.right))
            {
                canMove = false;
                StartCoroutine(MoveInGrid(xInt + gridSize, yInt, zInt));
            }
        }
        if (Input.GetKeyDown(KeyCode.A) && canMove)
        {
            if (!IsBarrierInDirection(Vector3.left))
            {
                canMove = false;
                StartCoroutine(MoveInGrid(xInt - gridSize, yInt, zInt));
            }
        }
        if (Input.GetKeyDown(KeyCode.S) && canMove)
        {
            if (!IsBarrierInDirection(Vector3.back))
            {
                canMove = false;
                StartCoroutine(MoveInGrid(xInt, yInt, zInt - gridSize));
            }
        }
    }

    IEnumerator MoveInGrid(int x, int y, int z) //entire section if basically movin player transform by 1 in either x -x z or -z to account for 4 dir
    {
        while (transform.position.x != x || transform.position.y != y || transform.position.z != z)
        {
            if (transform.position.x < x)
            {
                targetPosition.x = speed * Time.deltaTime;
                if (targetPosition.x + transform.position.x > x)
                {
                    targetPosition.x = x - transform.position.x;
                }
            }
            else if (transform.position.x > x)
            {
                targetPosition.x = -speed * Time.deltaTime;
                if (targetPosition.x + transform.position.x < x)
                {
                    targetPosition.x = -(transform.position.x - x);
                }
            }
            else
            {
                targetPosition.x = 0;
            }

            if (transform.position.y < y)
            {
                targetPosition.y = speed * Time.deltaTime;
                if (targetPosition.y + transform.position.y > y)
                {
                    targetPosition.y = y - transform.position.y;
                }
            }
            else if (transform.position.y > y)
            {
                targetPosition.y = -speed * Time.deltaTime;
                if (targetPosition.y + transform.position.y < y)
                {
                    targetPosition.y = -(transform.position.y - y);
                }
            }
            else
            {
                targetPosition.y = 0;
            }

            if (transform.position.z < z)
            {
                targetPosition.z = speed * Time.deltaTime;
                if (targetPosition.z + transform.position.z > z)
                {
                    targetPosition.z = z - transform.position.z;
                }
            }
            else if (transform.position.z > z)
            {
                targetPosition.z = -speed * Time.deltaTime;
                if (targetPosition.z + transform.position.z < z)
                {
                    targetPosition.z = -(transform.position.z - z);
                }
            }
            else
            {
                targetPosition.z = 0;
            }

            transform.Translate(targetPosition);
            yield return null;
        }
        canMove = true;
    }

    bool IsBarrierInDirection(Vector3 direction)
    {
        RaycastHit hit;
        Vector3 origin = transform.position + Vector3.up * 0.5f; //adjust for cube height
        float distance = gridSize;

        //check if there's a barrier in the direction
        if (Physics.Raycast(origin, direction, out hit, distance, opaqueLayer))
        {
            return true; //true if theres barrrier ; flase if not
        }

        return false;
    }
}
