using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTemplate : MonoBehaviour
{
    //For call the first onPhaseStart, start phase number should be the real phase - 1
    protected int phase;
    //SumPhase is the numbers of maxPhase
    //for example, if there is only one phase, sumPhase will be 1
    protected int sumPhase;

    //This should be called by DragCatcher
    public virtual bool reactBeginDrag()
    {
        return false;
    }

    //This should be called by DragCatcher
    public virtual bool reactEndDrag()
    {
        return false;
    }

    //This should be called by DragCatcher
    public virtual bool reactDraging()
    {
        return false;
    }

    //This should be called by ClickCatcher
    public virtual bool reactClick()
    {
        return false;
    }

    /// <summary>
    ///A phase is running as below:
    ///Start() -> call nextPhase()
    ///onPhaseEnd()
    ///onPhaseBegin()
    ///mainPart
    ///call nextPhase()
    ///run... ->endEvent()
    /// </summary>

    //Call it when a phase ending sign is appear
    public void nextPhase()
    {
        onPhaseEnd();
        phase++;
        if (phase > sumPhase)
            endEvent();
        else
            onPhaseStart();
    }

    //It will be called before the phase number change
    protected virtual void onPhaseEnd()
    {
        Debug.Log(gameObject + "Phase end: " + phase);
    }

    //It will be called on the wake of the phase number changing
    protected virtual void onPhaseStart()
    {
        Debug.Log(gameObject + "Phase start: " + phase);
    }

    //It will be called at the end of the event, that is to say when "phase > sumPhase"
    protected virtual void endEvent()
    {
        this.gameObject.SetActive(false);
    }

    //Return if two bounds are intersected
    protected bool isIntersect(Bounds A, Bounds B)
    {
        Vector3 APos = A.center;
        Vector3 ASize = A.extents;
        Vector3 BPos = B.center;
        Vector3 BSize = B.extents;
        //Two bounder's max distance
        float halfSum_X = ASize.x + BSize.x;
        float halfSum_Y = ASize.y + BSize.y;
        //Their real distance
        float distance_X = Mathf.Abs(APos.x - BPos.x);
        float distance_Y = Mathf.Abs(APos.y - BPos.y);
        //If all small than max value, it means they are intersected
        if (distance_X <= halfSum_X && distance_Y <= halfSum_Y)
            return true;
        else
            return false;
    }
}
