using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceHealth : MonoBehaviour
{

    public int health = 100;

    public ParticleSystem wood_Break_FX, wood_Explode_FX;

    public void DealDAmage(int damage)
    {
        health -= damage;
        wood_Break_FX.Play();


        if (health <= 0)
        {
            wood_Explode_FX.Play();
            AudioManager.instance.FenceExplosion();

            GameplayController.instance.fenceDestroyed = true;


            StartCoroutine(DeactivateGameobject());
        }

    }

    IEnumerator DeactivateGameobject()
    {

        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }

























}
