using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorAnimationSFX : MonoBehaviour
{
    private void Attack()
    {
        AudioManager.instance.PlayOneShoot(FMODEvents.instance.majorAttack, transform.position);
    }
}
