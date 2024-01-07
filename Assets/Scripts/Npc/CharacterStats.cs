using Newtonsoft.Json;
using UnityEngine;

public class CharacterStats : MonoBehaviour 
{

    public int maxEnergy;
    public int maxHunger;
    public int maxMoney;

    public int energy { get; private set; }
    public int hunger { get; private set; }
    public int money { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        //stats will be loaded from savegame just for debuging 
        //energy = 100;
        //hunger = 100;
        //money = 0;
    }

    void TakeDamage()
    {
        // currentHealth -= 1;
    }

    public void addHunger(int _hunger) { hunger += _hunger; }

    public void removeHunger(int _hunger) { hunger -= _hunger; }

    public void AddEnergy(int _sleep) { energy += _sleep; }
    public void RemoveEnergy(int _sleep) { energy -= _sleep; }

    internal void SetHunger(int v) => hunger = v;
    internal void SetEnergy(int v) => energy = v;
    internal void SetMoney(int v) => money = v;

    

}
