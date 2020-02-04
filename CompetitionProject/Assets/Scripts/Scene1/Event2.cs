using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Event2 : EventTemplate
{
    [SerializeField]
    private GameObject dragObject1;

    [SerializeField]
    private GameObject dragTarget1;
    private Bounds bound_dragTarget1;

    [SerializeField]
    private GameObject clickTarget2;

    [SerializeField]
    private PlayableDirector EventDirector;

    public override bool reactBeginDrag()
    {
        switch (phase)
        {
            case 1:
                dragTarget1.GetComponent<Animator>().Play("Active");
                break;
            case 3:
                dragTarget1.GetComponent<Animator>().Play("Active");
                break;
        }

        return base.reactBeginDrag();
    }

    public override bool reactEndDrag()
    {
        switch (phase)
        {
            case 1:
                dragTarget1.GetComponent<Animator>().Play("Idle");
                if (isIntersect(dragObject1.GetComponent<SpriteRenderer>().bounds, bound_dragTarget1))
                {
                    Debug.Log(dragObject1 + "Got Place: " + bound_dragTarget1 + phase);
                    dragObject1.GetComponent<DragCatcher>().truePlace(bound_dragTarget1.center);
                    nextPhase();
                    return true;
                }
                break;
        }

        return base.reactEndDrag();
    }

    public override bool reactClick()
    {
        switch (phase)
        {
            case 2:
                EventDirector.Play();
                clickTarget2.SetActive(false);
                break;
        }

        return base.reactClick();
    }

    protected override void onPhaseEnd()
    {
        switch (phase)
        {
            case 1:
                dragTarget1.SetActive(false);
                break;
            case 2:
                clickTarget2.SetActive(false);
                break;
        }
        base.onPhaseEnd();
    }

    protected override void onPhaseStart()
    {
        switch (phase)
        {
            case 1:
                dragTarget1.SetActive(true);
                break;
            case 2:
                clickTarget2.SetActive(true);
                clickTarget2.GetComponent<Animator>().Play("Active");
                break;
        }
        base.onPhaseStart();
    }

    // Start is called before the first frame update
    private void Start()
    {
        
        phase = 0;
        sumPhase = 3;
        bound_dragTarget1 = dragTarget1.GetComponent<SpriteRenderer>().bounds;

        dragTarget1.SetActive(false);
        clickTarget2.SetActive(false);

        nextPhase();
        Debug.Log(gameObject + "Phase: " + phase);
    }

    private void FixedUpdate()
    {
        switch (phase)
        {
            case 2:
                //When animate's playing is completed
                if (EventDirector.time == EventDirector.duration)
                {
                    dragObject1.GetComponent<DragCatcher>().truePlace(dragObject1.transform.position);
                    EventDirector.Stop();
                    nextPhase();
                }
                break;
        }
    }
}
