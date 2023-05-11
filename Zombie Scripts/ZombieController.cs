using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZombieController : MonoBehaviour
{
    private ZombieMovement zombie_Movement;
    private ZombieAnimation zombie_Animation;

    private Transform targetTransform;
    private bool canAttack;
    private bool zombie_Alive;

    public GameObject damage_Collider;

    public int zombieHealth = 10;
    public GameObject[] fxDead;

    private float timerAttack;
    private int fireDamage = 10;
    public GameObject coinCollectable;
    public GameObject health_Pack;
    

    void Start ()
    {
        zombie_Movement = GetComponent<ZombieMovement>();
        zombie_Animation = GetComponent<ZombieAnimation>();
        zombie_Alive = true;

        if (GameplayController.instance.zombieGoal == GameplayController.ZombieGoal.PLAYER)
        {

            targetTransform = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;

        } else if (GameplayController.instance.zombieGoal == GameplayController.ZombieGoal.FENCE)
        {

            GameObject[] fences = GameObject.FindGameObjectsWithTag(TagManager.FENCE_TAG);
            targetTransform = fences[Random.Range(0, fences.Length)].transform;

        }



    }


    void Update ()
    {
        if (zombie_Alive)
        {
            CheckDistance();
        }
    }

     void CheckDistance()
    {
        

        if (targetTransform)
        {
            if (Vector3.Distance(targetTransform.position, transform.position) > 1.5f)  // 1.5f горизонталь дистация урона зомби до игрока
            {
                zombie_Movement.Move(targetTransform); // зомби может ходить тут!
            }  else
            {
                // постоянная дистация, не переходит в атаку
                if (canAttack)
                {
                    zombie_Animation.Attack();
                    timerAttack += Time.deltaTime;
                    if (timerAttack > 0.6f)   // интервал атаки зомбаря
                    {
                        timerAttack = 0;
                        AudioManager.instance.ZombieAttackSound(); 

                    }
                }
            }
        }
    }

    public void ActivateDeadEffect (int index)
    {
        fxDead[index].SetActive(true);

        if (fxDead[index].GetComponent<ParticleSystem>())
        {
            fxDead[index].GetComponent<ParticleSystem>().Play();
        }
    }


    IEnumerator DeactivateZombie()
    {
        AudioManager.instance.ZombieDieSound();
        //if (zombieHealth <= 0) {
        //    gameObject.GetComponent<BoxCollider2D>().enabled = false;
        //    GameObject.FindGameObjectWithTag("ZombieHealth").GetComponent<BoxCollider2D>().enabled = false;
        //    GameObject.FindWithTag("ZombieHealth").GetComponent<BoxCollider2D>().enabled = false;
        //}

        yield return new WaitForSeconds(2f);
        GameplayController.instance.ZombieDied();

        if (Random.Range(0,10) > 5)
        {
        Instantiate(coinCollectable, transform.position, Quaternion.identity);
        }

        if (Random.Range(0, 10) > 8)
        {
            Instantiate(health_Pack, transform.position, Quaternion.identity);
        }

        gameObject.SetActive(false);   //  Должно быть включено
    }

    public void DealDamage (int damage)
    {
        zombie_Animation.Hurt();
        zombieHealth -= damage;

        if (zombieHealth <= 0)
        {
            zombie_Alive = false;
            zombie_Animation.Dead();
            StartCoroutine(DeactivateZombie());
        }
    }

    void ActivateDamagePoint ()
    {
        damage_Collider.SetActive(true);
    }

    void DeactivateDamagePoint ()
    {
        damage_Collider.SetActive(false);
    }





    void OnTriggerEnter2D (Collider2D target)
    {
        if(target.tag == TagManager.PLAYER_HEALTH_TAG || target.tag == TagManager.PLAYER_TAG || target.tag == TagManager.FENCE_TAG)
        {

            canAttack = true;
        }   

        if (target.tag == TagManager.BULLET_TAG || target.tag == TagManager.ROCKET_MISSILE_TAG)
        {
            zombie_Animation.Hurt();
            zombieHealth -= target.gameObject.GetComponent<BulletController>().damage;


            if (target.tag == TagManager.ROCKET_MISSILE_TAG)
            {
                target.gameObject.GetComponent<BulletController>().ExplosionFX();
            }

            if (zombieHealth <= 0)
            {

                zombie_Alive = false;
                zombie_Animation.Dead();

                StartCoroutine(DeactivateZombie());
            }

            target.gameObject.SetActive(false);
        }

        if (target.tag == TagManager.FIRE_BULLET_TAG)
        {
            zombie_Animation.Hurt();
            zombieHealth -= fireDamage;

            if (zombieHealth <= 0)
            {
                zombie_Alive = false;
                zombie_Animation.Dead();
                StartCoroutine(DeactivateZombie());
            }

        }


    }

   

    void OnTriggerExit2D(Collider2D target)
    {
        if (zombie_Alive)
        {
            if (target.tag == TagManager.PLAYER_HEALTH_TAG || target.tag == TagManager.PLAYER_TAG || target.tag == TagManager.FENCE_TAG)
            {
                canAttack = false;
            }
        }

        
    }


    



}
