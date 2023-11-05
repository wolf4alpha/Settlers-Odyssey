using System.Collections;
using UnityEngine;

public class VillagerWorkState : VillagerState
{

    public VillagerWorkState(Villager _villager, VillagerStateMachine _stateMachine, string _animBoolName) : base(_villager, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        Debug.Log("enter work state");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        if(villager.currentAction == null)
        {
           villager.DoWork(5);
        }

        base.Update();
       
    }
    
}

