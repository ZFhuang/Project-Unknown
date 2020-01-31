using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubScene : MonoBehaviour
{
    //State machine settings
    enum STATE
    {
        IDLE = 0,
        IN = 1,
        OUT = 2
    }
    private STATE state = STATE.IDLE;

    [SerializeField]
    private SpriteRenderer subForeground;
    [SerializeField]
    private SpriteRenderer subBackground;
    private float oldAlpha_fore;
    private float oldAlpha_back;
    private float alphaSpeed = 0f;
    private float newAlpha_fore = 0f;
    private float newAlpha_back = 1f;

    public float alphaTime = 20f;

    //Call from the corresponding event trigger
    public void sceneIn()
    {
        state = STATE.IN;
    }

    //Using Transform.BroadcastMessage to call this function
    public void sceneOut()
    {
        state = STATE.OUT;
    }

    private void Start()
    {
        //Set init color
        oldAlpha_fore = subForeground.color.a;
        oldAlpha_back = subBackground.color.a;
    }

    private void FixedUpdate()
    {
        if (state == STATE.IDLE)
        {
            //Do nothing
            ;
        }
        //Using SmoothDamp below to smooth change the value of color's alpha
        else if (state == STATE.IN)
        {
            //damp subBackground to 1 and subForeground 0
            subBackground.color = new
                Color(subBackground.color.r, subBackground.color.g, subBackground.color.b,
                Mathf.SmoothDamp(subBackground.color.a, newAlpha_back,
                ref alphaSpeed, alphaTime * Time.deltaTime));
            subForeground.color = new
                Color(subForeground.color.r, subForeground.color.g, subForeground.color.b,
                Mathf.SmoothDamp(subForeground.color.a, newAlpha_fore, 
                ref alphaSpeed, alphaTime * Time.deltaTime));
            if (Mathf.Abs(subBackground.color.a - newAlpha_back) <= newAlpha_back * 0.01f)
            {
                subForeground.color = new
                Color(subBackground.color.r, subBackground.color.g, subBackground.color.b, newAlpha_back);
                subForeground.color = new
                Color(subForeground.color.r, subForeground.color.g, subForeground.color.b, newAlpha_fore);
                state = STATE.IDLE;
            }
        }
        else if (state == STATE.OUT)
        {
            //damp subBackground and subForeground back to starting condition
            subBackground.color = new
                Color(subBackground.color.r, subBackground.color.g, subBackground.color.b,
                Mathf.SmoothDamp(subBackground.color.a, oldAlpha_back,
                ref alphaSpeed, alphaTime * Time.deltaTime));
            subForeground.color = new
                Color(subForeground.color.r, subForeground.color.g, subForeground.color.b,
                Mathf.SmoothDamp(subForeground.color.a, oldAlpha_fore, 
                ref alphaSpeed, alphaTime * Time.deltaTime));
            if (Mathf.Abs(subBackground.color.a - oldAlpha_back) <= oldAlpha_back * 0.01f)
            {
                subForeground.color = new
                Color(subBackground.color.r, subBackground.color.g, subBackground.color.b, oldAlpha_back);
                subForeground.color = new
                Color(subForeground.color.r, subForeground.color.g, subForeground.color.b, oldAlpha_fore);
                state = STATE.IDLE;
            }
        }
    }
}