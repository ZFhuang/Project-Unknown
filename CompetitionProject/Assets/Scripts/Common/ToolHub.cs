using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolHub : MonoBehaviour
{
    bool isDown = false;
    [SerializeField] Animator toolHubAni;
    [SerializeField] Animator exHubAni;

    public void action()
    {
        if (isDown)
        {
            Debug.Log("UP");
            isDown = false;
            toolHubAni.SetBool("isDown", isDown);
            exHubAni.SetBool("isDown", isDown);
        }
        else
        {
            Debug.Log("DOWN");
            isDown = true;
            toolHubAni.SetBool("isDown", isDown);
            exHubAni.SetBool("isDown", isDown);
        }
    }
}
