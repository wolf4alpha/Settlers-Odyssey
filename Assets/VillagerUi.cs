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

    public Text inventoryText;

    public Villager villager;

    
   
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //if key "H" is pressed, toggle the canvas
        if (Input.GetKeyDown(KeyCode.H))
        {
            canvas.enabled = !canvas.enabled;
        }
        canvas.transform.forward = cam.transform.forward;
        energyText.text = villager.stats.energy.ToString();
        hungerText.text = villager.stats.hunger.ToString();
        moneyText.text = villager.stats.money.ToString();

        stateText.text = villager.stateMachine.currentState.ToString();
        actionText.text = villager.currentAction;
        inventoryText.text = villager.inventory.items[0].Name + " " + villager.inventory.items[0].Amount+
                            "\n" + villager.inventory.items[1].Name + " " + villager.inventory.items[1].Amount +
                           "\n" + villager.inventory.items[2].Name + " " + villager.inventory.items[2].Amount;
    }
}
