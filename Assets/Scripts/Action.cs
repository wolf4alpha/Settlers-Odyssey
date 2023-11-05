using UnityEngine;

public abstract class Action : ScriptableObject
{
    public string Name;
    private float _score;

    public float score
    {
        get { return _score; }
        set
        {
            _score = Mathf.Clamp01(value);
        }
    }

    public Consideration[] considerations;
    
    public Transform RequiredDestination;

    public virtual void Awake()
    {
        score = 0;
    }

    public abstract void Execute(Villager villager); 
    
    public abstract void SetDestination(Villager villager);
}
