using System.Collections.Generic
;using UnityEngine;
using UnityEngine.AI;

public class AiController : MonoBehaviour
{
    private GameObject destination;
    private Villager villager;
    private NavMeshAgent agent;
    [SerializeField] 
    private List<GameObject> destinationList;

    Animator anim;


    void Start()
    {
        destination = GameObject.FindGameObjectWithTag("Destination");
        villager = GetComponent<Villager>();
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(destination.transform.position);
    }

    private void Update()
    {
        //find next task
        if(villager.stateMachine.currentState == villager.moveState)
        {
            agent.isStopped = false;
        }
        else
        {
            agent.isStopped = true;
        }
        
        if (agent.remainingDistance < 1)
        {
            villager.stateMachine.ChangeState(villager.idleState);
        
        }
        
        if(agent.remainingDistance >= 2 && villager.stateMachine.currentState != villager.moveState)
        {
            villager.stateMachine.ChangeState(villager.moveState);
        }
        //agent.SetDestination(destination.transform.position);

    }


}
