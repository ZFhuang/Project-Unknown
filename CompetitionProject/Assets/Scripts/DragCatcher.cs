using System.Collections;
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

    private EventTrigger eventTrigger;
    [SerializeField]
    private EventSystem eventSystem;
    private SpriteRenderer sprite;
    private Vector3 storePos;
    private bool isDraging;

    public void beginDragEvent()
    {
        //Call the eventTrigger attached
        if (eventTrigger != null)
            eventTrigger.OnBeginDrag(new PointerEventData(eventSystem));
    }

    public void endDragEvent()
    {
        //Call the eventTrigger attached
        if (eventTrigger != null)
            eventTrigger.OnEndDrag(new PointerEventData(eventSystem));
    }

    public void onDragEvent()
    {
        //Call the eventTrigger attached
        if (eventTrigger != null)
            eventTrigger.OnDrag(new PointerEventData(eventSystem));
    }

    // Start is called before the first frame update
    private void Start()
    {
        eventTrigger = this.GetComponent<EventTrigger>();
        sprite = this.GetComponent<SpriteRenderer>();
        storePos = this.transform.position;

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
        if (platform == PLATFORM.STANDALONE)
        {
            //Get click mouse button down
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePoint.z = sprite.bounds.center.z;
                Debug.Log(mousePoint); 
                Debug.Log(sprite.bounds);
                if (sprite.bounds.Contains(mousePoint))
                {
                    beginDragEvent();
                    CameraController.canTrans = false;
                    isDraging = true;
                }
            }
            //Get click mouse button up
            if (Input.GetMouseButtonUp(0))
            {
                Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePoint.z = sprite.bounds.center.z;
                endDragEvent();
                CameraController.canTrans = true;
                isDraging = false;
            }
            //On draging
            if (isDraging == true)
            {
                if (Input.GetMouseButton(0))
                {
                    Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePoint.z = sprite.bounds.center.z;
                    onDragEvent();
                    CameraController.canTrans = false;
                    this.transform.position = mousePoint;
                }
            }
        }
        else
        {
            //Get touch began
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector3 clickPoint = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                clickPoint.z = sprite.bounds.center.z;
                if (sprite.bounds.Contains(clickPoint))
                {
                    beginDragEvent();
                    CameraController.canTrans = false;
                    isDraging = true;
                }
            }
            //Get touch ended
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Vector3 clickPoint = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                clickPoint.z = sprite.bounds.center.z;
                endDragEvent();
                CameraController.canTrans = true;
                isDraging = false;
            }
            //On draging
            if (isDraging == true)
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    Vector3 clickPoint = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                    clickPoint.z = sprite.bounds.center.z;
                    onDragEvent();
                    CameraController.canTrans = false;
                    this.transform.position = clickPoint;
                }
            }
        }
    }
}
