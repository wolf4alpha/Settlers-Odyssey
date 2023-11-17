using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<ItemInstance> items = new();
    public void AddItem(ItemInstance itemToAdd)
    {
        items.Add(itemToAdd);
    }
    public void RemoveItem(ItemInstance itemToRemove)
    {
        items.Remove(itemToRemove);
    }
}
