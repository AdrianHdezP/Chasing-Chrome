using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Points : Item
{
    [SerializeField] private int points;

    public override void UseItem()
    {
        gameManager.AddCredits(points);
        Destroy(gameObject);
    }

    public override void SavePowerup()
    {
    }

    public override void UsePowerup()
    {
    }
}

