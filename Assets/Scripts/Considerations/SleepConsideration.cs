using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SleepConsideration", menuName = "UtilityAI/Considerations/Sleep Consideration")]
public class SleepConsideration : Consideration
{
    public override float ScoreConsideration()
    {
        return 0.1f;
    }
}

