using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenElevatorController : MonoBehaviour
{
    private Animator anim;

    private float timer = 30;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            anim.SetBool("Rat", true);
        }
    }

    public void RatAnimationOver()
    {
        anim.SetBool("Rat", false);

        timer = Random.Range(15, 45);
    }

}
