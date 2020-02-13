using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IluustrationFast : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI mainText;
    [SerializeField] private TMPro.TextMeshProUGUI itemName;
    [SerializeField] private Image image;
    [SerializeField] private PauseMenu pause;
    [SerializeField] private ToolHub toolHub;

    //Load the item illustration and show in fast panel, 
    //will pause the game at the same time
    public void show(string name,string text,Sprite img)
    {
        gameObject.SetActive(true);
        pause.pauseGame();
        toolHub.gameObject.SetActive(false);
        toolHub.action();
        loadName(name);
        loadText(text);
        loadImage(img);
    }

    //Change main text
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

    //Big icon
    private void loadImage(Sprite sprite)
    {
        image.sprite = sprite;
    }

    //Name in title
    private void loadName(string name)
    {
        itemName.text = name;
    }
}
