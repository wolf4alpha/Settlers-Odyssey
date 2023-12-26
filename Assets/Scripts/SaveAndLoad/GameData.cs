using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public int food;
    public int wood;
    public int stone;

   
    public GameData()
    {
        food = 0;
        wood = 0;
        stone = 0;
        //inventory = new Dictionary<string, int>();
        //character = new Dictionary<string, string>();
        //character.Add("Villager", "test");
        //character.Add("Villager (1)", "test2");



    }
}
