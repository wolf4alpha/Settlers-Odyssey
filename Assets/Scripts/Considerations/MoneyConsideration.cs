using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MoneyConsideration", menuName = "UtilityAI/Considerations/Money Consideration")]
public class MoneyConsideration : Consideration
{
    [SerializeField]
    private AnimationCurve responseCurve;
    public override float ScoreConsideration(Villager villager)
    {
        score = responseCurve.Evaluate(Mathf.Clamp01(villager.stats.money / villager.stats.maxMoney));
        //Debug.Log("money: " + score);
        return score;
    }
}
