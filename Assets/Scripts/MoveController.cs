using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveController : MonoBehaviour
{
    private NavMeshAgent agent;

    public MoveController()
    {
        agent = new NavMeshAgent();
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
    
    }

    public void MoveTo(Vector3 position)
    {
        agent.destination = position;
    }
}
    
