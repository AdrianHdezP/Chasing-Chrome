using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Money : Item
{
    public override void UseItem()
    {
        gameManager.AddScrap();
        Destroy(gameObject);
    }

    public override void SavePowerup()
    {
    }

    public override void UsePowerup()
    {
    }
}
