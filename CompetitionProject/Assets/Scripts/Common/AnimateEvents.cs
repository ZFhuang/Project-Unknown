using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateEvents : MonoBehaviour
{
    private void _closeThisObject()
    {
        gameObject.SetActive(false);
    }

    private void _openThisObject()
    {
        gameObject.SetActive(true);
    }
}
