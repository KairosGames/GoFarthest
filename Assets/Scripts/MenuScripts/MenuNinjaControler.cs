using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNinjaControler : MonoBehaviour
{
    [SerializeField] Animator animator;
    private sbyte step;

    void Start()
    {
        step = 0;
    }

    void Update()
    {
        if (transform.position.y < -0.2)
        {
            transform.position = new Vector3(transform.position.x, -0.2f, transform.position.z);
        }
    }

    void IdleEnd()
    {
        if (step == 0)
        {
            animator.SetBool("down", true);
            step++;
        }
        else if (step == 1)
        {
            animator.SetBool("warmup", true);
            step++;
        }
        else if (step == 2)
        {
            animator.SetBool("fight", true);
            step++;
        }
        else if (step == 3)
        {
            animator.SetBool("taunt", true);
        }
    }

    void DownEnd()
    {
        animator.SetBool("down", false);
    }
    void WarmupEnd()
    {
        animator.SetBool("warmup", false);
    }
    void FightEnd()
    {
        animator.SetBool("fight", false);
    }
    void TauntEnd()
    {
        step = 0;
        animator.SetBool("taunt", false);
    }
}
