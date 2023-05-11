using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDamage : MonoBehaviour
{

    public LayerMask collisionLayer;
    public float radius = 1.0f;
    public int damage = 3;

    //private bool playerDead;

    // void OnEnable ()
    //{
    //    PlayerHealth.PlayerDead += PlayerDeadListener;
    //}

    //void OnDisable()
    //{
    //    PlayerHealth.PlayerDead -= PlayerDeadListener;
    //}
    public void FixedUpdate()
    {
        if (GameplayController.instance.zombieGoal == GameplayController.ZombieGoal.PLAYER)
        {
            AttackPlayer();
        }
        if (GameplayController.instance.zombieGoal == GameplayController.ZombieGoal.FENCE)
        {
            AttackFence();
        }



    }

    void AttackPlayer ()
    {
        if (GameplayController.instance.playerAlive)
        {
            //if (!playerDead)
            //{
                Collider2D target = Physics2D.OverlapCircle(transform.position, radius, collisionLayer);

                if (target.tag == TagManager.PLAYER_HEALTH_TAG)
                {
                    target.GetComponent<PlayerHealth>().DealDamage(damage);
                }
            //}
        }
    }

    void AttackFence ()
    {
        if (!GameplayController.instance.fenceDestroyed)
        {
            Collider2D target = Physics2D.OverlapCircle(transform.position, radius, collisionLayer);

            if (target.tag == TagManager.FENCE_TAG)
            {
                target.GetComponent<FenceHealth>().DealDAmage(damage);
            }
        }
    }


    //void PlayerDeadListener (bool dead)
    //{
    //    playerDead = dead;
    //}
} // class
