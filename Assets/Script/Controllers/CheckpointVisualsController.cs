using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointVisualsController : MonoBehaviour
{
    private Animator anim;

    private float timer = 10;
    private int randomAnim;

    private bool use = false;

    private void Start()
    {
        anim = GetComponent<Animator>();

        RandomAnimation();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (!use)
            {
                if (randomAnim == 0)
                    anim.SetBool("Idle_1", true);
                else if (randomAnim == 1)
                    anim.SetBool("Idle_2", true);
            }
            else
            {
                anim.SetBool("Variation", true);
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            anim.SetBool("Use", true);
            use = true;
        }
    }

    public void AnimationOver()
    {
        if (!use)
        {
            anim.SetBool("Idle_1", false);
            anim.SetBool("Idle_2", false);

            timer = Random.Range(5, 10);

            RandomAnimation();
        }
        else
        {
            anim.SetBool("Variation", false);

            timer = Random.Range(5, 10);
        } 
    }

    private void RandomAnimation() => randomAnim = Random.Range(0, 2);
}
