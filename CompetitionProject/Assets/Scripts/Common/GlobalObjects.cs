using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalObjects : MonoBehaviour
{
    //Should use prefabs
    [SerializeField] private GameObject prefab_MainCamera;
    [SerializeField] private GameObject prefab_MainEventSystem;
    [SerializeField] private GameObject prefab_MainAudio;
    [SerializeField] private GameObject prefab_UI;

    private GameObject g;

    //Instantiate these global things
    void Awake()
    {
        g = GameObject.Find("Main Camera(Clone)");
        if (g == null)
        {
            g=Instantiate(prefab_MainCamera);
        }
        //Reset the position
        g.transform.position = new Vector3(0, 0, g.transform.position.z);
        DontDestroyOnLoad(g);

        g = GameObject.Find("Main EventSystem(Clone)");
        if (g == null)
        {
            g = Instantiate(prefab_MainEventSystem);
        }
        DontDestroyOnLoad(g);

        g = GameObject.Find("Main Audio(Clone)");
        if (g == null)
        {
            g = Instantiate(prefab_MainAudio);
        }
        DontDestroyOnLoad(g);

        g = GameObject.Find("UI(Clone)");
        if (g == null)
        {
            g = Instantiate(prefab_UI);
        }
        //Set the render camera
        g.GetComponent<Canvas>().worldCamera=Camera.main;
        DontDestroyOnLoad(g);
    }
}
