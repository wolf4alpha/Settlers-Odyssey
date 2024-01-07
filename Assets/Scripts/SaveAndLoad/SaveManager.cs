using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;
using System;
using UnityEngine.UIElements;
using Assets.Scripts.InventoryManager.InventoryItems;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    
    private GameData gameData;
    private List<ISaveManager> saveManagers;
    private FileDataHandler dataHandler;
  
    private UIManager uiManager;

    [SerializeField]
    GameObject villagerPrefab;

    [SerializeField]
    private string fileName;

    

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;

    }

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        saveManagers = FindAllSaveManagers();
        LoadGame();
    }   

    public void NewGame()
    {
        gameData = new GameData();
    }

    public void LoadGame()
    {
        gameData = dataHandler.LoadJson();
        if(gameData == null)
        {
            Debug.Log("No game data found, creating new game");
            NewGame(); 
        }
        else
        {
            Debug.Log("savegame found create stuff");
            LoadVillagers();
            SetCameraSettings();
        }

        


        //foreach (ISaveManager saveManager in saveManagers)
        //{
        //    saveManager.LoadData(gameData);
        //}        
       
    }

    private void SetCameraSettings()
    {
       GameObject camera = GameObject.Find("Camera Rig Base");
       var cameraSettings = camera.GetComponent<CameraManager>();
       
        

    }

    private void LoadVillagers()
    {
        GameObject characters = GameObject.Find("Characters");

        var position = new float[3] { 52.27f, 37, 37 };
     
        foreach (KeyValuePair<string, GameDataCharacter> villagerData in gameData.characters)
        {
           //gameobject
            GameObject newVillager = Instantiate(villagerPrefab, new Vector3(position[0], position[1], position[2]), Quaternion.identity);
            newVillager.name = villagerData.Key;
            newVillager.transform.parent = characters.transform;
            Debug.Log("loading villager: "+villagerData.Key);

            //get villager script
            Villager villagerComponent = newVillager.GetComponent<Villager>();
            villagerComponent.LoadData(villagerData.Value);

            //move to villager
            //foreach(KeyValuePair<string, string> item in villagerData.Value.inventory)
            //{
            //    Debug.Log("loading item: " + item.Key + " x " + item.Value);
            //    ItemData itemData = Resources.Load<ItemData>(item.Key);
            //    for (int i = 0; i < int.Parse(item.Value); i++)
            //    {
            //        villagerComponent.inventory.AddItem(new ItemInstance(itemData));
            //    }
            //}
           
             //add villiager to UIManager
            uiManager.addVilliager(villagerComponent);
        }


    }

    //villager.position[0] = 0;
    //Debug.Log("loading villager " + villager.name);
    //GameObject newVillager = Instantiate(Resources.Load<GameObject>("Prefabs/Villager"), new Vector3(villager.position[0], villager.position[1], villager.position[2]), Quaternion.identity);
    //newVillager.name = villager.name;
    //newVillager.GetComponent<Villager>().LoadData(villager);

    public void SaveGame()
    {
        saveManagers = FindAllSaveManagers();
        Debug.Log("saving game, found "+saveManagers.Count()+" SaveManager");
        gameData = new GameData();
        foreach (ISaveManager saveManager in saveManagers)
        {
           Debug.Log("saving data for " + saveManager.GetType().ToString());
            saveManager.SaveData(ref gameData);
        }
        
        var json = JsonConvert.SerializeObject(gameData);
        dataHandler.SaveJson(gameData);
        //dataHandler.SaveXml(gameData);
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
