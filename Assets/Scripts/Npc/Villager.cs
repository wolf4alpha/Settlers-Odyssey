using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;




public class Villager : MonoBehaviour
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

    public InventoryManager inventory { get; private set; }

    public GameObject destination;

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
        inventory = GetComponent<InventoryManager>();
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


    //coroutine

    public void DoWork(int time)
    {
        // AssignVilager should get the villager to assing the place where he is working
        //So the villager will have place and the place will have the villager
        destination.GetComponentInParent<Properties>().AssingVillager();
        StartCoroutine(WorkCoroutine(time));
        int amount = GenerateRandomNumber(6, 12);
        destination.GetComponentInParent<Properties>().RemoveRessource(amount);
        inventory.AddItem(destination.GetComponent<Properties>().RessourceID(), amount);
    }

    public void DoSleep(int time)
    {
        destination.GetComponentInParent<Properties>().AssingVillager();
        StartCoroutine(SleepCoroutine(time));
    }

    public void DoEating(int time)
    {
        destination.GetComponentInParent<Properties>().AssingVillager();
        StartCoroutine(EatCoroutine(time));
    }

    public void DoWait(int time)
    {
        StartCoroutine(WaitCoroutine(time));
    }

    public void OnFinishedAction()
    {
        stateMachine.ChangeState(idleState);
        if (destination != null)
        {
            destination.GetComponentInParent<Properties>().RemoveVillager();
        }
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


    public int GenerateRandomNumber(int min, int max)
    {
        System.Random random = new System.Random();
        return random.Next(min, max + 1);
    }

}
