using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryPanel;

    [SerializeField]
    private RectTransform inventoryContentPanel;

    [SerializeField]
    private UiInventoryItem itemPrefab;

    List<UiInventoryItem> listofUiItems = new List<UiInventoryItem>();
    private int inventorySize = 10;

    public List<ItemInstance> items = new();

    private void OnEnable()
    {
        DynamicInventory.OpenInventoryEvent += OpenInventory;
    }

    private void OnDisable()
    {
        DynamicInventory.OpenInventoryEvent -= OpenInventory;
    }

    public void InitializeInventoryUI()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            UiInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(inventoryContentPanel,false) ;
            listofUiItems.Add(uiItem);
        }
    }

    private void OpenInventory(List<ItemInstance> items, PointerEventData arg2)
    {
        Debug.Log("open inventory");

        Debug.Log("init inventory");
        InitializeInventoryUI();

        this.items = items;
        if (inventoryPanel.activeSelf == true)
            inventoryPanel.SetActive(false);
        else
            inventoryPanel.SetActive(true);
        if (items != null)
        {
            if (items.Count > 0)
                UpdateItems();
        }



    }

    private void UpdateItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            var currentItem = items[i];
            if (currentItem.itemType != null)
            {
                //add to first slot
                listofUiItems[i].SetData(currentItem.itemType.icon, currentItem.amount);
            }

        }


    }
}
