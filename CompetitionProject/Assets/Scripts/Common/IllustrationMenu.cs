using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IllustrationMenu : MonoBehaviour
{
    private bool isDown = false;
    private bool[] objectsValid = new bool[6];
    private GameObject[] objects = new GameObject[6];
    private int selecting;
    private int sum;

    //Sort the hub objects
    public void refreshScoller()
    {
        //Choose a capble color
        for (int i = 0; i < 6; i++)
        {
            if (!objectsValid[i])
            {
                objects[selecting].GetComponent<Image>().color = new Color(0, 0, 0);
            }
            else
            {
                objects[i].GetComponent<Image>().color = new Color(1, 1, 1);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
