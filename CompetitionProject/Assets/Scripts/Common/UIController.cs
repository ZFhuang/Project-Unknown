using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(Camera.main.gameObject);
    }
}
