using Assets.Scripts.InventoryManager.InventoryItems;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

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
        for (int i = inventory.items.Count -1; i >= 0; i--)
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

    public void LoadData(GameData _data)
    {
        Debug.Log("Loaded data for " + name);

    }

    public void SaveData(ref GameData _data)
    {
        //serialize inventory
        Dictionary<string, int> inventortoSave = new Dictionary<string, int>();
        foreach (var item in inventory.items)
        {
            inventortoSave.Add(item.itemType.itemName, item.amount);
        }
        string inventoryjson = JsonConvert.SerializeObject(inventortoSave);
        
      // _data.character.Add(name, inventoryjson);
    }

    public int GenerateRandomNumber(int min, int max)
    {
        System.Random random = new System.Random();
        return random.Next(min, max + 1);
    }

}
