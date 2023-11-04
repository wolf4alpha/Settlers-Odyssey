using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    
    public int maxHp;
    public int maxSleep;
    public int maxFood;
    public int maxHunger;

    [SerializeField]
    private int currentHealth;
    [SerializeField]
    private int currentSleep;
    [SerializeField]
    private int currentHunger;

    public int currentEnergy; 
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHp;  
        currentSleep = maxSleep;
        currentHunger = maxHunger;
    }

    void TakeDamage()
    {
        currentHealth -= 1;
    }

    public void addHunger(int _hunger) { currentHunger += _hunger; }

    public void removeHunger(int _hunger) { currentHunger -= _hunger; }

    public void AddSleep(int _sleep) { currentSleep += _sleep; }
    public void removeSleep(int _sleep) { currentSleep -= _sleep; }
    public int getSleep() { return currentSleep; }
    public int getHunger() { return currentHunger; }

    
}
