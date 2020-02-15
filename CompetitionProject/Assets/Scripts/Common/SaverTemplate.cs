using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaverTemplate : MonoBehaviour
{
    protected int state;
    protected SaveAndLoad saver;

    protected virtual void Save()
    {
        saver.saveState(gameObject.name, state);
    }

    protected virtual void Load()
    {
        state = saver.loadState(gameObject.name);
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        saver = GameObject.Find("SaveAndLoad(Clone)").GetComponent<SaveAndLoad>();

        Load();
    }

    // Call on gameobject's destory
    protected virtual void OnDestroy()
    {
        Save();
    }
}
