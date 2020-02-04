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
    private PlayableDirector EventDirector;

    private new int phase = 1;
    private new int sumPhase = 1;

    public override bool reactEndDrag()
    {
        if (phase == 1)
        {
            if (isIntersect(dragObject1.GetComponent<SpriteRenderer>().bounds, bound_dragTarget1))
            {
                Debug.Log(dragObject1 + "Got Place: " + phase);
                dragObject1.GetComponent<DragCatcher>().truePlace(bound_dragTarget1.center);
                dragTarget1.SetActive(false);
                nextPhase();
                return true;
            }
        }

        return base.reactEndDrag();
    }

    private void phase1()
    {
        if (dragObject1.GetComponent<DragCatcher>().isDraging)
        {
            //dragTarget1 do something here
        }
    }

    private void phase2()
    {
        ;
    }

    private void phase3()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        bound_dragTarget1 = dragTarget1.GetComponent<SpriteRenderer>().bounds;
        Debug.Log(gameObject + "Phase: " + phase);
    }

    void FixedUpdate()
    {
        switch (phase)
        {
            case 1:
                phase1();
                break;
            case 2:
                phase2();
                break;
            case 3:
                phase3();
                break;
            default:
                endEvent();
                break;
        }
    }
}
