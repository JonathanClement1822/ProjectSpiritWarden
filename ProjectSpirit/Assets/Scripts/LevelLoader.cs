using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float TransitionTime = 1f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
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
