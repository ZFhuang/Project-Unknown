using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickCatcher : MonoBehaviour
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
    private Bounds spriteBounds;

    public void clickEvent()
    {
        Debug.Log(gameObject);
        eventTrigger.OnPointerClick(new PointerEventData(eventSystem));
    }

    // Start is called before the first frame update
    void Start()
    {
        eventTrigger = this.GetComponent<EventTrigger>();
        spriteBounds = this.GetComponent<SpriteRenderer>().bounds;

        //Use preprocessor to get using platform
#if UNITY_STANDALONE
        platform = PLATFORM.STANDALONE;
        Debug.Log("UNITY_STANDALONE.STANDALONE");
#endif

#if UNITY_ANDROID
        platform = PLATFORM.PHONE;
        Debug.Log("UNITY_ANDROID.PHONE");
#endif

#if UNITY_IPHONE
        platform = PLATFORM.PHONE;
        Debug.Log("UNITY_IPHONE.PHONE");
#endif

#if UNITY_EDITOR
        platform = PLATFORM.STANDALONE;
        Debug.Log("UNITY_EDITOR.STANDALONE");
#endif
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (platform == PLATFORM.STANDALONE)
        {
            //Get click
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePoint.z = spriteBounds.center.z;
                if (spriteBounds.Contains(mousePoint))
                {
                    clickEvent();
                }
            }
        }
        else
        {
            //Get touch
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector3 clickPoint = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                clickPoint.z = spriteBounds.center.z;
                if (spriteBounds.Contains(clickPoint))
                {
                    clickEvent();
                }
            }
        }
    }
}