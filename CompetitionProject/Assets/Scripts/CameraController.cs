using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
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
    private float zoomSpeed = 0f;
    private float zoomScale_old = 5f;
    private float zoomScale_to;
    private Vector3 moveSpeed;
    private Vector3 lastPosition;
    private Vector3 targetPosition;

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

    //Transwrite of cameraIn
    public void cameraOut()
    {
        targetPosition = lastPosition;
        zoomScale_to = zoomScale_old;
        camState = STATE.MOVE_ZOOM;
        mousePressing = false;
    }

    //Change the state to zoom in FixedUpdate
    public void cameraIn(GameObject target)
    {
        lastPosition = Camera.main.gameObject.transform.position;
        targetPosition = target.gameObject.transform.position;
        zoomScale_to = zoomScale;
        camState = STATE.MOVE_ZOOM;
        mousePressing = false;
    }

    // Start is called before the first frame update
    private void Start()
    {
        //init
        loadCamSettings();

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
        if (camState == STATE.IDLE)
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
        else if (camState == STATE.MOVE_ZOOM)
        {
            Camera.main.orthographicSize = Mathf.SmoothDamp(
                Camera.main.orthographicSize, zoomScale_to, ref zoomSpeed, zoomTime * Time.deltaTime);
            Camera.main.transform.position = Vector3.SmoothDamp(
                Camera.main.transform.position,
                new Vector3(targetPosition.x, targetPosition.y, Camera.main.transform.position.z),
                ref moveSpeed, zoomTime * Time.deltaTime);
            if (Mathf.Abs(Camera.main.orthographicSize - zoomScale_to) <= zoomScale_to * 0.01f)
            {
                Debug.Log("Got place!");
                Camera.main.orthographicSize = zoomScale_to;
                Camera.main.transform.position =
                new Vector3(targetPosition.x, targetPosition.y, Camera.main.transform.position.z);
                camState = STATE.IDLE;
                mousePressing = false;
            }
        }

        //Camera slide

        //Camera zoom

        //Camera move

        //Camera touch
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
            Vector3 p = Camera.main.transform.position;
            //Move X
            Vector3 p1 = p - Camera.main.transform.right *
                Input.GetAxis("Mouse X") * speed * Time.timeScale * 0.5f;
            //Move Y
            Vector3 p2 = p1 - Camera.main.transform.up *
                Input.GetAxis("Mouse Y") * speed * Time.timeScale * 0.5f;
            //Set transform
            Camera.main.transform.position = p2;
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
            Camera.main.transform.Translate(-delta.x * speed / 50f, -delta.y * speed / 50f, 0);
        }
    }
}
