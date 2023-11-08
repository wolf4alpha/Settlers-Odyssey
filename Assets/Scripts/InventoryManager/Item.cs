using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "GameStuff/Item")]
public class Item : MonoBehaviour
{
    public int id;
    public string Name;
    public int Amount;
    public string Description;
    public Sprite Icon;
}
