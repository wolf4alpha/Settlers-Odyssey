using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Item
{
    public Food()
    {
        id = 2;
        Name = "Food";
        Amount = 0;
        Description = "Food is a resource that can be gathered from animals.";
        // Icon = Resources.Load<Sprite>("Images/food");
    }
}
