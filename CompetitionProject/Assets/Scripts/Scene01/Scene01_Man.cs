using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene01_Man : MonoBehaviour
{
    private int state;
    SaveAndLoad saver;

    private void OnMouseDown()
    {
        state = 1;
    }

    private void Save()
    {
        saver.saveState(gameObject.name, state);
    }

    private void Load()
    {
        state=saver.loadState(gameObject.name);
    }

    // Start is called before the first frame update
    private void Start()
    {
        saver = GameObject.Find("SaveAndLoad").GetComponent<SaveAndLoad>();
        Load();
        if (state > 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Save();
    }
}
