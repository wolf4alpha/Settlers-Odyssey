using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Eat", menuName = "UtilityAI/Actions/Eat")]
public class eat : Action
{
    public override void Execute(Villager villager)
    {
        //        SetDestination(villager);
        //            villager.DoEat(5);
    }

    public override void SetDestination(Villager villager)
    {
        RequiredDestination = villager.destinations["eat"].transform;
    }
}
