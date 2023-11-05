using System;
using Unity;
using UnityEngine;

public class VillagerIdleState : VillagerState
{

    public VillagerIdleState(Villager _villager, VillagerStateMachine _stateMachine, string _animBoolName) : base(_villager, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        villager.brain.DecideBestAction();
        
        //meh
        villager.brain.bestAction.SetDestination(villager);
        villager.moveController.MoveTo(villager.brain.bestAction.RequiredDestination.position);

        if (villager.moveController.RemainingDistance() < 2f)
        {
            Debug.Log("reached destination distance: "+ villager.moveController.RemainingDistance());
            stateMachine.ChangeState(villager.workState);
        }
        else
        {
            Debug.Log("move to destination");
            villager.stateMachine.ChangeState(villager.moveState);
        }

    }
}
