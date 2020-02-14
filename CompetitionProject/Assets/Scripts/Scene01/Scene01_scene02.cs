using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene01_scene02 : MonoBehaviour
{
    private int state;
    SaveAndLoad saver;

    public void show()
    {
        state = 1;
        gameObject.SetActive(true);
    }

    private void Save()
    {
        saver.saveState(gameObject.name, state);
    }

    private void Load()
    {
        state = saver.loadState(gameObject.name);
        Debug.Log(gameObject.name + " state: " + state);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        saver = GameObject.Find("SaveAndLoad(Clone)").GetComponent<SaveAndLoad>();
        Load();
        if (state == 1)
        {
            gameObject.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        Save();
    }
}
