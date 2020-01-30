using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void playGame()
    {
        //Load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quitGame()
    {
        Debug.Log("Quit!");
        //close the game while in editor
        //UnityEditor.EditorApplication.isPlaying = false;
        //quit
        Application.Quit();
    } 
}
