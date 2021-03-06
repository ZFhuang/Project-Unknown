﻿using System.Collections;
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
    private Vector3 startPos;

    enum PLATFORM
    {
        STANDALONE = 0,
        PHONE = 1
    }
    private PLATFORM platform;

    private void OnMouseDown()
    {
        startPos = Input.mousePosition;
        Debug.Log(startPos);
    }

    private void OnMouseDrag()
    {
        if (needTool)
        {
            if (toolHub.getSelectingName() == tool)
            {
                if (platform == PLATFORM.STANDALONE)
                {
                    //For Windows, Linux, OSX
                    drag_STANDALONE();
                }
                else
                {
                    //Temporarily for Android and iOS
                    drag_PHONE();
                }
            }
        }
        else
        {
            if (platform == PLATFORM.STANDALONE)
            {
                //For Windows, Linux, OSX
                drag_STANDALONE();
            }
            else
            {
                //Temporarily for Android and iOS
                drag_PHONE();
            }
        }
    }

    private void drag_STANDALONE()
    {
        if (xDrag)
        {
            if (!rightDrag && Mathf.Abs(Input.mousePosition.x - startPos.x) > xRange)
            {
                rightDrag = true;
                leftDrag = false;
                if (hasTarget && targetObject != null)
                    targetObject.SendMessage("rightDrag", SendMessageOptions.DontRequireReceiver);
                Debug.Log("rightDrag");
            }
            if (!leftDrag && Mathf.Abs(Input.mousePosition.x - startPos.x) < xRange)
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
            if (!upDrag && Mathf.Abs(Input.mousePosition.y - startPos.y) > yRange)
            {
                upDrag = true;
                downDrag = false;
                if (hasTarget && targetObject != null)
                    targetObject.SendMessage("upDrag", SendMessageOptions.DontRequireReceiver);
                Debug.Log("upDrag");
            }
            if (!downDrag && Mathf.Abs(Input.mousePosition.y - startPos.y) < yRange)
            {
                downDrag = true;
                upDrag = false;
                if (hasTarget && targetObject != null)
                    targetObject.SendMessage("downDrag", SendMessageOptions.DontRequireReceiver);
                Debug.Log("downDrag");
            }
        }
    }

    private void drag_PHONE()
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

#if UNITY_STANDALONE
        platform = PLATFORM.STANDALONE;
        Debug.Log("UNITY_STANDALONE.STANDALONE");
#endif

#if UNITY_ANDROID
        platform = PLATFORM.PHONE;
        //Debug.Log("UNITY_ANDROID.PHONE");
#endif

#if UNITY_IPHONE
        platform = PLATFORM.PHONE;
        Debug.Log("UNITY_IPHONE.PHONE");
#endif

#if UNITY_EDITOR
        platform = PLATFORM.STANDALONE;
        //Debug.Log("UNITY_EDITOR.STANDALONE");
#endif
    }
}
