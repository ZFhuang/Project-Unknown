using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Serializable game data
[System.Serializable]
public class GameData
{
    //Use Dictionary to save different scene's object state
    public Dictionary<string, int>[] scene;

    public float musicVol = 50;
    public float speedVol = 50;
    public int currentSceneIdx;
    public string[] tools;
    public bool[] itemIlluValid;

    public const int itemSum = 6;
    public const int toolSum = 6;
    public const int sceneSum = 6;

    public GameData()
    {
        tools = new string[toolSum];
        itemIlluValid = new bool[itemSum];
        scene = new Dictionary<string, int>[sceneSum];
    }
}
