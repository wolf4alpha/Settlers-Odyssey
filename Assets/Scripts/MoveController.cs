using UnityEngine;
using UnityEngine.AI;

public class MoveController : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Awake()
    {
        Debug.Log("move controller awake");
    }
    void Start()
    {
        Debug.Log("move controller start");
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
            Debug.LogError("NavMeshAgent component not found");
    }

    private void Update()
    {

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

