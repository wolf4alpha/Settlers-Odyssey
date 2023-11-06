using UnityEngine;

[CreateAssetMenu(fileName = "Work", menuName = "UtilityAI/Actions/Work")]
public class work : Action
{
    
    public override void Execute( Villager villager)
    {
      //  villager.DoWork(5);
    }

    public override void SetDestination(Villager villager)
    {
       // RequiredDestination = villager.transform;
    }
}
