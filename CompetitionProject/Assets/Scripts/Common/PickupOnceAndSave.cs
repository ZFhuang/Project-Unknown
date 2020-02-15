using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupOnceAndSave : SaverTemplate
{
    private void pickUp()
    {
        if (state == 0)
        {
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
