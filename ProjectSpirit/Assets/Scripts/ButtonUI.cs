using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class ButtonUI : MonoBehaviour
{
    public Button Title;
    public Button Quit;


    /* Made by Jonathan on 2D team.
     I dont wanna mess with any other script so this only quits the game when pressing the "X"
     button from the Into UI */


    public void IntroQuitButton()
    {
        Application.Quit();
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }
}
   