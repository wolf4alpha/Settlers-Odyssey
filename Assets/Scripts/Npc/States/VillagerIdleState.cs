using UnityEngine;

public class VillagerIdleState : VillagerState
{

    public VillagerIdleState(Villager _villager, VillagerStateMachine _stateMachine, string _animBoolName) : base(_villager, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        villager.currentAction = null;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        villager.brain.DecideBestAction();
        Debug.Log("best action: " + villager.brain.bestAction.name);

        // find nearest destination in future
        villager.brain.bestAction.SetDestination(villager);

        villager.moveController.MoveTo(villager.brain.bestAction.RequiredDestination.position);


        if (villager.moveController.RemainingDistance() < 2f)
        {
           
            Debug.Log("villager.brain.bestAction.name: " + villager.brain.bestAction.name);
            if (villager.brain.bestAction.name == "Eat")
            {
                Debug.Log("change state to eat");
                stateMachine.ChangeState(villager.eatState);
            }

            if (villager.brain.bestAction.name == "Work")
            {
                Debug.Log("change state to work");
                stateMachine.ChangeState(villager.workState);
            }

            if (villager.brain.bestAction.name == "Sleep")
            {
                Debug.Log("change state to sleep");
                stateMachine.ChangeState(villager.sleepState);
            }

        }
        else
        {
            villager.stateMachine.ChangeState(villager.moveState);
        }
    }
}
