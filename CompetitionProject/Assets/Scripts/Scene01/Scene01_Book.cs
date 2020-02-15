using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene01_Book : SaverTemplate
{
    private void pickUp()
    {
        if (state == 0)
        {
            Debug.Log("Get!");
            state = 1;
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        if (state == 1)
        {
            Destroy(gameObject);
        }
    }
}
