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
    private GameObject dragTarget3;
    private Bounds bound_dragTarget3;

    public override bool reactBeginDrag()
    {
        switch (phase)
        {
            //Two sign animated
            case 1:
                dragTarget1.GetComponent<Animator>().Play("Active");
                break;
            case 3:
                dragTarget3.GetComponent<Animator>().Play("Active");
                break;
        }

        return base.reactBeginDrag();
    }

    public override bool reactEndDrag()
    {
        //Because the animator is set "ApplyRootMotion", thus it should be reset rotation after played
        dragObject1.GetComponent<Animator>().Play("Idle");
        dragObject1.transform.eulerAngles = new Vector3(0, 0, 0);
        switch (phase)
        {
            //Check if the object collided the target
            case 1:
                dragTarget1.GetComponent<Animator>().Play("Idle");
                if (isIntersect(dragObject1.GetComponent<SpriteRenderer>().bounds, bound_dragTarget1))
                {
                    Debug.Log(dragObject1 + "Got Place: " + bound_dragTarget1 + phase);
                    dragObject1.transform.position = bound_dragTarget1.center;
                    dragObject1.GetComponent<DragCatcher>().truePlace(bound_dragTarget1.center);
                    nextPhase();
                    return true;
                }
                break;
            case 3:
                dragTarget3.GetComponent<Animator>().Play("Idle");
                if (isIntersect(dragObject1.GetComponent<SpriteRenderer>().bounds, bound_dragTarget3))
                {
                    Debug.Log(dragObject1 + "Got Place: " + bound_dragTarget3 + phase);
                    dragObject1.transform.position = bound_dragTarget3.center;
                    dragObject1.GetComponent<DragCatcher>().truePlace(bound_dragTarget3.center);
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
            //Start the moving animation
            case 2:
                dragObject1.GetComponent<Animator>().Play("TesterMove1");
                dragObject1.GetComponent<DragCatcher>().canDrag = false;
                clickTarget2.SetActive(false);
                Debug.Log(dragObject1 + ": " + dragObject1.transform.position);
                Debug.Log(clickTarget2 + ": " + clickTarget2.GetComponent<SpriteRenderer>().bounds.center);
                break;
        }

        return base.reactClick();
    }

    protected override void onPhaseEnd()
    {
        switch (phase)
        {
            case 1:
                //Close the dragTarget1
                dragTarget1.SetActive(false);
                break;
            case 2:
                //Set the true position of dragObject1 after animated
                Debug.Log(dragObject1 + ": " + dragObject1.transform.localPosition);
                Debug.Log(clickTarget2 + ": " + clickTarget2.transform.localPosition);
                dragObject1.transform.position = clickTarget2.transform.position;
                dragObject1.GetComponent<DragCatcher>().canDrag = true;
                dragObject1.GetComponent<DragCatcher>().truePlace(clickTarget2.GetComponent<SpriteRenderer>().bounds.center);
                break;
            case 3:
                //Close the dragTarget3
                dragTarget3.SetActive(false);
                break;
        }
        base.onPhaseEnd();
    }

    protected override void onPhaseStart()
    {
        switch (phase)
        {
            case 1:
                //Open the dragTarget1
                dragTarget1.SetActive(true);
                break;
            case 2:
                //Start the click sign
                clickTarget2.SetActive(true);
                clickTarget2.GetComponent<Animator>().Play("Active");
                break;
            case 3:
                //Open the dragTarget1
                dragTarget3.SetActive(true);
                break;
        }
        base.onPhaseStart();
    }

    protected override void endEvent()
    {
        dragObject1.GetComponent<DragCatcher>().canDrag = false;
        base.endEvent();
    }

    // Start is called before the first frame update
    private void Start()
    {
        //Init
        phase = 0;
        sumPhase = 3;
        bound_dragTarget1 = dragTarget1.GetComponent<SpriteRenderer>().bounds;
        bound_dragTarget3 = dragTarget3.GetComponent<SpriteRenderer>().bounds;

        dragTarget1.SetActive(false);
        clickTarget2.SetActive(false);
        dragTarget3.SetActive(false);

        nextPhase();
        Debug.Log(gameObject + "Phase: " + phase);
    }

    private void FixedUpdate()
    {
        switch (phase)
        {
            case 2:
                AnimatorStateInfo animatorInfo = dragObject1.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
                //True when TesterMove1's play is over
                if ((animatorInfo.normalizedTime >= 1.0f) && (animatorInfo.IsName("TesterMove1")))
                {
                    nextPhase();
                }
                break;
        }
    }
}
