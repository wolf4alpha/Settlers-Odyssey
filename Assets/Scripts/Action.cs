using UnityEngine;

public abstract class Action : ScriptableObject
{
    public string Name;
    public string fsmState;
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
    public GameObject Target;


    public virtual void Awake()
    {
        score = 0;
    }

}
