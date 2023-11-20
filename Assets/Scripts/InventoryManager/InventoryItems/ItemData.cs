using UnityEngine;

namespace Assets.Scripts.InventoryManager.InventoryItems
{
    [CreateAssetMenu]
    public class ItemData : ScriptableObject
    {
        public int itemId;
        public string itemName;
        public Sprite icon;
        public GameObject model;
        [TextArea]
        public string description;        

        public bool stackable;
        
    }

}
