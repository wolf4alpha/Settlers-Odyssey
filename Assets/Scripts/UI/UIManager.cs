using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryPanel;

    [SerializeField]
    private RectTransform inventoryContentPanel;

    [SerializeField]
    private GameObject StatsPanel;

    [SerializeField]
    private Text StatsName;

    [SerializeField]
    private Text StatsHunger;

    [SerializeField]
    private Text StatsEnergy;

    [SerializeField]
    private Text StatsMoney;

    [SerializeField]
    private UiInventoryItem itemPrefab;

    [SerializeField]
    private Villager selectVillager;

    [SerializeField]
    List<UiInventoryItem> listofUiItems = new List<UiInventoryItem>();
    private int inventorySize = 10;

    public List<ItemInstance> items = new();

    private float uiUpdateInterval = 0.5f;

    private float currentUpdateInterval = 0;


    private void Awake()
    {
        // InitializeInventoryUI();

    }

    private void OnEnable()
    {
        //  for global inv? DynamicInventory.ChangeInventoryEvent += ChangeInventory;
        Villager.SelectedVillagerEvent += SelectVillager;
    }

    private void OnDisable()
    {
        //   DynamicInventory.ChangeInventoryEvent -= ChangeInventory;
        Villager.SelectedVillagerEvent -= SelectVillager;
    }

    private void Update()
    {
        if (selectVillager == null)
            return;

        currentUpdateInterval -= Time.deltaTime;
        if (currentUpdateInterval < 0)
        {
            UpdateItems();
            UpdateStats();
            currentUpdateInterval = uiUpdateInterval;
        }
    }

    public void InitializeInventoryUI()
    {
        listofUiItems.Clear();
        while (inventoryContentPanel.transform.childCount > 0)
        {
            DestroyImmediate(inventoryContentPanel.transform.GetChild(0).gameObject);
        }

        if (selectVillager == null)
            return;

        Debug.Log("Villager maxitems = " + selectVillager.inventory.maxItems);
        for (int i = 0; i < selectVillager.inventory.maxItems; i++)
        {
            UiInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(inventoryContentPanel, false);
            listofUiItems.Add(uiItem);
        }

    }


    private void SelectVillager(Villager villager)
    {
        selectVillager = villager;
        items = villager.inventory.items;
        Debug.Log("new villager select:" + villager.name);
        InitializeInventoryUI();
        UpdateItems();
        UpdateStats();
    }

    private void UpdateStats()
    {
        StatsName.text = selectVillager.name;
        StatsHunger.text = selectVillager.stats.hunger.ToString();
        StatsEnergy.text = selectVillager.stats.energy.ToString();
        StatsMoney.text = selectVillager.stats.money.ToString();
    }

    private void UpdateItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            var currentItem = items[i];
            if (currentItem.itemType != null)
            {
                //add to first slot
                listofUiItems[i].gameObject.SetActive(true);
                listofUiItems[i].SetData(currentItem.itemType.icon, currentItem.amount);
            }

        }


    }
}
