using Assets.Scripts.InventoryManager.InventoryItems;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UIWorkListItem : MonoBehaviour
{

    [SerializeField]
    private Villager villager;

    [SerializeField]
    private List<ItemData> workItems = new List<ItemData>();


    // Start is called before the first frame update
    void Start()
    {
        var dropdown = GetComponentInChildren<Dropdown>();
        dropdown.options.Clear();

        foreach (ItemData item in workItems)
        {
            dropdown.options.Add(new Dropdown.OptionData("gather:" + item.name));
        }

    }

    public void OnDropdownValueChanged(Dropdown change)
    {
        Debug.Log("Dropdown value: " +change.value);
                villager.selectedWork = workItems[change.value]; 
              
       
    }

    internal void setVillager(Villager villager)
    {
        this.villager = villager;
    }
}


