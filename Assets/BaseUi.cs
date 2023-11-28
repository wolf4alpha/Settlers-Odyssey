using UnityEngine;
using UnityEngine.UI;

public class BaseUI : MonoBehaviour
{

    public Canvas canvas;
    public Camera cam;
    
    public Text inventoryText;
    //public InventoryManager inventory;

    public void Awake()
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
        //inventoryText.text = inventory.items[0].Name + " " + inventory.items[0].Amount + "\n" +
        //            inventory.items[1].Name + " " + inventory.items[1].Amount + "\n" +
        //            inventory.items[2].Name + " " + inventory.items[2].Amount;
    }                       
}
