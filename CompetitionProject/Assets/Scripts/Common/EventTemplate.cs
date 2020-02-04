using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTemplate : MonoBehaviour
{
    protected int phase;
    protected int sumPhase;

    public void nextPhase()
    {
        if (phase >= sumPhase)
        {
            endEvent();
        }
    }

    public void endEvent()
    {
        this.gameObject.SetActive(false);
    }

    public virtual bool reactBeginDrag()
    {
        return false;
    }

    public virtual bool reactEndDrag()
    {
        return false;
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
