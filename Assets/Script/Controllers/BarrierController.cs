using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    private GameManager gameManager;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && gameManager.isDoingTackle == true)
            AudioManager.instance.PlayOneShoot(FMODEvents.instance.barrier, transform.position);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && gameManager.isDoingTackle == true)
        {
            boxCollider.enabled = false;
            anim.SetBool("Destroy", true); 
        }
    }
}
