using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunGranadeController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy _enemy = collision.gameObject.GetComponent<Enemy>();

        if (_enemy != null)
        {
            
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Ground")
            Destroy(gameObject, 1.5f);
    }
}
