using System;
using System.Collections;
using System.Collections.Generic;
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
        //energy = maxEnergy;
        //hunger = maxHunger;
        //money = 0;
        
        // go to eat state
        energy = 100;
        hunger = 0;
        money = 900;
    }

    void TakeDamage()
    {
       // currentHealth -= 1;
    }

    public void addHunger(int _hunger) { hunger += _hunger; }

    public void removeHunger(int _hunger) { hunger -= _hunger; }

    public void AddEnergy(int _sleep) { energy += _sleep; }
    public void RemoveEnergy(int _sleep) { energy -= _sleep; }
    

}
