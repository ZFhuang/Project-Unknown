using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideObject : MonoBehaviour
{
    [SerializeField] private float xRange;
    [SerializeField] private float yRange;
    [SerializeField] private bool xDrag;
    [SerializeField] private bool yDrag;
    [SerializeField] private bool hasTarget;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private bool needTool;
    [SerializeField] private string tool;

    private bool leftDrag;
    private bool rightDrag;
    private bool upDrag;
    private bool downDrag;
    private ToolHub toolHub;

    private void OnMouseDrag()
    {
        if (needTool)
        {
            if (toolHub.getSelectingName() == tool)
            {
                drag();
            }
        }
        else
        {
            drag();
        }
    }

    private void drag()
    {
        if (xDrag)
        {
            if (!rightDrag && Input.touchCount > 0 && Input.GetTouch(0).deltaPosition.x > xRange)
            {
                rightDrag = true;
                leftDrag = false;
                if (hasTarget && targetObject != null)
                    targetObject.SendMessage("rightDrag", SendMessageOptions.DontRequireReceiver);
                Debug.Log("rightDrag");
            }
            if (!leftDrag && Input.touchCount > 0 && Input.GetTouch(0).deltaPosition.x < xRange)
            {
                leftDrag = true;
                rightDrag = false;
                if (hasTarget && targetObject != null)
                    targetObject.SendMessage("leftDrag", SendMessageOptions.DontRequireReceiver);
                Debug.Log("leftDrag");
            }
        }
        if (yDrag)
        {
            if (!upDrag && Input.touchCount > 0 && Input.GetTouch(0).deltaPosition.y > yRange)
            {
                upDrag = true;
                downDrag = false;
                if (hasTarget && targetObject != null)
                    targetObject.SendMessage("upDrag", SendMessageOptions.DontRequireReceiver);
                Debug.Log("upDrag");
            }
            if (!downDrag && Input.touchCount > 0 && Input.GetTouch(0).deltaPosition.y < yRange)
            {
                downDrag = true;
                upDrag = false;
                if (hasTarget && targetObject != null)
                    targetObject.SendMessage("downDrag", SendMessageOptions.DontRequireReceiver);
                Debug.Log("downDrag");
            }
        }
    }

    private void Start()
    {
        toolHub = GameObject.Find("ToolMenu").GetComponent<ToolHub>();
    }
}
