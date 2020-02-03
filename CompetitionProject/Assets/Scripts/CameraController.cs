using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static bool canTrans = true;

    enum PLATFORM
    {
        STANDALONE = 0,
        PHONE = 1
    }
    private PLATFORM platform = PLATFORM.PHONE;

    //Camera's state machine settings
    enum STATE
    {
        SLIDE = 0,
        TOUCH = 1,
        MOVE = 2,
        ZOOM = 3,
        MOVE_ZOOM = 4,
        IDLE = 5
    }
    private STATE camState = STATE.IDLE;

    private bool mousePressing = false;
    //Camera move speed
    private float speed;
    private Camera mCam;
    private float zoomSpeed = 0f;
    private float zoomScale_old = 5f;
    private float zoomScale_to;
    private Vector3 moveSpeed;
    private Vector3 lastPosition;
    private SpriteRenderer lastBounder;
    private Vector3 targetPosition;
    //Camera deadzone settings
    private float minHor;
    private float maxHor;
    private float minVer;
    private float maxVer;
    [SerializeField]
    private SpriteRenderer backgoundBounder;

    public float zoomTime = 20f;
    public float zoomScale = 2f;

    //Load options or reload on resume
    public void loadCamSettings()
    {
        if (!PlayerPrefs.HasKey("speed"))
        {
            PlayerPrefs.SetFloat("speed", 0.5f);
            speed = 0.5f;
            Debug.Log(this.gameObject + " init speed: " + speed);
        }
        else if (PlayerPrefs.HasKey("speed"))
        {
            speed = PlayerPrefs.GetFloat("speed");
            Debug.Log(this.gameObject + " load speed: " + speed);
        }
    }

    //Refresh camera deadzone to ensure player control the camera rightly
    public void refreshDeadzone(SpriteRenderer input, float camSize)
    {
        //Using aspectRation to caculate the horizon width
        float aspectRatio = Screen.width * 1.0f / Screen.height;

        minHor = input.transform.position.x - input.bounds.size.x / 2 + camSize * aspectRatio;
        maxHor = input.transform.position.x + input.bounds.size.x / 2 - camSize * aspectRatio;
        minVer = input.transform.position.y - input.bounds.size.y / 2 + camSize;
        maxVer = input.transform.position.y + input.bounds.size.y / 2 - camSize;

        //Restrict the bounds
        if (minHor > maxHor)
        {
            minHor = input.transform.position.x;
            maxHor = input.transform.position.x;
        }
        if (minVer > maxVer)
        {
            minVer = input.transform.position.y;
            maxVer = input.transform.position.y;
        }
    }

    //Transwrite of cameraIn
    public void cameraOut()
    {
        targetPosition = lastPosition;
        zoomScale_to = zoomScale_old;
        camState = STATE.MOVE_ZOOM;
        mousePressing = false;
        backgoundBounder = lastBounder;
        //When zooming out, camSize should bet set at first ensuring not leading a flash change
        refreshDeadzone(backgoundBounder, zoomScale_to);
    }

    //Change the state to zoom in FixedUpdate
    public void cameraIn(GameObject target)
    {
        lastPosition = mCam.gameObject.transform.position;
        lastBounder = backgoundBounder;
        backgoundBounder = target.GetComponent<SpriteRenderer>();
        targetPosition = target.gameObject.transform.position;
        zoomScale_to = zoomScale;
        camState = STATE.MOVE_ZOOM;
        mousePressing = false;
    }

    // Start is called before the first frame update
    private void Start()
    {
        mCam = Camera.main;
        //Init options
        loadCamSettings();
        //Init deadzone
        refreshDeadzone(backgoundBounder, mCam.orthographicSize);

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

    //Using FixedUpdate to make sure the game will stop when timescale = 0
    private void FixedUpdate()
    {
        //Camera idle
        if (camState == STATE.IDLE || camState == STATE.SLIDE)
        {
            //Choose capable moving method
            if (platform == PLATFORM.STANDALONE)
            {
                //For Windows, Linux, OSX
                cameraTranlate_STANDALONE();
            }
            else
            {
                //Temporarily for Android and iOS
                cameraTranlate_PHONE();
            }
        }
        //Camera move and zoom
        //Using SmoothDamp to smoothly changing the values
        else if (camState == STATE.MOVE_ZOOM)
        {
            mCam.orthographicSize = Mathf.SmoothDamp(
                mCam.orthographicSize, zoomScale_to, ref zoomSpeed, zoomTime * Time.deltaTime);
            Vector3 pos = Vector3.SmoothDamp(
                mCam.transform.position,
                new Vector3(targetPosition.x, targetPosition.y, mCam.transform.position.z),
                ref moveSpeed, zoomTime * Time.deltaTime);

            cameraPositionTrans(pos.x, pos.y);

            if (Mathf.Abs(mCam.orthographicSize - zoomScale_to) <= zoomScale_to * 0.001f)
            {
                mCam.orthographicSize = zoomScale_to;
                cameraPositionTrans(targetPosition.x, targetPosition.y);
                //When complete zooming, we should refresh the zone to adapt the new bound
                refreshDeadzone(backgoundBounder, zoomScale_to);
                camState = STATE.IDLE;
                mousePressing = false;
            }
        }

        //Camera slide

        //Camera zoom

        //Camera move
    }

    //Camera moving method for Windows, Linux, OSX
    private void cameraTranlate_STANDALONE()
    {
        //On mouse down
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Mouse Left DOWN!");
            mousePressing = true;
        }
        //On mouse up
        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log("Mouse Left UP!");
            mousePressing = false;
        }
        //On mouse pressing
        if (mousePressing)
        {
            Vector3 p = mCam.transform.position;
            //Move X
            Vector3 p1 = p - mCam.transform.right *
                Input.GetAxis("Mouse X") * speed * Time.timeScale * 0.5f *
                (mCam.orthographicSize / 5f);
            //Move Y
            Vector3 p2 = p1 - mCam.transform.up *
                Input.GetAxis("Mouse Y") * speed * Time.timeScale * 0.5f *
                (mCam.orthographicSize / 5f);
            //Set transform
            cameraPositionTrans(p2.x, p2.y);
        }
    }

    //Camera moving method for Android and iOS
    private void cameraTranlate_PHONE()
    {
        //Touch and drag
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 delta = Input.GetTouch(0).deltaPosition;
            //Set transform
            //Devide by (mCam.orthographicSize / 5f) to make sure the speed are same
            //when changing the camera size
            cameraPositionTrans(
                mCam.transform.position.x
                - delta.x * speed / 50f * (mCam.orthographicSize / 5f),
                mCam.transform.position.x
                - delta.x * speed / 50f * (mCam.orthographicSize / 5f));
        }
    }

    //Pack the camera position translate function to restrict the position
    private void cameraPositionTrans(float x, float y)
    {
        if (canTrans)
        {

            if (x < minHor)
                x = minHor;
            if (x > maxHor)
                x = maxHor;
            if (y < minVer)
                y = minVer;
            if (y > maxVer)
                y = maxVer;

            //Position change here
            mCam.transform.position = new Vector3(x, y, mCam.transform.position.z);
        }
    }
}
