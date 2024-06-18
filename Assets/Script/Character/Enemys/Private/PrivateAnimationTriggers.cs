using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivateAnimationTriggers : MonoBehaviour
{
    private EnemyPrivate enemy => GetComponentInParent<EnemyPrivate>();

    private void AnimationTrigger() => enemy.AnimationFinishTriggers();

    private void AnimationAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
                hit.GetComponent<Player>().ReciveDamage();
        }
    }

}
