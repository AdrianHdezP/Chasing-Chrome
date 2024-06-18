using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Powerup_StunGranade : Item
{
    public override void UseItem()
    {
    }

    public override void SavePowerup()
    {
        if (gameManager.IsPowerupPickedUp())
            return;

        gameManager.AddPowerupsToTheInventory(gameObject);
        gameObject.SetActive(false);
    }

    public override void UsePowerup()
    {
        gameManager.player.isUsingPowerup = true;

        gameManager.StunGranade();
    }
}
