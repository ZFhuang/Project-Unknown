using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene02_scene03 : SaverTemplate
{
    public void show()
    {
        state = 1;
        Save();
        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        gameObject.SetActive(false);
        if (state == 1)
        {
            gameObject.SetActive(true);
        }
    }
}
