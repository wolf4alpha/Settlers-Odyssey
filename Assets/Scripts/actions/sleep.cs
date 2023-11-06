using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sleep", menuName = "UtilityAI/Actions/Sleep")]
public class sleep : Action
{
    public override void Execute(Villager villager)
    {
      //  villager.DoSleep(5);
    }

    public override void SetDestination(Villager villager)
    {
        RequiredDestination = villager.destinations["sleep"].transform;
        Debug.Log("sleep destination set" + RequiredDestination);
    }
}
