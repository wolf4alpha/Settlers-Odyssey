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

        switch (villager.brain.bestAction.name)
        {
            case "Eat":
                stateMachine.ChangeState(villager.eatState);
                break;
            case "Work":
                stateMachine.ChangeState(villager.workState);
                break;
            case "Sleep":
                stateMachine.ChangeState(villager.sleepState);
                break;
            case "ReturnItems":
                stateMachine.ChangeState(villager.moveState);
                break;
            default:
                stateMachine.ChangeState(villager.moveState);
                break;
        }


    }

    private void findNearestActionDestination()
    {
        var nearestDistance = Mathf.Infinity;
        RessourceProperties nearestProperty = null;

        foreach (RessourceProperties property in Object.FindObjectsOfType<RessourceProperties>())
        {
            if (property.Action == villager.brain.bestAction.name)
            {
                if (property._currentVillagers < property._maxVillagers)
                {
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
            villager.destinationProperties = nearestProperty.GetComponent<RessourceProperties>();
            villager.moveController.MoveTo(nearestProperty.transform.position);
        }
        else
        {
            Debug.Log("no property found");
            villager.DoWait(10);
        }
    }
}
