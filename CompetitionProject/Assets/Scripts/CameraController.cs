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

    [SerializeField]
    private Camera mainCamera;
    private bool mousePressing = false;
    private PLATFORM platform = PLATFORM.PHONE;

    //Camera move speed
    private float speed;

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

    // Update is called once per frame
    private void FixedUpdate()
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
            Vector3 p1 = p - mainCamera.transform.right *
                Input.GetAxisRaw("Mouse X") * speed * Time.timeScale * 0.5f;
            //Move Y
            Vector3 p2 = p1 - mainCamera.transform.up *
                Input.GetAxisRaw("Mouse Y") * speed * Time.timeScale * 0.5f;
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
            mainCamera.transform.Translate(-delta.x * speed /50f, -delta.y * speed / 50f, 0);
        }
    }
}
