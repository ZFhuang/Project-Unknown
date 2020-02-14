using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private SaveAndLoad saver;
    private string sceneName;
    private Animator blackScreen;

    //react mouse down
    private void OnMouseDown()
    {
        Debug.Log("Loading scene: " + sceneName);
        blackScreen.gameObject.SetActive(true);
        StartCoroutine(changeScene());
    }

    //wait 0.5seconds that is the least time animate played
    private IEnumerator changeScene()
    {
        blackScreen.Play("BlackOut");
        saver = GameObject.Find("SaveAndLoad(Clone)").GetComponent<SaveAndLoad>();
        yield return new WaitForSeconds(.5f);
        blackScreen.Rebind();
        saver.sceneChange(sceneName);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    private void Awake()
    {
        sceneName = gameObject.name;
        blackScreen = GameObject.Find("BlackScreen").GetComponent<Animator>();
    }
}
