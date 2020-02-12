using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
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
        yield return new WaitForSeconds(.5f);
        blackScreen.Rebind();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    private void Start()
    {
        sceneName = gameObject.name;
        blackScreen = GameObject.Find("BlackScreen").GetComponent<Animator>();
    }
}
