using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SaveAndLoad saver;
    private Animator blackScreen;

    public void playGame()
    {
        //Load the target scene
        blackScreen.gameObject.SetActive(true);
        StartCoroutine(changeScene());
    }

    private IEnumerator changeScene()
    {
        blackScreen.Play("BlackOut");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(saver.getSceneName(), LoadSceneMode.Single);
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

    private void Start()
    {
        blackScreen = GameObject.Find("BlackScreen").GetComponent<Animator>();
    }
}
