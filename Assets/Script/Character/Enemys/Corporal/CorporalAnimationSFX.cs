using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorporalAnimationSFX : MonoBehaviour
{
    private void Shoot()
    {
        AudioManager.instance.PlayOneShoot(FMODEvents.instance.corporalShoot, transform.position);
    }
}
