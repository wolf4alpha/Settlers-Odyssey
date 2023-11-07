using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Properties : MonoBehaviour
{

    [SerializeField]
    private string _ressourceType;

    public int _maxVillagers  = 4;

    public int _currentVillagers  = 0;

    public string Action;

    public int currentVillagersbla;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void AssingVillager()
    {
        _currentVillagers++;
    }

    internal void RemoveVillager()
    {
        _currentVillagers--;
    }
}
