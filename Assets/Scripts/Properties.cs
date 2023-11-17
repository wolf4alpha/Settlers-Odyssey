using Assets.Scripts.InventoryManager.InventoryItems;
using UnityEngine;




public class Properties : MonoBehaviour
{

    [SerializeField]
    private ItemData _ressourceType;
    
   // [SerializeField]
    //private int _maxRessource = 100;
    [SerializeField]
    private int _currentRessource = 100;

    public int _maxVillagers = 4;

    public int _currentVillagers = 0;

    public string Action;


    public ItemData getRessource()
    {
        return _ressourceType;
    }

    internal void AssingVillager()
    {
        _currentVillagers++;
    }

    internal void RemoveVillager()
    {
        _currentVillagers--;
    }

    public void RemoveRessource(int amount)
    {
        _currentRessource -= amount;
        if (_currentRessource < 0)
        {
            _currentRessource = 0;
            Destroy(gameObject);
        }
    }



}
