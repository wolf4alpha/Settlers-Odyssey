using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public int Wood;
    [SerializeField]
    private Text woodValue;
    [SerializeField]
    private Text stoneValue;
    [SerializeField]
    private Text foodValue;

    [SerializeField]
    private InventoryMangerScripableObject inventoryScriptableObject;

    void Start()
    {
        Wood = inventoryScriptableObject.Wood;   
    }

    private void OnEnable()
    {
        inventoryScriptableObject.InventoryChangeEvent.AddListener(ChangeWoodValue);
    }

    private void OnDisable()
    {
        inventoryScriptableObject.InventoryChangeEvent.RemoveListener(ChangeWoodValue);
    }

    void ChangeWoodValue(int amount)
    {
        woodValue.text = amount.ToString();
    }
}
