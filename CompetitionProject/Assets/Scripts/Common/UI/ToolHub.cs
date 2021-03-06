﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolHub : MonoBehaviour
{
    [SerializeField] private IllustrationMenu illustration;
    [SerializeField] private SaveAndLoad saver;
    [SerializeField] private Animator toolHubAni;
    [SerializeField] private Animator exHubAni;
    [SerializeField] private GameObject _object0;
    [SerializeField] private GameObject _object1;
    [SerializeField] private GameObject _object2;
    [SerializeField] private GameObject _object3;
    [SerializeField] private GameObject _object4;
    [SerializeField] private GameObject _object5;

    private bool isDown = false;
    private bool[] objectsValid = new bool[6];
    private GameObject[] objects = new GameObject[6];
    private int selecting;
    private int sum;

    //Sort the hub objects
    public void refreshToolHub()
    {
        for (int i = 0; i < sum; i++)
        {
            if (!objectsValid[i])
            {
                for (int j = i + 1; j < sum + 2; j++)
                {
                    if (objectsValid[j])
                    {
                        objects[i].name = objects[j].name;
                        objects[i].GetComponent<Image>().sprite =
                            objects[j].GetComponent<Image>().sprite;
                    }
                }
            }
        }

        for (int i = sum; i < 6; i++)
        {
            if (!objectsValid[i])
            {
                objects[i].name = null;
                objects[i].GetComponent<Image>().sprite = null;
            }
        }

        //Choose a capble color
        for (int i = 0; i < 6; i++)
        {
            if (!objectsValid[i])
            {
                objects[i].SetActive(false);
            }
            else
            {
                objects[i].SetActive(true);
                if (i == selecting)
                {
                    objects[selecting].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
                }
                else
                {
                    objects[i].GetComponent<Image>().color = new Color(1, 1, 1);
                }
            }
        }
    }

    //Add a new object to hub
    public bool addObject(string name)
    {
        if (name == "")
        {
            return false;
        }
        else if (sum < 6)
        {
            objectsValid[sum] = true;
            objects[sum].name = name;
            objects[sum].GetComponent<Image>().sprite = illustration.GetSprite(name);
            sum += 1;
            refreshToolHub();
            Debug.Log("Toolhub add: " + name);
            saver.saveTools();
            return true;
        }
        return false;
    }

    //Add a new object to hub
    public bool addObject(GameObject gameObject)
    {
        if (sum < 6)
        {
            objectsValid[sum] = true;
            objects[sum].name = gameObject.name;
            objects[sum].GetComponent<Image>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            sum += 1;
            refreshToolHub();
            Debug.Log("Toolhub add: " + gameObject.name);
            saver.saveTools();
            return true;
        }
        return false;
    }

    //Called by clicking the button
    public void selectObject(int num)
    {
        if (num >= 0 && num < 6 && objectsValid[num] == true)
        {
            selecting = num;
            refreshToolHub();
        }
    }

    //Return the target object's name for checking
    public string getToolName(int idx)
    {
        if (idx >= 0 && idx < 6 && objectsValid[idx] == true)
        {
            return objects[idx].name;
        }
        return null;
    }

    public Sprite getToolSprite(int idx)
    {
        if (idx >= 0 && idx < 6 && objectsValid[idx] == true)
        {
            return objects[idx].GetComponent<Image>().sprite;
        }
        return null;
    }

    //Return the selecting object's name for checking
    public string getSelectingName()
    {
        if (selecting >= 0 && selecting < 6 && objectsValid[selecting] == true)
        {
            return objects[selecting].name;
        }
        return null;
    }

    //Use the selecting object in the hub
    public bool useSelectingObject()
    {
        if (selecting >= 0 && selecting <= 6 && objectsValid[selecting] == true)
        {
            objects[selecting].name = null;
            objects[selecting].GetComponent<Image>().sprite = null;
            sum -= 1;
            objectsValid[selecting] = false;
            selecting = -1;
            refreshToolHub();
            saver.saveTools();
            return true;
        }
        return false;
    }

    //Use object in the hub by name
    public bool useObjectByName(string name)
    {
        for (int i = 0; i < 6; i++)
        {
            if (objectsValid[i]&&objects[i].name==name)
            {
                objects[i].name = null;
                objects[i].GetComponent<Image>().sprite = null;
                sum -= 1;
                objectsValid[i] = false;
                selecting = -1;
                refreshToolHub();
                saver.saveTools();
                return true;
            }
        }
        return false;
    }

    //Hub's animations
    public void action()
    {
        if (isDown)
        {
            isDown = false;
            toolHubAni.SetBool("isDown", isDown);
            exHubAni.SetBool("isDown", isDown);
        }
        else
        {
            isDown = true;
            toolHubAni.SetBool("isDown", isDown);
            exHubAni.SetBool("isDown", isDown);
        }
    }

    //Init
    private void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            objectsValid[i] = false;
        }
        objects[0] = _object0;
        objects[1] = _object1;
        objects[2] = _object2;
        objects[3] = _object3;
        objects[4] = _object4;
        objects[5] = _object5;
        selecting = -1;
        sum = 0;

        saver.loadTools();
        refreshToolHub();
    }

    private void OnDestroy()
    {
        saver.saveTools();
    }
}
