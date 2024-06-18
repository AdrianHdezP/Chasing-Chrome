using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Bullet : Item
{
    public override void UseItem()
    {
        gameManager.PickUpBullets(gameObject);
    }

    public override void SavePowerup()
    {
    }

    public override void UsePowerup()
    {
    }
}
