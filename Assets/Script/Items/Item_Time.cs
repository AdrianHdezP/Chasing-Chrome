using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Time : Item
{
    [SerializeField] private int time;

    public override void UseItem()
    {
        gameManager.AddTime(time);
        Destroy(gameObject);
    }

    public override void SavePowerup()
    {
    }

    public override void UsePowerup()
    {
    }
}
