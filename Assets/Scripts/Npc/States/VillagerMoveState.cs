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
        destination = GameObject.FindGameObjectWithTag("Destination");
        ground = GameObject.FindGameObjectWithTag("Ground");
      //  agent = ground.GetComponentInChildren<NavMeshAgent>();
      //  agent.SetDestination(destination.transform.position);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        
        //villager.characterStats.removeEnergy(1);

        //if (agent.remainingDistance < 1) 
        //{
        //    villager.stateMachine.ChangeState(villager.idleState);
        //}

    }
}
