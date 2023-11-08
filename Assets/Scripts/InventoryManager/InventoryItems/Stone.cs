using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : Item
{
    public Stone()
    {
        id = 1;
        Name = "Stone";
        Amount = 0; 
        Description = "Stone is a resource that can be gathered from rocks.";
        // Icon = Resources.Load<Sprite>("Images/stone");
    }
}
