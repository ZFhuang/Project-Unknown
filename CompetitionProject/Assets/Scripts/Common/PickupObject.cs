using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    [SerializeField] private bool hasItem;
    [SerializeField] private bool isTool;
    [SerializeField] private bool canPick;
    [SerializeField] private bool needTool;
    [SerializeField] private string tool;
    [SerializeField] private bool hasTarget;
    [SerializeField] private GameObject targetObject;

    private bool isAnimating;
    private Vector3 _speed;
    private Vector3 _speed2;
    private Vector3 oldScale;
    private Vector3 oldTarget;
    private float _time = 0.3f;
    private float scale = 2f;
    private float waitTime = 0.7f;
    private ToolHub toolHub;
    private IllustrationMenu IlluMenu;

    public void setCanPick(bool input)
    {
        canPick = input;
    }

    public void PickUp()
    {
        StartCoroutine(pickAnimate());
    }

    private IEnumerator pickAnimate()
    {
        //Start animation and wait
        isAnimating = true;
        oldTarget = Camera.main.transform.position;
        yield return new WaitForSeconds(waitTime);
        isAnimating = false;
        addToTool();
        Destroy(gameObject);
    }

    private void addToTool()
    {
        if (isTool && toolHub != null)
        {
            toolHub.addObject(gameObject);
        }
        if (hasItem && IlluMenu != null)
        {
            if (IlluMenu.addObject(gameObject.name))
            {
                Debug.Log("Get new Illu");
            }
        }
    }

    private void OnMouseDown()
    {
        if (canPick)
        {
            if (needTool)
            {
                if(toolHub.getSelectingName() == tool)
                {
                    if (hasTarget&&targetObject!=null)
                    {
                        targetObject.SendMessage("pickUp", SendMessageOptions.DontRequireReceiver);
                    }
                    toolHub.useSelectingObject();
                    PickUp();
                }
            }
            else
            {
                if (hasTarget && targetObject != null)
                {
                    targetObject.SendMessage("pickUp", SendMessageOptions.DontRequireReceiver);
                }
                PickUp();
            }
        }
    }

    private void Start()
    {
        toolHub = GameObject.Find("ToolMenu").GetComponent<ToolHub>();
        IlluMenu = GameObject.Find("FuncButton").GetComponent<IllustrationMenu>();
        oldScale = gameObject.transform.localScale;
    }

    private void FixedUpdate()
    {
        if (isAnimating)
        {
            //Become larger and move to old camera position
            gameObject.transform.position =
                Vector3.SmoothDamp(gameObject.transform.position,
                oldTarget, ref _speed, _time);
            gameObject.transform.localScale =
                Vector3.SmoothDamp(gameObject.transform.localScale,
                oldScale * scale, ref _speed2, _time);
        }
    }
}
