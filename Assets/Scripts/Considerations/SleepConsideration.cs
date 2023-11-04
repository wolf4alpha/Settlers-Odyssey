using UnityEngine;

[CreateAssetMenu(fileName = "SleepConsideration", menuName = "UtilityAI/Considerations/Sleep Consideration")]
public class SleepConsideration : Consideration
{
    [SerializeField]
    private AnimationCurve responseCurve;
    public override float ScoreConsideration(Villager villager)
    {
        score = responseCurve.Evaluate(Mathf.Clamp01(villager.stats.energy / 100f));
        Debug.Log("sleep: "+score);
        return score;
    }
}

