using System.Collections;
using UnityEngine;

public class VillagerWorkState : VillagerState
{

    public VillagerWorkState(Villager _villager, VillagerStateMachine _stateMachine, string _animBoolName) : base(_villager, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        //if (villager.inventory.items[0].Amount == 10)
        //{
        //    villager.brain.bestAction.RequiredDestination = GameObject.Find("Base").transform;
        //    villager.moveController.MoveTo(villager.brain.bestAction.RequiredDestination.position);
        //    villager.currentAction = "Move to base";
        //    villager.stateMachine.ChangeState(villager.moveState);
        //    Debug.Log("move to base");
            
        //}
        //else
        //{
            villager.DoWork(2);
        //}
        
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
                //villager.inventory.RemoveItem(0, 5);
                Debug.Log("dropped 5 Wood to Base");
                villager.stateMachine.ChangeState(villager.idleState);
            }
        }
        

    }
    
}

