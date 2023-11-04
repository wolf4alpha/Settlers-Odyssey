using UnityEngine;
using UnityEngine.UI;

public class VillagerUi : MonoBehaviour
{

    public Canvas canvas;
    public Camera cam;

    public Text hungerText;
    public Text sleepText;
    public Text stateText;
    public Text actionText;

    public Villager villager;

    
   
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        canvas.transform.forward = cam.transform.forward;
        sleepText.text = villager.characterStats.getSleep().ToString();
        hungerText.text = villager.characterStats.getHunger().ToString();
        stateText.text = villager.stateMachine.currentState.ToString();
        actionText.text = villager.currentAction;
    }
}
