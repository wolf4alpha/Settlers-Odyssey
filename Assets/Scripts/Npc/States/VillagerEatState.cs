using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Video;

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
        villager.DoEating(3);
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
