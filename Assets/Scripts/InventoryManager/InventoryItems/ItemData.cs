using UnityEngine;

namespace Assets.Scripts.InventoryManager.InventoryItems
{
    [CreateAssetMenu]
    public class ItemData : ScriptableObject
    {
        public string itemName;
        public Sprite icon;
        public GameObject model;
        [TextArea]
        public string description;
        public int startingAmmo;
        public int startingCondition;
    }

}
