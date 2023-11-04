using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Eat", menuName = "UtilityAI/Actions/Eat")]
public class eat : Action
{
    public override void Execute(Villager villager)
    {
        villager.DoEat(3);
    }


}
