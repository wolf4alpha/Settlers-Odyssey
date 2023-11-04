using UnityEngine;
using UnityEngine.AI;

public class VillagerEatState : VillagerState
{
    private GameObject destination;
    private NavMeshAgent agent;
    private GameObject ground;
    public VillagerEatState(Villager _villager, VillagerStateMachine _stateMachine, string _animBoolName) : base(_villager, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
