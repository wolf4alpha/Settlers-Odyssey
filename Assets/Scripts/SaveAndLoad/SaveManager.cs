using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    
    private GameData gameData;
    private List<ISaveManager> saveManagers;

    

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;

    }

    private void Start()
    {
        saveManagers = FindAllSaveManagers();
        LoadGame();
    }   

    public void NewGame()
    {
        gameData = new GameData();
    }

    public void LoadGame()
    {
        
        if(gameData == null)
        {
            Debug.Log("No game data found, creating new game");
            NewGame(); 
        }

        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.LoadData(gameData);
        }        
       
    }

    public void SaveGame()
    {
        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.SaveData(ref gameData);
        }
        
        var json = JsonUtility.ToJson(gameData);
        Debug.Log("Game was Saved:" + json);  
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<ISaveManager> FindAllSaveManagers()
    {
        IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();
        return new List<ISaveManager>(saveManagers);
    }


}
