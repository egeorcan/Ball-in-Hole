using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelLoader : MonoBehaviour
{
    public Animator transition;

    private static int nextLevel;

    void Update()
    {
        if (ballMovement.isComplete)
        {
            nextLevel = (SceneManager.GetActiveScene().buildIndex) + 1;
            StartCoroutine(LoadLevel(0));            
        }
        if (ballTransition.finishedTransition)
        {
            StartCoroutine(LoadLevel(nextLevel));
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("finished");

        yield return new WaitForSeconds(1.5f);

        ballMovement.isComplete = false;
        ballTransition.finishedTransition = false;

        SceneManager.LoadScene(levelIndex);
    }
}
