using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorporalAnimationTriggers : MonoBehaviour
{
    private EnemyCorporal enemy => GetComponentInParent<EnemyCorporal>();

    private void AnimationTrigger() => enemy.AnimationFinishTriggers();

    private void AnimationAttack() => enemy.Shoot();
}
