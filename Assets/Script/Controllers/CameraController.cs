using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject cameraLight;
    public GameObject cameraLight2;

    private GameManager gameManager;
    private Animator anim;
    private EdgeCollider2D edgeCollider;


    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        anim = GetComponentInParent<Animator>();
        edgeCollider = GetComponent<EdgeCollider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !gameManager.isInvisible)
        {
            anim.SetBool("PlayerDetected", true);
            cameraLight.SetActive(false);
            cameraLight2.SetActive(true);
            AudioManager.instance.PlayOneShoot(FMODEvents.instance.cameraAlarm, transform.position);
            gameManager.SubtractTime(15);
            edgeCollider.enabled = false;
        }
    }
}
