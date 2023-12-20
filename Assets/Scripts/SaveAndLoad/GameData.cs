using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public List<ItemInstance> items;
    public Dictionary<string , List<ItemInstance>> villagerInventory;
    public int food;
    public int wood;
    public int stone;



    public GameData()
    {
        villagerInventory = new();
        food = 0;
        wood = 0;
        stone = 0;
    }
}
