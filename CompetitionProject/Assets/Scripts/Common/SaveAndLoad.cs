using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveAndLoad : MonoBehaviour
{
    public GameData currentData;

    //Load value stored by objects, using buildIndex to identify the scene
    public int loadState(string name)
    {
        int val;
        if (currentData.scene[SceneManager.GetActiveScene().buildIndex].TryGetValue(name, out val))
        {
            Debug.Log(gameObject.name + " state: " + val);
        }
        else
        {
            val = 0;
            Debug.Log(gameObject.name + " create state: " + val);
        }
        return val;
    }

    //Save value for objects, using buildIndex to identify the scene
    public void saveState(string name,int state)
    {
        if (currentData.scene[SceneManager.GetActiveScene().buildIndex].ContainsKey(name))
        {
            currentData.scene[SceneManager.GetActiveScene().buildIndex].Remove(name);
        }
        currentData.scene[SceneManager.GetActiveScene().buildIndex].Add(name, state);
        Debug.Log(name + " save state: " + state);
    }

    //Save the whole data
    public void saveData(ref GameData save)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.OpenWrite(Application.persistentDataPath + "/save.dat");
        if (save == null)
        {
            Debug.LogError("Saving Not Found");
            file.Close();
        }
        else
        {
            Debug.Log("Save Data");
            bf.Serialize(file, save);
            file.Close();
        }
    }

    //Load the data, may create new file in Application.persistentDataPath + "/save.dat"
    //Application.persistentDataPath is C:\Users\*\AppData\LocalLow\*\*\
    public GameData loadData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (!File.Exists(Application.persistentDataPath + "/save.dat"))
        {
            FileStream file = File.Create(Application.persistentDataPath + "/save.dat");
            Debug.Log("Create new data");
            GameData gameData = new GameData();
            file.Close();
            return gameData;
        }
        else
        {
            FileStream file = File.OpenRead(Application.persistentDataPath + "/save.dat");
            if (file.Length == 0)
            {
                Debug.Log("Empty file");
                Debug.Log("Create new data");
                GameData gameData = new GameData();
                file.Close();
                return gameData;
            }
            else
            {
                GameData gameData = (GameData)bf.Deserialize(file);
                file.Close();
                Debug.Log("Load data");
                return gameData;
            }
        }
    }


    // Start is called before the first frame update
    void Awake()
    {
        currentData = loadData();
    }

    private void OnDestroy()
    {
        saveData(ref currentData);
    }
}
