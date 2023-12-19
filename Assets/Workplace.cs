using Assets.Scripts.InventoryManager.InventoryItems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workplace : MonoBehaviour
{
   
    [SerializeField]
    private List<Villager> villagerList;

    [SerializeField]
    private int maxWorkerAmount;

    [SerializeField]
    public List<ItemData> ProductionItems {  get; set; }

}
