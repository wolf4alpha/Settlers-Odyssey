using Assets.Scripts.InventoryManager.InventoryItems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DynamicInventory : MonoBehaviour
{
    public int maxItems = 10;
    public List<ItemInstance> items = new();
    public bool AddItem(ItemInstance itemToAdd)
    {
        Debug.Log("Added item to inventory");
        // Finds an empty slot if there is one
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
            {
                items[i] = itemToAdd;
                return true;
            }
        }
        // Adds a new item if the inventory has space
        if (items.Count < maxItems)
        {
           
            items.Add(itemToAdd);
            return true;
        }
        Debug.Log("No space in the inventory");
        return false;
    }
}


[System.Serializable]
public class ItemInstance
{
    public ItemData itemType;
    public int condition;
    public int ammo;
    public ItemInstance(ItemData itemData)
    {
        itemType = itemData;
        condition = itemData.startingCondition;
        ammo = itemData.startingAmmo;
    }
}