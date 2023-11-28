using UnityEngine;
using static UnityEditor.Progress;

public class VillagerWorkState : VillagerState
{

    public VillagerWorkState(Villager _villager, VillagerStateMachine _stateMachine, string _animBoolName) : base(_villager, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        villager.DoWork(2);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (villager.currentAction == "Move to base")
        {
            if (villager.moveController.RemainingDistance() < 2f)
            {
                Debug.Log("reached base!");
                Debug.Log("dropped 5 Wood to Base");

                villager.stateMachine.ChangeState(villager.idleState);
            }
        }
        

    }
    
}

