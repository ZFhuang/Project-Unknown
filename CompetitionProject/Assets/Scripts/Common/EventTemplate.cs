using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    [SerializeField]
    private GameObject dragObject1;

    [SerializeField]
    private GameObject dragTarget1;
    private Bounds bound_dragTarget1;

    private int phase=0;
    private int sumPhase=3;

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

    private void phase1()
    {
        
    }

    private void phase2()
    {

    }

    private void phase3()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        bound_dragTarget1 = dragTarget1.GetComponent<SpriteRenderer>().bounds;
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

    //Return if two bounds are intersected
    private bool isIntersect(Bounds A, Bounds B)
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
