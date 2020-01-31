using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    //Pause the game by setting the timeScale to 0
    //This will make difference to FixedUpdate
    public void pauseGame()
    {
        Time.timeScale = 0f;
    }

    //Resume the game by setting the timeScale to 1
    public void resumeGame()
    {
        Time.timeScale = 1f;
    }

    public void quitGame()
    {
        Debug.Log("Quit!");
#if UNITY_EDITOR
        //close the game while in editor
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        //quit
        Application.Quit();
    }
}
