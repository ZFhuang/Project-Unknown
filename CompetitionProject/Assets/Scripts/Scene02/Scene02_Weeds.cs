using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene02_Weeds : SaverTemplate
{
    private Vector3 lastPos;

    private void rightDrag()
    {
        gameObject.transform.position = new Vector3(lastPos.x+1, lastPos.y, lastPos.z);
        if (state < 10)
        {
            state++;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void leftDrag()
    {
        gameObject.transform.position = new Vector3(lastPos.x - 1, lastPos.y, lastPos.z);
        if (state < 10)
        {
            state++;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected override void Start()
    {
        base.Start();

        lastPos = gameObject.transform.position;

        if (state >= 10)
        {
            Destroy(gameObject);
        }
    }
}
