using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene01_Hat : MonoBehaviour
{
    private int state;
    SaveAndLoad saver;
    Animator animator;

    public void fly()
    {
        if (state == 0)
        {
            state = 1;
            gameObject.GetComponent<PickupObject>().setCanPick(true);
            animator.SetInteger("state", state);
        }
    }

    private void OnMouseDown()
    {
        if (state == 1)
        {
            state = 2;
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
        animator = this.GetComponent<Animator>();

        Load();
        if (state == 2)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Save();
    }
}
