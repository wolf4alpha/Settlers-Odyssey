using UnityEngine;
using UnityEngine.AI;

public class MovePreview : MonoBehaviour
{
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 position)
    {
        agent.destination = position;
    }

    public float RemainingDistance()
    {
        if (agent.pathPending)
        {
            return 100f;
        }
        
        return agent.remainingDistance;
    }
}

