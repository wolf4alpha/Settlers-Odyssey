using System.IO.Pipes;
using UnityEngine;

public class VillagerBrain : MonoBehaviour
{
    public Action bestAction {  get; set; }
    public bool finishedDeciding {  get; set; }

    private Villager villager;

    void Start()
    {
        villager = GetComponent<Villager>();
        finishedDeciding = false;
    }

    void Update()
    {
        if(bestAction is null) {
            DecideBestAction(villager.actionsAvailable);
        }
    }

    public void DecideBestAction(Action[] actionsAvailable)
    {
        float score = 0f;
        int nextBestActionIndex = 0;
        for(int i = 0; i < actionsAvailable.Length; i++)
        {
            if (ScoreAction(actionsAvailable[i]) > score) 
            {
                nextBestActionIndex = i;
                score = actionsAvailable[i].score;              
            }
        }

        bestAction = actionsAvailable[nextBestActionIndex];
        finishedDeciding = true; 
    }

    public float ScoreAction(Action action)
    {
        float score = 1f;
        for (int i = 0; i < action.considerations.Length; i++)
        {
            float considerationScore = action.considerations[i].ScoreConsideration();
            score *= considerationScore;

            if(score == 0f)
            {
                action.score = 0;
                return action.score;
            }
        }

        float originalScore = score;
        float modFactor = 1 - (1 / action.considerations.Length);
        float makeupValue = (1 - originalScore) * modFactor;
        action.score = originalScore + (makeupValue * originalScore);
        
        Debug.Log("Action: " + action.name + "got: " + action.score);
        
        return action.score;

    }
}