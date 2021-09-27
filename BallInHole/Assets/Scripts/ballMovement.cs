using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballMovement : MonoBehaviour
{
    public Animator screenFlash;
    
    [SerializeField]
    private float moveStep;

    public static bool isComplete;
    public static bool isHit;
    public static bool isMoving;

    private Vector3 initPos;
    private Vector3 targetPos;

    Ray ray;
    RaycastHit hit;

    void Awake()
    {
        isComplete = false;

        initPos = new Vector3(transform.position.x, 1, transform.position.z);
        targetPos = initPos;
    }

    void Update()
    {
        checkDirection();
        moveBall();
    }

    public void checkDirection()
    {
        if (Input.GetKey(KeyCode.A) && !isMoving)
        {
            ray = new Ray(transform.position, Vector3.left);
            Physics.Raycast(ray, out hit);
            if (hit.transform.CompareTag("Hazard")) targetPos = hit.transform.position;
            else targetPos = new Vector3(transform.position.x - (hit.distance - 1f), transform.position.y, transform.position.z);
        }

        if (Input.GetKey(KeyCode.D) && !isMoving)
        {
            ray = new Ray(transform.position, Vector3.right);
            Physics.Raycast(ray, out hit);

            if (hit.transform.CompareTag("Hazard")) targetPos = hit.transform.position;
            else targetPos = new Vector3(transform.position.x + (hit.distance - 1f), transform.position.y, transform.position.z);
        }

        if (Input.GetKey(KeyCode.W) && !isMoving)
        {
            ray = new Ray(transform.position, Vector3.forward);
            Physics.Raycast(ray, out hit);

            if (hit.transform.CompareTag("Hazard")) targetPos = hit.transform.position;
            else targetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + (hit.distance - 1f));
        }

        if (Input.GetKey(KeyCode.S) && !isMoving)
        {
            ray = new Ray(transform.position, Vector3.back);
            Physics.Raycast(ray, out hit);

            if (hit.transform.CompareTag("Hazard")) targetPos = hit.transform.position;
            else targetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - (hit.distance - 1f));
        }
    }

    public void moveBall()
    {
        if (isComplete)
        {
            transform.position += Vector3.down * 2f * Time.deltaTime;
            isMoving = true;
        }
        else if (transform.position == targetPos && !isComplete)
        {
            isHit = false;
            isMoving = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveStep * Time.deltaTime);
            isMoving = true;
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Hole")) isComplete = true;

        if (collision.gameObject.CompareTag("Hazard"))
        {
            screenFlash.SetTrigger("Hit");
            isHit = true;            
            targetPos = initPos;
        }
    }

}
