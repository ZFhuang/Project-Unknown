using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera mainCamera;
    bool mousePressing = false;
    //Camera move speed
    public float speed = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = this.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
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
            HandleMouseInput();
        }
    }

    private void HandleMouseInput()
    {
        if (mainCamera != null)
        {
            Vector3 p = mainCamera.transform.position;
            //Move X
            Vector3 p1 = p - mainCamera.transform.right * Input.GetAxisRaw("Mouse X") * speed * Time.timeScale;
            //Move Y
            Vector3 p2 = p1 - mainCamera.transform.up * Input.GetAxisRaw("Mouse Y") * speed * Time.timeScale;
            //Set transform
            mainCamera.transform.position = p2;
            //Debug.Log(p2);
        }
    }
}
