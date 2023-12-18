using UnityEngine;

[CreateAssetMenu(fileName = "ReturnItemsConsideration", menuName = "UtilityAI/Considerations/Return Items")]
public class ReturnItemsConsideration : Consideration
{
    [SerializeField]
    private AnimationCurve responseCurve;
    public override float ScoreConsideration(Villager villager)
    {
        score = responseCurve.Evaluate(Mathf.Clamp01(villager.inventory.InventoryCapacity() / 100f));
        return score;
    }
}
