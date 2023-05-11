using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimation : MonoBehaviour
{

    private Animator anim;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Attack ()
    {
        anim.SetTrigger(TagManager.ATTACK_PARAMETER);
    }

    public void Hurt ()
    {
        anim.SetTrigger(TagManager.GET_HURT_PARAMETER); 
    }
    public void Dead()
    {
        anim.SetTrigger(TagManager.DEAD_PARAMETER);
    }


























}
