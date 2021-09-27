using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballTransition : MonoBehaviour
{
    public static bool finishedTransition = false;

    [SerializeField]
    private float transitionSpeed;

    void Update()
    {
         transform.position += Vector3.down * transitionSpeed * Time.deltaTime;
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("TransitionEnd"))
        {
            finishedTransition = true;
            Destroy(gameObject, 2f);
        }
    }
}
