using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Animator blackScreen;

    //react mouse down
    public void OnMouseDown()
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
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
