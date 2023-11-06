using System.Collections;
using System.Collections.Generic;
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
   
    public Dictionary<string, GameObject> destinations;

    public MoveController moveController { get; private set; }
    public VillagerBrain brain { get; private set; }


    private GameObject workDestination;
    private GameObject sleepDestination;
    private GameObject eatDestination;



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

        moveController = GetComponent<MoveController>();
      
    }  

    private void Start()
    {
        animator = GetComponent<Animator>();
        
        brain = GetComponent<VillagerBrain>();
        stats.maxEnergy = 100;
        workDestination = GameObject.Find("WorkDestination");
        sleepDestination = GameObject.Find("SleepDestination");
        eatDestination = GameObject.Find("EatDestination");
        stateMachine.Initialize(idleState);

        destinations = new Dictionary<string, GameObject>
        {
            { "work", workDestination },
            { "sleep", sleepDestination },
            { "eat", eatDestination }
        };


        
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }


    //coroutine

    public void DoWork(int time)
    {
        StartCoroutine(WorkCoroutine(time));
    }

    public void DoSleep(int time)
    {
        StartCoroutine(SleepCoroutine(time));
    }

    public void DoEating(int time)
    {
        StartCoroutine(EatCoroutine(time));
    }

    public void OnFinishedAction()
    {
        Debug.Log("finished action");
        stateMachine.ChangeState(idleState);
        
        //go back to idle state
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

        Debug.Log("i got 100 Energy, lost 10 Hunger from sleep");
        stats.AddEnergy(70);
        stats.removeHunger(2);
        OnFinishedAction();
    }

    IEnumerator EatCoroutine(int time)
    {
       Debug.Log("i am eating");
        
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



}
