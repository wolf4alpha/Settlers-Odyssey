using Assets.Scripts.InventoryManager.InventoryItems;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DynamicInventory : MonoBehaviour, ISaveManager
{
    public int maxItems = 10;
    public int maxItemStack = 5;
    public List<ItemInstance> items = new();

    public static event System.Action<List<ItemInstance>> ChangeInventoryEvent;

    public bool AddItem(ItemInstance itemToAdd)
    {

        if (FindItemAndAdd(itemToAdd) == true)
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

        if (items.Contains(itemToRemove))
        {
            items.Remove(itemToRemove);
            return true;
        }
        return false;
    }

    //
    // Zusammenfassung:
    //     return the percentage of the used inventory capacity
    //
    // Parameter:
    //   value:
    public float InventoryCapacity()
    {
        int currentCapacity = 0;
        int maxCapacity = maxItems * maxItemStack;

        // count item.amount for each item in the inventory
        for (int i = 0; i < items.Count; i++)
        {
            currentCapacity += items[i].amount;
        }
        //Debug.Log("Maxitems = " + maxItems + " maxItemStack = " + maxItemStack + " currentCapacity = " + currentCapacity * 100 / maxCapacity);
        return currentCapacity * 100 / maxCapacity;

    }

    public bool transferedItem(DynamicInventory receiverInventory, ItemInstance item)
    {

        if (receiverInventory != null)
        {
            if (receiverInventory.AddItem(item))
                return true;

        }

        return false;

    }

    public void LoadData(GameData _data)
    {
        if (this.transform.name == "Base")
        {
            items[0].amount = _data.food;
            items[1].amount = _data.wood;
            items[2].amount = _data.stone;
        }
            Debug.Log("Loaded data for " + this.transform.name);
        
    }

    public void SaveData(ref GameData _data)
    {
        if (this.transform.name == "Base")
        {
            _data.food = items[0].amount;
            _data.wood = items[1].amount;
            _data.stone = items[2].amount;

            var json = JsonUtility.ToJson(_data);
            Debug.Log("Saved data for " + this.transform.name + ": "+json);
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