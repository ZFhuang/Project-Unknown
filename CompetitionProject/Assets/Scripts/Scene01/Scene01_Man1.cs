using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene01_Man1 : MonoBehaviour
{
    [SerializeField] GameObject scene02;
    private int state;
    SaveAndLoad saver;
    ToolHub toolHub;
    Animator animator;

    public void pickUpBook()
    {
        if (state == 0)
        {
            state = 1;
            animator.SetInteger("state", state);
        }
    }

    private void OnMouseDown()
    {
        if (toolHub.getSelectingName() == "Hat")
        {
            toolHub.useSelectingObject();
            state = 2;
            scene02.GetComponent<Scene01_scene02>().show();
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
        animator = this.GetComponent<Animator>();
        Load();
        if (state ==2)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Save();
    }
}
