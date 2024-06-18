using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Player>().ReciveDamage();
            gameObject.SetActive(false);
            Destroy(gameObject);
            return;
        }

        Enemy _enemy = collision.gameObject.GetComponent<Enemy>();  

        if (_enemy != null)
        {
            _enemy.Die();

            if (_enemy.hp <= 0)
                return;

            gameObject.SetActive(false);
            Destroy(gameObject);
            return;
        }

        if (collision.gameObject.tag == "Ground")
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            AudioManager.instance.PlayOneShoot(FMODEvents.instance.bulletImpact, transform.position);
            return;
        }
    }

}
