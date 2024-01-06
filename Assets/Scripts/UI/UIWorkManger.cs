using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWorkManger : MonoBehaviour
{
    [SerializeField]
    private GameObject workListPanel;

    [SerializeField]
    private GameObject workListItemPrefab;

    [SerializeField]
    private List<Villager> villagerList;


    private void Start()
    {
        villagerList = new List<Villager>();
        villagerList.AddRange(FindObjectsOfType<Villager>());
        foreach (Villager villager in villagerList)
        {
            GameObject newObject = Instantiate(workListItemPrefab);
            //set text of child textfield from newobjecet to villager.name
            newObject.GetComponentInChildren<UnityEngine.UI.Text>().text = villager.name;
            newObject.GetComponent<UIWorkListItem>().setVillager(villager);
            
            newObject.transform.SetParent(workListPanel.transform);
            newObject.transform.localScale = Vector3.one;

        }

        
    }
}
