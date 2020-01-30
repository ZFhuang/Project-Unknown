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

    private Camera mainCamera;
    private bool mousePressing = false;
    private PLATFORM platform = PLATFORM.PHONE;

    //Camera move speed
    public float speed = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        //init
        mainCamera = this.GetComponent<Camera>();

        //Use preprocessor to get using platform
#if UNITY_EDITOR || UNITY_STANDALONE
        platform=0;
#elif UNITY_IPHONE || UNITY_ANDROID
        platform=1;
#endif
    }

    // Update is called once per frame
    void Update()
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

    //Camera moving method for Windows, Linux, OSX
    private void cameraTranlate_STANDALONE()
    {
        //On mouse down
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Left DOWN!");
            mousePressing = true;
        }
        //On mouse up
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse Left UP!");
            mousePressing = false;
        }
        //On mouse pressing
        if (mousePressing)
        {
            Vector3 p = mainCamera.transform.position;
            //Move X
            Vector3 p1 = p - mainCamera.transform.right * Input.GetAxisRaw("Mouse X") * speed * Time.timeScale;
            //Move Y
            Vector3 p2 = p1 - mainCamera.transform.up * Input.GetAxisRaw("Mouse Y") * speed * Time.timeScale;
            //Set transform
            mainCamera.transform.position = p2;
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
            mainCamera.transform.Translate(delta.x * speed, delta.y * speed, 0);
        }
    }
}
