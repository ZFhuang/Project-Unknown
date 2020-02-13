using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Serializable game data
[System.Serializable]
public class GameData
{
    //Use Dictionary to save different scene's object state
    public Dictionary<string, int>[] scene=new Dictionary<string, int>[sceneSum];

    public float musicVol = 50;
    public float speedVol = 50;
    //Scene count, the only thing need to be writed
    public static int sceneSum = 5;
}
