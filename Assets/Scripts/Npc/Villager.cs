using Assets.Scripts.InventoryManager.InventoryItems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class Villager : MonoBehaviour, IPointerClickHandler, ISaveManager
{
    #region states
    public VillagerStateMachine stateMachine { get; private set; }
    public VillagerIdleState idleState { get; private set; }
    public VillagerMoveState moveState { get; private set; }
    public VillagerWorkState workState { get; private set; }
    public VillagerSleepState sleepState { get; private set; }
    public VillagerEatState eatState { get; private set; }
    #endregion

    public Animator animator { get; private set; }
    public CharacterStats stats { get; private set; }
    public MovePreview moveController { get; private set; }
    public VillagerBrain brain { get; private set; }

    public DynamicInventory inventory { get; private set; }
    //  public InventoryMangerScripableObject inventoryManager;

    public GameObject destination;
    public RessourceProperties destinationProperties;

    public bool lastActionFilledInventory;
    public bool isSelected;

    [SerializeField]
    private Workplace Workplaces;

    public ItemData selectedWork;

    private Home home;

    public static event System.Action<Villager> SelectedVillagerEvent;


    #region Debug
    public string currentAction;
    #endregion


    private void Awake()
    {

        stateMachine = new VillagerStateMachine();

        idleState = new VillagerIdleState(this, stateMachine, "idle");
        moveState = new VillagerMoveState(this, stateMachine, "walk");
        workState = new VillagerWorkState(this, stateMachine, "work");
        sleepState = new VillagerSleepState(this, stateMachine, "sleep");
        eatState = new VillagerEatState(this, stateMachine, "eat");

        stats = GetComponent<CharacterStats>();
        inventory = GetComponent<DynamicInventory>();
        moveController = GetComponent<MovePreview>();

    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        brain = GetComponent<VillagerBrain>();
        stats.maxEnergy = 100;
        stateMachine.Initialize(idleState);

    }

    private void Update()
    {
        stateMachine.currentState.Update();
        //   currentAction = stateMachine.currentState.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        selectVillager();
    }

    public void selectVillager()
    {
        isSelected = true;
        SelectedVillagerEvent?.Invoke(this);
    }

    public void SetWorkItem(ItemData item)
    {
        selectedWork = item;
    }

    //coroutine
    public void DoWork(int time)
    {
        if(destinationProperties == null)
            return;
        animator.SetBool("work", true);
        destinationProperties.AssingVillager();
        StartCoroutine(WorkCoroutine(time));
    }

    public void DoSleep(int time)
    {
        destinationProperties.AssingVillager();
        StartCoroutine(SleepCoroutine(time));
    }

    public void DoEating(int time)
    {
        destinationProperties.AssingVillager();
        StartCoroutine(EatCoroutine(time));
    }

    public void DoWait(int time)
    {
        StartCoroutine(WaitCoroutine(time));
    }

    public void DoTransferItems(int time)
    {
        StartCoroutine(TransferItemsCoroutine(time));
    }



    public void OnFinishedAction()
    {

        if (destinationProperties != null)
        {
            destinationProperties.RemoveVillager();
        }
        stateMachine.ChangeState(idleState);
    }

    IEnumerator WorkCoroutine(int counter)
    {
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            stats.RemoveEnergy(2);
            stats.removeHunger(2);
            counter--;
        }
        var harvestAmount = GenerateRandomNumber(1, 3);
        destinationProperties.RemoveRessource(harvestAmount);
        var targetRessource = new ItemInstance(destinationProperties.getRessource());
        targetRessource.amount = harvestAmount;
        inventory.AddItem(targetRessource);



        ItemInstance itemInstance = new ItemInstance(destinationProperties.getRessource());

        OnFinishedAction();

    }

    IEnumerator SleepCoroutine(int time)
    {
        int counter = time;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }

        Debug.Log("i got 70 Energy, lost 5 Hunger from sleep");
        stats.AddEnergy(70);
        stats.removeHunger(5);
        OnFinishedAction();
    }

    IEnumerator EatCoroutine(int time)
    {

        int counter = time;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }

        Debug.Log("i got 50 Hunger from eat");
        stats.addHunger(50);
        OnFinishedAction();
    }

    IEnumerator WaitCoroutine(int time)
    {

        int counter = time;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }

        Debug.Log("i just waited");
        OnFinishedAction();
    }

    IEnumerator TransferItemsCoroutine(int time)
    {
        for (int i = inventory.items.Count - 1; i >= 0; i--)
        {
            //  Debug.Log("i transfer " + inventory.items[i].amount + " x " + inventory.items[i].itemType.itemName + " to " + destination?.name);
            if (inventory.transferedItem(destination.GetComponent<DynamicInventory>(), inventory.items[i]))
            {
                inventory.items.RemoveAt(i);
            }


        }

        int counter = time;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }

        OnFinishedAction();
    }

    public void LoadData(GameDataCharacter _data)
    {
        //load inventory
        foreach (KeyValuePair<string, string> item in _data.inventory)
        {
            Debug.Log("loading item for " +this.name +" : " +item.Key + " x " + item.Value);
            ItemData itemData = Resources.Load<ItemData>(item.Key);
            for (int i = 0; i < int.Parse(item.Value); i++)
            {
                inventory.AddItem(new ItemInstance(itemData));
            }
        }
        //load stats
        foreach (KeyValuePair<string, string> stat in _data.stats)
        {
            Debug.Log("loading stat for " + this.name + " : " + stat.Key + " x " + stat.Value);
            switch (stat.Key)
            {
                case "hunger":
                    stats.SetHunger(int.Parse(stat.Value));
                    break;
                case "energy":
                    stats.SetEnergy(int.Parse(stat.Value));
                    break;
                case "money":
                    stats.SetMoney(int.Parse(stat.Value));
                    break;
                default:
                    break;
            }
        }

        //load position
        Vector3 position = new Vector3(_data.position[0], _data.position[1], _data.position[2]);
        this.transform.position = position;

        Debug.Log("Loaded data for " + this.transform.name);

    }

    public void SaveData(ref GameData _data)
    {
        GameDataCharacter chardata = new();

        //inventory
        foreach (var item in inventory.items)
        {
            chardata.inventory.Add(item.itemType.itemName, item.amount.ToString());
        }

        //stats
        chardata.stats.Add("hunger", stats.hunger.ToString());
        chardata.stats.Add("energy", stats.energy.ToString());
        chardata.stats.Add("money", stats.money.ToString());

        //position
        chardata.position[0] = this.transform.position.x;
        chardata.position[1] = this.transform.position.y;
        chardata.position[2] = this.transform.position.z;

        //add all data to gameData
        _data.characters.Add(this.transform.name, chardata);
    }


    public int GenerateRandomNumber(int min, int max)
    {
        System.Random random = new System.Random();
        return random.Next(min, max + 1);
    }

}
