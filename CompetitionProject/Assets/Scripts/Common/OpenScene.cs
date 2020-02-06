using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class OpenScene : MonoBehaviour
{
    [SerializeField]
    private Button skipButton;
    //[SerializeField]
    //private PlayableDirector director;

    private Animation skipButtonShow;

    public void nextScene()
    {
        //Load next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Start()
    {
        skipButtonShow = skipButton.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        //Click anywhere to active the skip button
        if (Input.GetMouseButtonDown(0))
        {
            skipButton.gameObject.SetActive(true);
            skipButtonShow.Rewind("SkipButtonShow");
            skipButtonShow.Play("SkipButtonShow");

        }

        //Check if the animation is played
        if (skipButton.IsActive() &&
            Mathf.Abs(skipButtonShow["SkipButtonShow"].normalizedTime - 
            skipButtonShow["SkipButtonShow"].length) <= 
            skipButtonShow["SkipButtonShow"].length * 0.01)
        {
            skipButton.gameObject.SetActive(false);
        }
    }
}
