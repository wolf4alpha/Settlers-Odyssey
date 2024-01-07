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
    private Text FSM;

    [SerializeField]
    private Text BestAction;

    [SerializeField]
    private UiInventoryItem itemPrefab;

    [SerializeField]
    private Villager selectVillager;

    [SerializeField]
    private List<Villager> _villagers = new List<Villager>();

    [SerializeField]
    List<UiInventoryItem> listofUiItems = new List<UiInventoryItem>();

    #region BaseItems
    [SerializeField]
    private DynamicInventory BaseInventory;
    [SerializeField]
    private Text BaseWoodAmount;
    [SerializeField]
    private Text BaseFoodAmount;
    [SerializeField]
    private Text BaseStoneAmount;
    #endregion

    public List<ItemInstance> items = new();

    private float uiUpdateInterval = 0.5f;

    private float currentUpdateInterval = 0;

    private void Awake()
    {
        BaseInventory = GameObject.FindWithTag("Base").GetComponent<DynamicInventory>();
       
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
            SelectNextVillager();

        currentUpdateInterval -= Time.deltaTime;
        if (currentUpdateInterval < 0)
        {
            UpdateItems();
            UpdateBaseItems();
            UpdateStats();
            currentUpdateInterval = uiUpdateInterval;
        }
    }

    private void UpdateBaseItems()
    {
       
       BaseWoodAmount.text = BaseInventory.items.Find(x => x.itemType.name == "Wood")?.amount.ToString() ?? "0";
       BaseFoodAmount.text = BaseInventory.items.Find(x => x.itemType.name == "Food")?.amount.ToString() ?? "0";
       BaseStoneAmount.text = BaseInventory.items.Find(x => x.itemType.name == "Stone")?.amount.ToString() ?? "0";

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

        for (int i = 0; i < selectVillager.inventory.maxItems; i++)
        {
            UiInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(inventoryContentPanel, false);
            listofUiItems.Add(uiItem);
        }

    }

    public void SelectNextVillager()
    {
        if(selectVillager == null)
        {
            if (_villagers.Count > 0)
            {
                _villagers[0].selectVillager();
            }
        }
        else
        {
            int index = _villagers.IndexOf(selectVillager);
            if (index < _villagers.Count - 1)
            {
                _villagers[index + 1].selectVillager();
            }
            else
            {
                _villagers[0].selectVillager();
            }
        }
    }

    //selectpreviousVillager
    public void SelectPreviousVillager()
    {
        if (selectVillager == null)
        {
            if (_villagers.Count > 0)
            {
                _villagers[0].selectVillager(); 
            }
        }
        else
        {
            int index = _villagers.IndexOf(selectVillager);
            if (index > 0)
            {
                _villagers[index - 1].selectVillager();
            }
            else
            {
              _villagers[_villagers.Count - 1].selectVillager();
            }
        }
    }


    private void SelectVillager(Villager villager)
    {
        selectVillager = villager;
        items = villager.inventory.items;
        InitializeInventoryUI();
        UpdateItems();
        UpdateStats();
    }

    private void UpdateStats()
    {
        if(selectVillager == null)
            return;
        StatsName.text = selectVillager.name;
        StatsHunger.text = selectVillager.stats.hunger.ToString();
        StatsEnergy.text = selectVillager.stats.energy.ToString();
        StatsMoney.text = selectVillager.stats.money.ToString();
        FSM.text = selectVillager.stateMachine.currentState.ToString();
        BestAction.text = selectVillager.brain.bestAction?.name ?? "none";
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

    public void addVilliager(Villager villager)
    {
        _villagers.Add(villager);
    }
}
