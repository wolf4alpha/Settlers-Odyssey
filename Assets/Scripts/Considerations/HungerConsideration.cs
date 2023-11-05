using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HungerConsideration", menuName = "UtilityAI/Considerations/Hunger Consideration")]
public class HungerConsideration : Consideration
{
    [SerializeField]
    private AnimationCurve responseCurve;
    public override float ScoreConsideration(Villager villager)
    {
        score = responseCurve.Evaluate(Mathf.Clamp01(villager.stats.hunger / 100f));
       // Debug.Log("hunger: " + score);
        return score;
    }
}
