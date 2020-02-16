using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene02_Weeds : SaverTemplate
{
    [SerializeField] Scene02_scene03 scene03;

    private Vector3 lastPos;
    private ToolHub toolHub;

    private void rightDrag()
    {
        gameObject.transform.position = new Vector3(lastPos.x + 0.5f, lastPos.y, lastPos.z);
        if (state < 10)
        {
            state++;
        }
        else
        {
            end();
        }
    }

    private void leftDrag()
    {
        gameObject.transform.position = new Vector3(lastPos.x - 0.5f, lastPos.y, lastPos.z);
        if (state < 10)
        {
            state++;
        }
        else
        {
            end();
        }
    }

    private void end()
    {
        Save();
        toolHub.useObjectByName("Sickle");
        scene03.show();
        Destroy(gameObject);
    }

    protected override void Start()
    {
        base.Start();

        lastPos = gameObject.transform.position;
        toolHub = GameObject.Find("ToolMenu").GetComponent<ToolHub>();

        if (state >= 10)
        {
            end();
        }
    }
}
