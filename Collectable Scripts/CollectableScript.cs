using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour
{

    void OnTriggerEnter2D (Collider2D target) 
    {
        if (target.tag == TagManager.PLAYER_TAG || target.tag == TagManager.PLAYER_HEALTH_TAG)
        {
            GameplayController.instance.coinCount++;
            gameObject.SetActive(false);
        }

    }
}
