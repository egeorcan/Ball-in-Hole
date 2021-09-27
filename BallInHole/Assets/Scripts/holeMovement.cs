using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holeMovement : MonoBehaviour
{
    [SerializeField]
    private float moveStep;

    public static bool isComplete;

    private Vector3 initPos;
    private Vector3 targetPos;
    private bool isMoving = false;

    Ray ray;
    RaycastHit hit;

    void Awake()
    {
        initPos = transform.position;
        targetPos = transform.position;
    }

    void Update()
    {
        isComplete = ballMovement.isComplete;
        checkDirection();
        moveHole();
    }

    public void checkDirection()
    {
        if (Input.GetKey(KeyCode.A) && !isMoving)
        {
            ray = new Ray(transform.position, Vector3.left);
            Physics.Raycast(ray, out hit);
            targetPos = new Vector3(transform.position.x - (hit.distance - 1f), transform.position.y, transform.position.z);
        }

        if (Input.GetKey(KeyCode.D) && !isMoving)
        {
            ray = new Ray(transform.position, Vector3.right);
            Physics.Raycast(ray, out hit);
            targetPos = new Vector3(transform.position.x + (hit.distance - 1f), transform.position.y, transform.position.z);
        }

        if (Input.GetKey(KeyCode.W) && !isMoving)
        {
            ray = new Ray(transform.position, Vector3.forward);
            Physics.Raycast(ray, out hit);
            targetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + (hit.distance - 1f));
        }

        if (Input.GetKey(KeyCode.S) && !isMoving)
        {
            ray = new Ray(transform.position, Vector3.back);
            Physics.Raycast(ray, out hit);
            targetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - (hit.distance - 1f));
        }
    }

    public void moveHole()
    {
        if (isComplete)
        {
            isMoving = true;
        }
        else if (transform.position == targetPos && !isComplete)
        {
            isMoving = false;
        }
        else if(ballMovement.isHit)
        {
            targetPos = initPos;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveStep * Time.deltaTime);
            isMoving = true;
        }
    }

}
