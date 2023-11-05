using UnityEngine;

public class VillagerState 
{
    public VillagerStateMachine stateMachine;
    public Villager villager;
    public string animBoolName;

    public VillagerState(Villager _villager, VillagerStateMachine _stateMachine, string _animBoolName)
    {
        villager = _villager;
        stateMachine = _stateMachine;
        animBoolName = _animBoolName;
    }

    public virtual void Enter() {
        
        villager.animator.SetBool(animBoolName, true);
        Debug.Log("change state to: "+animBoolName);

    }

    public virtual void Update() { 
    
    }

    public virtual void Exit() {
        villager.animator.SetBool(animBoolName, false);
    }
}
