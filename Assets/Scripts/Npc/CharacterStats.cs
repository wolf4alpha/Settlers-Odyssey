using Newtonsoft.Json;
using UnityEngine;

public class CharacterStats : MonoBehaviour, ISaveManager
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
        // work stats
        //energy = 60;
        //hunger = 60;
        //money = 0;

        // go to eat state

        //generate random stats
        energy = Random.Range(0, maxEnergy);
        hunger = Random.Range(0, maxHunger);
        money = Random.Range(0, maxMoney);

        energy = 100;
        hunger = 100;
        money = 0;
    }

    void TakeDamage()
    {
        // currentHealth -= 1;
    }

    public void addHunger(int _hunger) { hunger += _hunger; }

    public void removeHunger(int _hunger) { hunger -= _hunger; }

    public void AddEnergy(int _sleep) { energy += _sleep; }
    public void RemoveEnergy(int _sleep) { energy -= _sleep; }

    public void LoadData(GameData _data)
    {
        throw new System.NotImplementedException();
    }

    public void SaveData(ref GameData _data)
    {
        //GameDataStats saveStats = new();

        //saveStats.stats.Add("hunger", hunger.ToString());
        //saveStats.stats.Add("energy", energy.ToString());
        //saveStats.stats.Add("money", money.ToString());

        

       // var json = JsonConvert.SerializeObject(saveStats);
       // _data.stats.Add(this.transform.name, json);

    }
}
