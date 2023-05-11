using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int health = 100;
    [HideInInspector] int bonusHealth = 30;
    public GameObject[] blood_FX;
    private PlayerAnimations playerAnim;

    //public delegate void PlayerDeadEvent(bool dead);
    //public static event PlayerDeadEvent PlayerDead;
    public void AddHealth()
    {
        health += bonusHealth;
        GameplayController.instance.PlayerLifeCounter(health);

    }
    void Awake()
    {
        playerAnim = GetComponentInParent<PlayerAnimations>();
    }

    void Update()
    {

    }


    public void DealDamage(int damage)
    {

        health -= damage;

        GameplayController.instance.PlayerLifeCounter(health);

        playerAnim.Hurt();

        if (health <= 0)
        {
            //if (PlayerDead != null)
            //{
            //    PlayerDead(true);
            //}

            GameplayController.instance.playerAlive = false;
            GetComponent<Collider2D>().enabled = false;
            playerAnim.Dead();
            blood_FX[Random.Range(0, blood_FX.Length)].SetActive(true);
            GameplayController.instance.PlayerDie();
        }
    }
}
