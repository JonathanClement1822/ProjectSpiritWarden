using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour
{
    public Button Start;
    public Button Quit;
    public Animator transition;
    public float TransitionTime = 1f;

    // Update is called once per frame
    public void StartButton()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    IEnumerator LoadLevel(int LevelIndex)
    {
        //play animation
        transition.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(TransitionTime);

        //load scene

        SceneManager.LoadScene(LevelIndex);
    }

}
