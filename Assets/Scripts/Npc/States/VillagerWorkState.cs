using UnityEngine;
using UnityEngine.AI;

public class VillagerWorkState : VillagerState
{

    public VillagerWorkState(Villager _villager, VillagerStateMachine _stateMachine, string _animBoolName) : base(_villager, _stateMachine, _animBoolName)
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
