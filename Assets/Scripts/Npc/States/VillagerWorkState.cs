using UnityEngine;
using static UnityEditor.Progress;

public class VillagerWorkState : VillagerState
{

    public VillagerWorkState(Villager _villager, VillagerStateMachine _stateMachine, string _animBoolName) : base(_villager, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
      Debug.Log("enter work state");
        villager.DoWork(5);
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

