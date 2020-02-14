using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveAndLoad : MonoBehaviour
{
    [SerializeField] GameObject UI;
    private ToolHub toolHub;
    private IllustrationMenu illustrationMenu;
    private static GameData currentData;

    //Load value stored by objects, using buildIndex to identify the scene
    public int loadState(string name)
    {
        int val;
        if (currentData.scene[SceneManager.GetActiveScene().buildIndex] == null)
            currentData.scene[SceneManager.GetActiveScene().buildIndex] = new Dictionary<string, int>();
        if (!currentData.scene[SceneManager.GetActiveScene().buildIndex].TryGetValue(name, out val))
        {
            val = 0;
        }
        return val;
    }

    //Save value for objects, using buildIndex to identify the scene
    public void saveState(string name, int state)
    {
        if (currentData.scene[SceneManager.GetActiveScene().buildIndex] == null)
            currentData.scene[SceneManager.GetActiveScene().buildIndex] = new Dictionary<string, int>();
        if (currentData.scene[SceneManager.GetActiveScene().buildIndex].ContainsKey(name))
        {
            currentData.scene[SceneManager.GetActiveScene().buildIndex].Remove(name);
        }
        currentData.scene[SceneManager.GetActiveScene().buildIndex].Add(name, state);
    }

    public void sceneChange(string sceneName)
    {
        currentData.currentSceneName = sceneName;
    }

    public string getSceneName()
    {
        return currentData.currentSceneName;
    }

    public void saveTools()
    {
        if (toolHub == null)
            toolHub = GameObject.Find("ToolMenu").GetComponent<ToolHub>();

        for (int i = 0; i < GameData.toolSum; i++)
        {
            currentData.tools[i] = toolHub.getToolName(i);
        }
    }

    public void loadTools()
    {
        if (toolHub == null)
            toolHub = GameObject.Find("ToolMenu").GetComponent<ToolHub>();

        for (int i = 0; i < GameData.toolSum; i++)
        {
            if (currentData.tools[i] == null)
            {
                currentData.tools[i] = "";
            }
            toolHub.addObject(currentData.tools[i]);
        }
    }

    public void saveIllu()
    {
        for (int i = 0; i < GameData.itemSum; i++)
        {
            currentData.itemIlluValid[i] = IllustrationMenu.objectsValid[i];
        }
    }

    public void loadIllu(ref bool[] inp)
    {
        for (int i = 0; i < GameData.itemSum; i++)
        {
            inp[i] = currentData.itemIlluValid[i];
        }
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
    public void loadData(ref GameData gameData)
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (!File.Exists(Application.persistentDataPath + "/save.dat"))
        {
            FileStream file = File.Create(Application.persistentDataPath + "/save.dat");
            Debug.Log("Create new data");
            gameData = new GameData();
            if (gameData.currentSceneName == null)
                gameData.currentSceneName = "Scene01";
            file.Close();
        }
        else
        {
            FileStream file = File.OpenRead(Application.persistentDataPath + "/save.dat");
            if (file.Length == 0)
            {
                Debug.Log("Empty file");
                Debug.Log("Create new data");
                gameData = new GameData();
                if (gameData.currentSceneName == null)
                    gameData.currentSceneName = "Scene01";
                file.Close();
            }
            else
            {
                gameData = (GameData)bf.Deserialize(file);
                file.Close();
                if (gameData.currentSceneName == null)
                    gameData.currentSceneName = "Scene01";
                Debug.Log("Load data");
            }
        }
    }


    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        currentData = new GameData();
        loadData(ref currentData);
    }

    private void Start()
    {
        illustrationMenu = UI.GetComponentInChildren<IllustrationMenu>();
        //SceneManager.LoadScene(currentData.currentSceneName, LoadSceneMode.Single);
    }

    private void OnDestroy()
    {
        saveData(ref currentData);
    }
}
