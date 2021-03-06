﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Interface to draging things and move back things to its place
public class DragCatcher : MonoBehaviour
{
    enum PLATFORM
    {
        STANDALONE = 0,
        PHONE = 1
    }
    private PLATFORM platform = PLATFORM.PHONE;

    public float moveTime = 8f;

    public bool canDrag = true;

    [SerializeField]
    private int localOrder;
    [SerializeField]
    private EventTemplate EventController;

    private EventTrigger eventTrigger;
    private SpriteRenderer sprite;
    private Vector3 backPos;
    private Vector3 moveSpeed;
    private float moveLen;
    private bool isDraging=false;
    private bool isBacking=false;

    public void beginDragEvent()
    {
        //Call the eventTrigger attached
        if (eventTrigger != null)
            eventTrigger.OnBeginDrag(new PointerEventData(EventSystem.current));
        if (EventController != null)
            EventController.reactBeginDrag();
    }

    public void endDragEvent()
    {
        //Call the eventTrigger attached
        if (eventTrigger != null)
            eventTrigger.OnEndDrag(new PointerEventData(EventSystem.current));
        if (EventController != null && !EventController.reactEndDrag())
            falsePlace();
    }

    public void onDragEvent()
    {
        //Call the eventTrigger attached
        if (eventTrigger != null)
            eventTrigger.OnDrag(new PointerEventData(EventSystem.current));
    }

    public void truePlace(Vector3 pos)
    {
        isBacking = false;
        backPos = pos;
    }

    public void falsePlace()
    {
        isBacking = true;
        moveLen = (transform.position - backPos).sqrMagnitude;
    }

    // Start is called before the first frame update
    private void Start()
    {
        eventTrigger = this.GetComponent<EventTrigger>();
        sprite = this.GetComponent<SpriteRenderer>();
        backPos = this.transform.position;

        //Use preprocessor to get using platform
#if UNITY_STANDALONE
        platform = PLATFORM.STANDALONE;
        Debug.Log("UNITY_STANDALONE.STANDALONE");
#endif

#if UNITY_ANDROID
        platform = PLATFORM.PHONE;
#endif

#if UNITY_IPHONE
        platform = PLATFORM.PHONE;
        Debug.Log("UNITY_IPHONE.PHONE");
#endif

#if UNITY_EDITOR
        platform = PLATFORM.STANDALONE;
#endif
    }

    private void FixedUpdate()
    {
        if (localOrder == CameraController.ORDER&& canDrag)
        {
            if (platform == PLATFORM.STANDALONE)
            {
                //Get click mouse button down
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePoint.z = sprite.bounds.center.z;
                    if (sprite.bounds.Contains(mousePoint))
                    {
                        beginDragEvent();
                        CameraController.CANTRANS = false;
                        isDraging = true;
                    }
                }
                //Get click mouse button up
                if (isDraging == true && Input.GetMouseButtonUp(0))
                {
                    endDragEvent();
                    CameraController.CANTRANS = true;
                    isDraging = false;
                }
                //On draging
                if (isDraging == true && Input.GetMouseButton(0))
                {
                    Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePoint.z = sprite.bounds.center.z;
                    onDragEvent();
                    CameraController.CANTRANS = false;
                    this.transform.position = mousePoint;
                }
            }
            else
            {
                //Get touch began
                if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Vector3 clickPoint = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                    clickPoint.z = sprite.bounds.center.z;
                    if (sprite.bounds.Contains(clickPoint))
                    {
                        beginDragEvent();
                        CameraController.CANTRANS = false;
                        isDraging = true;
                    }
                }
                //Get touch ended
                if (isDraging == true && Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    endDragEvent();
                    CameraController.CANTRANS = true;
                    isDraging = false;
                }
                //On draging, perform badly, should find some way to get the real touch position in WorldPoint
                if (isDraging == true && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    onDragEvent();
                    Vector2 delta = Input.GetTouch(0).deltaPosition;
                    Vector3 newPos = new Vector3(transform.position.x + delta.x / 200, 
                        transform.position.y + delta.y / 200, transform.position.z);
                    transform.position = newPos;
                }
            }

            //Make the draging thing fly back to old pos
            if (isBacking)
            {
                Vector3 pos = Vector3.SmoothDamp(
                    transform.position,
                    new Vector3(backPos.x, backPos.y, this.transform.position.z),
                    ref moveSpeed, moveTime * Time.deltaTime);

                transform.position = pos;

                if ((transform.position - backPos).sqrMagnitude <= moveLen * 0.001f)
                {
                    transform.position = backPos;
                    isBacking = false;
                }
            }
        }
    }
}
