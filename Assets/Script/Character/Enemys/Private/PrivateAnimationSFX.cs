using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivateAnimationSFX : MonoBehaviour
{
    private void Attack()
    {
        AudioManager.instance.PlayOneShoot(FMODEvents.instance.privateAttack, transform.position);
    }
}
