using UnityEngine;
using UnityEngine.AI;

public class MoveController : MonoBehaviour
{
    private NavMeshAgent agent;
    public float distanceToTarget;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if(agent == null)
        {
            Debug.LogError("No NavMeshAgent found on " + gameObject.name);
        }
    }

    public void Update()
    {
        distanceToTarget = RemainingDistance();
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

    public bool TargetReached()
    {
        if(RemainingDistance() < 3)
            if (agent.pathStatus == NavMeshPathStatus.PathComplete)
                if (agent.remainingDistance < 1)
                {
                    return true;
                }
        return false;
    }
}

