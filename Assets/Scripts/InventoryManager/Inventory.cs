using Assets.Scripts.InventoryManager.InventoryItems;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu]
public class DynamicInventory : MonoBehaviour
{
    public int maxItems = 10;
    public int maxItemStack = 5;
    public List<ItemInstance> items = new();

    public static event System.Action<List<ItemInstance>> ChangeInventoryEvent;

    
    public bool AddItem(ItemInstance itemToAdd)
    {

       if(FindItemAndAdd(itemToAdd) == true)
       {
            ChangeInventoryEvent?.Invoke(items);
            return true;
       }

       return false;            

    }

    public bool FindItemAndAdd(ItemInstance itemToAdd)
    {
        //find the item in the inventory
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i]?.itemType?.itemId == itemToAdd.itemType.itemId)
            {
                //if the item is stackable, add to the stack
                if (items[i].itemType.stackable)
                {
                    if (items[i].amount < maxItemStack)
                    {
                        items[i].amount += itemToAdd.amount;
                        return true;
                    }
                }
            }
        }

        if (AddToNextEmptySlot(itemToAdd))
        {
            return true;
        }
        Debug.Log("No space in the inventory");
        return false;

    }

    private bool AddToNextEmptySlot(ItemInstance itemToAdd)
    {
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

        return false;
    }

    public bool RemoveItem(ItemInstance itemToRemove)
    {
        if(items.Contains(itemToRemove))
        {
            items.Remove(itemToRemove);
            return true;
        }
        return false;
    }

    public float InventoryCapacity()
    {
        int currentCapacity = 0;
        int maxCapacity = maxItems * maxItemStack;
        // count item.amount for each item in the inventory
        for (int i = 0; i < items.Count; i++)
        {
            currentCapacity += items[i].amount;
        }

        return currentCapacity*100/maxCapacity;

    }

    private void transferItems(DynamicInventory receiverInventory, ItemInstance item, int amount)
    {
        if(receiverInventory != null)
        {
            for (int i = 0; i < amount; i++)
            {
                if (receiverInventory.AddItem(item))
                {
                    RemoveItem(item);
                }
            }
            
        }

    
    }

}


[System.Serializable]
public class ItemInstance
{
    public ItemData itemType;
    public int amount;
    
    public ItemInstance(ItemData itemData)
    {
        itemType = itemData;
        amount = 1;               
    }
}