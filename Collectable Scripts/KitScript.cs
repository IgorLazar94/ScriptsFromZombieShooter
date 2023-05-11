using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitScript : MonoBehaviour
{
    PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GameObject.Find("Health").GetComponent<PlayerHealth>();
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == TagManager.PLAYER_TAG || target.tag == TagManager.PLAYER_HEALTH_TAG)
        {
            playerHealth.AddHealth();
            gameObject.SetActive(false);
        }

    }
}
