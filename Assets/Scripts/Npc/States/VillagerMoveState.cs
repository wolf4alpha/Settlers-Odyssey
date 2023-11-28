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
        if(villager.currentAction == "Move to base")
        {
           
            if (villager.moveController.RemainingDistance() < 2)
            {
                Debug.Log("reached base!");
              //  villager.inventory.RemoveItem(0, 5);
              //  villager.brain.bestAction.RequiredDestination.GetComponent<InventoryManager>().AddItem(0, 5);
             //   villager.inventoryManager.AddWood(5);
                foreach(var item in villager.inventory.items)
                {

                }
                Debug.Log("added 5 Wood to Base");
                villager.stateMachine.ChangeState(villager.idleState);
                
            }
        }

       

        if (villager.moveController.RemainingDistance() < 2 && villager.currentAction!="Move to base") 
        {
            villager.stateMachine.ChangeState(villager.idleState);
        }

    }
}
