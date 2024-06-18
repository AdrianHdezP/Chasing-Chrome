using UnityEngine;

public class PlayerAnimationsTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger() => player.AnimationTriggers();

    //private void AnimationKnockback()
    //{
    //    Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

    //    foreach (var hit in colliders)
    //    {
    //        if (hit.GetComponent<Enemy>() != null)
    //        {
    //            hit.GetComponent<Enemy>().Kncockback();
    //        }
    //    }
    //}

    private void AnimationAttack() => player.Attack();

    private void AnimationShoot() => player.AnimationShoot();   

    private void UseStunGranade() => player.UseStunGrande();
}
