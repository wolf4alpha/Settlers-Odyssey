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

    //private void moveToBase()
    //{

    //    villager.destination = GameObject.FindGameObjectWithTag("Base");
    //    villager.moveController.MoveTo(villager.destination.transform.position);

    //    if (villager.moveController.TargetReached())
    //    {
    //        Debug.Log("reached base!");
    //        Debug.Log("dropped items to Base");
    //        villager.inventory.RemoveItem(villager.inventory.items[0]);
    //        villager.stateMachine.ChangeState(villager.idleState);
    //    }
    //}

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (villager.moveController.TargetReached())
            villager.stateMachine.ChangeState(villager.idleState);

    }
}
