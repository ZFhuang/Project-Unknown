using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene01_Book : MonoBehaviour
{
    private int state;
    SaveAndLoad saver;

    private void OnMouseDown()
    {
        if (state == 0)
        {
            state = 1;
        }
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
        saver = GameObject.Find("SaveAndLoad(Clone)").GetComponent<SaveAndLoad>();

        Load();
        if (state == 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Save();
    }
}
