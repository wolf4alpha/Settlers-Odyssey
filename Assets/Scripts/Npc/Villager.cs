using System.Collections;
using UnityEngine;


public class Villager : MonoBehaviour
{
    #region states
    public VillagerStateMachine stateMachine {  get; private set; }
    public VillagerIdleState idleState { get; private set; }
    public VillagerMoveState moveState { get; private set; }
    public VillagerWorkState workState { get; private set; }
    public VillagerSleepState sleepState { get; private set; }
    public VillagerEatState eatState { get; private set; }
    #endregion
    public Animator animator { get; private set; }
    public CharacterStats characterStats { get; private set; }
    public VillagerBrain brain { get; private set; }

    
    public MoveController moveController { get; private set; }

    public Action[] actionsAvailable;

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


        characterStats = GetComponent<CharacterStats>();
       
        moveController = GetComponent<MoveController>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        stateMachine.Initialize(idleState);
        brain = GetComponent<VillagerBrain>();
  
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.Update();
        if(brain.finishedDeciding == true) {
            brain.finishedDeciding = false;
            brain.bestAction.Execute(this);
            currentAction = brain.bestAction.Name;
        }

        UpdateStats();
    }

    private void UpdateStats()
    {
        
    }

    

    //coroutine

    public void DoWork(int time)
    {
        Debug.Log("i go to work");
        stateMachine.ChangeState(workState);
        StartCoroutine(WorkCoroutine(time));
    }

    public void DoSleep(int time)
    {
        this.stateMachine.ChangeState(sleepState);
        StartCoroutine(SleepCoroutine(time));
    }

    public void DoEat(int time)
    {
        this.stateMachine.ChangeState(sleepState);
        StartCoroutine(EatCoroutine(time));
    }

    public void OnFinishedAction()
    {
        brain.DecideBestAction(actionsAvailable);
    } 

    IEnumerator WorkCoroutine(int time)
    {
        int counter = time;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
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

        Debug.Log("i got 1 Energy from sleep");
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

        Debug.Log("i got 1 Hunger from eat");
        OnFinishedAction();
    }



}
