using Assets.Scripts.InventoryManager.InventoryItems;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    
    public Dictionary<string, GameDataCharacter> characters;

    public GameData()
    {
        characters = new Dictionary<string, GameDataCharacter>();
    }
}


[Serializable]
public class GameDataCharacter
{
    public Dictionary<string, string> inventory;
    public Dictionary<string, string> stats;
    public float[] position;

    public GameDataCharacter()
    {
        inventory = new Dictionary<string, string>();
        stats = new Dictionary<string, string>();
        position = new float[3];        
    }
}

public class GameDataInventory
{
    public Dictionary<string, string> inventory;

    public GameDataInventory()
    {
        inventory = new Dictionary<string, string>();
    }
}


public class GameDataStats
{
    public Dictionary<string, string> stats;

    public GameDataStats()
    {
        stats = new Dictionary<string, string>();
    }
}


