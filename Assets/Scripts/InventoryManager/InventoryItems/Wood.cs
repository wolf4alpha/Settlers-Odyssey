using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : Item
{
    public Wood()
    {
        id = 0;
        Name = "Wood";
        Amount = 0;
        Description = "Wood is a resource that can be gathered from trees.";
       // Icon = Resources.Load<Sprite>("Images/wood");   
    }
}
