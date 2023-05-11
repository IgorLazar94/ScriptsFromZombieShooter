using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponDamage : MonoBehaviour
{
    public LayerMask collisionLayer;
    public float radius = 1f;
    public int damage = 3;

    private void Update ()
    {
        Collider2D target = Physics2D.OverlapCircle(transform.position, radius, collisionLayer);

        if (target)
        {
            if (target.tag == TagManager.ZOMBIE_HEALTH_TAG)   // где он ещё находит тэг?!
            {
                target.transform.root.GetComponent<ZombieController>().DealDamage(damage);

            }
        }

    }







} // class
