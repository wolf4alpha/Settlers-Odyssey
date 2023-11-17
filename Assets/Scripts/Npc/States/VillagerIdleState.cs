using UnityEngine;

public class VillagerIdleState : VillagerState
{

    public VillagerIdleState(Villager _villager, VillagerStateMachine _stateMachine, string _animBoolName) : base(_villager, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        villager.currentAction = null;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        villager.brain.DecideBestAction();

        // find nearest destination in future
        findNearestActionDestination();

        if (villager.moveController.RemainingDistance() < 2f)
        {


            if (villager.brain.bestAction.name == "Eat")
            {
                //    Debug.Log("change state to eat");
                stateMachine.ChangeState(villager.eatState);
            }

            if (villager.brain.bestAction.name == "Work")
            {
                //    Debug.Log("change state to work");
                stateMachine.ChangeState(villager.workState);
            }

            if (villager.brain.bestAction.name == "Sleep")
            {
                //    Debug.Log("change state to sleep");
                stateMachine.ChangeState(villager.sleepState);
            }

        }
        else
        {
            villager.stateMachine.ChangeState(villager.moveState);
        }
    }

    private void findNearestActionDestination()
    {
        var nearestDistance = Mathf.Infinity;
        Properties nearestProperty = null;

        foreach (Properties property in Object.FindObjectsOfType<Properties>())
        {
            if (property.Action == villager.brain.bestAction.name)
            {
                if(property._currentVillagers < property._maxVillagers) { 
                    float distance = Vector3.Distance(property.transform.position, villager.transform.position);
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestProperty = property;
                    }
                }
            }
        }
        
        
  
        if (nearestProperty != null)
        {
            Debug.Log("nearest property selected: " + nearestProperty?.name + "with distance: " + nearestDistance);
            villager.destination = nearestProperty.gameObject;
            villager.destinationProperties = nearestProperty.GetComponent<Properties>();
            villager.moveController.MoveTo(nearestProperty.transform.position);
        }
        else
        {
            Debug.Log("no property found");
            villager.DoWait(10);
        }
    }
}
