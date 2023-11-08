using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Item[] items = new Item[10];
    public int maxItemCount = 10;

    public void Start()
    {
        items[0] = new Wood(); 
        items[1] = new Stone();
        items[2] = new Food();
        AddItem(0, 7);
    }

    public void AddItem(int id, int count = 1)
    {
        items[id].Amount += count;
        if (items[id].Amount > maxItemCount)
        {
            items[id].Amount = maxItemCount;
        }
    }

    public void RemoveItem(int id, int count = 1)
    {
       Debug.Log("RemoveItem :"+count);
        items[id].Amount -= count;
    }
}
