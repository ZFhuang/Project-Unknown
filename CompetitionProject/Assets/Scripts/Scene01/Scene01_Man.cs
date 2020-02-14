using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene01_Man : MonoBehaviour
{
    private int state;
    SaveAndLoad saver;
    ToolHub toolHub;

    private void OnMouseDown()
    {
        if (state == 0 && toolHub.getSelectingName() == "Book")
        {
            toolHub.useSelectingObject();
            state = 1;
            Destroy(gameObject);
        }
    }

    private void Save()
    {
        saver.saveState(gameObject.name, state);
    }

    private void Load()
    {
        state=saver.loadState(gameObject.name);
        Debug.Log(gameObject.name + " state: " + state);
    }

    // Start is called before the first frame update
    private void Start()
    {
        saver = GameObject.Find("SaveAndLoad(Clone)").GetComponent<SaveAndLoad>();
        toolHub = GameObject.Find("ToolMenu").GetComponent<ToolHub>();
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
