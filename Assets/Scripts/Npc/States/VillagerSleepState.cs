using UnityEngine;
using UnityEngine.AI;

public class VillagerSleepState : VillagerState
{
    private GameObject destination;
    private NavMeshAgent agent;
    private GameObject ground;
    public VillagerSleepState(Villager _villager, VillagerStateMachine _stateMachine, string _animBoolName) : base(_villager, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        villager.DoSleep(4);
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
