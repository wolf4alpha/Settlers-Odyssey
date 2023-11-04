using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Work", menuName = "UtilityAI/Actions/Work")]
public class work : Action
{
    public override void Execute( Villager villager)
    {
        villager.DoWork(3);
    }


}
