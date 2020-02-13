using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IllustrationMenu : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI mainText;
    [SerializeField] private TMPro.TextMeshProUGUI itemName;
    [SerializeField] private Image image;
    [SerializeField] private IluustrationFast iluustrationFast;

    [SerializeField] private GameObject _object0;
    [SerializeField] private GameObject _object1;
    [SerializeField] private GameObject _object2;
    [SerializeField] private GameObject _object3;
    [SerializeField] private GameObject _object4;
    [SerializeField] private GameObject _object5;

    private bool[] objectsValid;
    private GameObject[] objects;
    private int selecting;
    private int sum;

    //Sort the hub objects
    public void refreshScoller()
    {
        //Choose a capble color
        for (int i = 0; i < sum; i++)
        {
            if (!objectsValid[i])
            {
                objects[i].GetComponent<Image>().color = new Color(0, 0, 0);
            }
            else
            {
                if (i == selecting)
                {
                    objects[i].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
                }
                else
                {
                    objects[i].GetComponent<Image>().color = new Color(1, 1, 1);
                }
            }
        }
    }

    //Called by clicking the button
    public void selectObject(int num)
    {
        if (num >= 0 && num < sum)
        {
            if (objectsValid[num] == true)
            {
                selecting = num;
                loadText(objects[selecting].name);
                loadImage(objects[selecting].GetComponent<Image>().sprite);
                image.color = new Color(1, 1, 1);
                loadName(objects[selecting].name);
            }
            else
            {
                loadText(null);
                loadImage(objects[selecting].GetComponent<Image>().sprite);
                image.color = new Color(0, 0, 0);
                loadName("Object "+num);
            }
        }
        refreshScoller();
    }

    //Call when pickup a object
    public bool addObject(string name)
    {
        for(int i = 0; i < sum; i++)
        {
            if (objects[i].name == name)
            {
                if (objectsValid[i] == true)
                {
                    return false;
                }
                objectsValid[i] = true;
                refreshScoller();
                iluustrationFast.show(objects[i].name, objects[i].name,
                    objects[i].GetComponent<Image>().sprite);
                return true;
            }
        }
        Debug.LogError("Illustration Not Found");
        return false;
    }

    //Change main text content, this will load txt in Resources/Text
    private void loadText(string name)
    {
        if (name == null)
        {
            mainText.text = "Nothing"; 
        }
        else
        {
            TextAsset ta = (TextAsset)Resources.Load("Text\\" + name);
            if (ta == null)
            {
                Debug.LogError("File" + "[Text\\" + name + "] Not Found");
                mainText.text = "Text Not Found";
            }
            else
            {
                string txt = ta.text;
                mainText.text = txt;
            }
        }
    }

    //Change current big image
    private void loadImage(Sprite sprite)
    {
        image.sprite = sprite;
    }

    //Change item name
    private void loadName(string name)
    {
        itemName.text = name;
    }

    // Start is called before the first frame update
    void Start()
    {
        sum = 6;

        objectsValid = new bool[sum];
        objects = new GameObject[sum];

        for (int i = 0; i < sum; i++)
        {
            objectsValid[i] = false;
        }
        objects[0] = _object0;
        objects[1] = _object1;
        objects[2] = _object2;
        objects[3] = _object3;
        objects[4] = _object4;
        objects[5] = _object5;

        selectObject(0);
        refreshScoller();
    }
}
