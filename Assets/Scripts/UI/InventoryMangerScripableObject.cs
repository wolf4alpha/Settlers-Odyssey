using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "InventoryManager", menuName = "ScriptableObjects/InventoryManager", order = 1)]
public class InventoryMangerScripableObject : ScriptableObject
{

    public int Wood =10;
    public int Stone = 10;
        public int Food = 10;

    [System.NonSerialized]
    public UnityEvent<int> InventoryChangeEvent;

    private void OnEnable()
    {
        if(InventoryChangeEvent == null)
            InventoryChangeEvent = new UnityEvent<int>();
    }
   
    public void AddWood(int amount)
    {
        Wood += amount;
        InventoryChangeEvent.Invoke(Wood);
    }
}
