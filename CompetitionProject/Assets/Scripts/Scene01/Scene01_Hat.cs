using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene01_Hat : SaverTemplate
{
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

    private void pickUp()
    {
        if (state == 1)
        {
            state = 2;
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        animator = this.GetComponent<Animator>();

        if (state == 2)
        {
            Destroy(gameObject);
        }
    }
}
