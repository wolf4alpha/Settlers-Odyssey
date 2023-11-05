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
        destination = GameObject.FindGameObjectWithTag("Destination");
        ground = GameObject.FindGameObjectWithTag("Ground");
        
        
        villager.moveController.MoveTo(villager.brain.bestAction.RequiredDestination.position);
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
            villager.stateMachine.ChangeState(villager.idleState);
        }

    }
}
