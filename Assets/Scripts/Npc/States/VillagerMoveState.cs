using UnityEngine;
using UnityEngine.AI;

public class VillagerMoveState : VillagerState
{
    private GameObject destination;
    private NavMeshAgent agent;
    private GameObject ground;
    public VillagerMoveState(Villager _villager, VillagerStateMachine _stateMachine, string _animBoolName) : base(_villager, _stateMachine, _animBoolName)
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

        if (villager.moveController.RemainingDistance() < 2) 
        {
          if(villager.brain.bestAction.name == "ReturnItems")
            {
                villager.DoTransferItems(2);
            }
            villager.stateMachine.ChangeState(villager.idleState);
        }

    }
}
