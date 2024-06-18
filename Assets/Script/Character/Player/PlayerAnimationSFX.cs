using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerAnimationSFX : MonoBehaviour
{
    private void AttackOne()
    {
        AudioManager.instance.PlayOneShoot(FMODEvents.instance.playerAttack1, transform.position);
    }

    private void AttackTwo()
    {
        AudioManager.instance.PlayOneShoot(FMODEvents.instance.playerAttack2, transform.position);
    }

    private void AttackThree()
    {
        AudioManager.instance.PlayOneShoot(FMODEvents.instance.playerAttack3, transform.position);
    }

    private void Shoot()
    {
        AudioManager.instance.PlayOneShoot(FMODEvents.instance.playerShoot, transform.position);
    }
}
