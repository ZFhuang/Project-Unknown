using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene01_Man1 : SaverTemplate
{
    [SerializeField] GameObject scene02;
    Animator animator;
    ToolHub toolHub;

    public void pickUpBook()
    {
        if (state == 0)
        {
            state = 1;
            Save();
            animator.SetInteger("state", state);
        }
    }

    private void OnMouseDown()
    {
        if (toolHub.getSelectingName() == "Hat")
        {
            state = 2;
            Save();
            toolHub.useSelectingObject();
            scene02.GetComponent<Scene01_scene02>().show();
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        animator = this.GetComponent<Animator>();
        toolHub = GameObject.Find("ToolMenu").GetComponent<ToolHub>();

        if (state == 1)
        {
            animator.SetInteger("state", state);
        }

        if (state == 2)
        {
            Destroy(gameObject);
        }
    }
}
