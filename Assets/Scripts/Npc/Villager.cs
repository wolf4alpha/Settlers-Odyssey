using Assets.Scripts.InventoryManager.InventoryItems;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Villager : MonoBehaviour, IPointerClickHandler
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
    public MoveController moveController { get; private set; }
    public VillagerBrain brain { get; private set; }

    public DynamicInventory inventory { get; private set; }
  //  public InventoryMangerScripableObject inventoryManager;

    public GameObject destination;
    public RessourceProperties destinationProperties;

    public bool lastActionFilledInventory;
    public bool isSelected;

    private GameObject currentWorkplaces;
    private GameObject home;

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
        moveController = GetComponent<MoveController>();
       
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

    //coroutine
    public void DoWork(int time)
    {
        // AssignVilager should get the villager to assing the place where he is working
        //So the villager will have place and the place will have the villager
        destinationProperties.AssingVillager();
        
        StartCoroutine(WorkCoroutine(time));
       //instanciate SO
  
        
       
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

    public void OnFinishedAction()
    {
        
        if (destination != null)
        {
            destinationProperties.RemoveVillager();
        }
        stateMachine.ChangeState(idleState);
    }

    IEnumerator WorkCoroutine(int time)
    {
        int counter = time;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            stats.RemoveEnergy(9);
            stats.removeHunger(4);
            counter--;
        }

        Debug.Log("i harvested 1 ressource");
        destinationProperties.RemoveRessource(1);

     
        ItemInstance itemInstance = new ItemInstance(destinationProperties.getRessource());
        if (inventory.AddItem(itemInstance) == false)
        {
            lastActionFilledInventory = true;
            currentAction = "Move to base";
            //brain.bestAction.RequiredDestination = 
            // drop item on ground?
            // return To nearest storage to drop work items
        }
        else
        {
            OnFinishedAction();
        }
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


    public int GenerateRandomNumber(int min, int max)
    {
        System.Random random = new System.Random();
        return random.Next(min, max + 1);
    }

}
