using UnityEngine;
using UnityEngine.UI;

public class VillagerUi : MonoBehaviour
{

    public Canvas canvas;
    public Camera cam;

    public Text hungerText;
    public Text energyText;
    public Text stateText;
    public Text moneyText;
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
        energyText.text = villager.stats.energy.ToString();
        hungerText.text = villager.stats.hunger.ToString();
        moneyText.text = villager.stats.money.ToString();

        stateText.text = villager.stateMachine.currentState.ToString();
        actionText.text = villager.currentAction;
    }
}
