using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HungerConsideration", menuName = "UtilityAI/Considerations/Hunger Consideration")]
public class HungerConsideration : Consideration
{
    public override float ScoreConsideration()
    {
        return 0.2f;
    }
}
