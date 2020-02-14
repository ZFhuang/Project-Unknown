using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Use this with EventTrigger.PointerClick
public class ClickCatcher : MonoBehaviour
{
    private EventTrigger eventTrigger;

    public void clickEvent()
    {
        //Call the eventTrigger attached
        if (eventTrigger != null)
            eventTrigger.OnPointerClick(new PointerEventData(EventSystem.current));
    }

    // Start is called before the first frame update
    private void Start()
    {
        eventTrigger = this.GetComponent<EventTrigger>();
    }

    private void OnMouseDown()
    {
        clickEvent();
    }
}