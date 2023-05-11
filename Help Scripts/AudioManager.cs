using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   
    public static AudioManager instance;

    public AudioSource mainMusic;

    public AudioClip[] gunSounds;
    public AudioClip meleeSound;

    public AudioSource playerAttack_AudioSource;

    public AudioSource zombie_Attack_AudioSource;
    public AudioSource zombie_Rise_AudioSource;
    public AudioSource zombie_Die_AudioSource;

    public AudioClip zombie_Rise_Clip, zombieDie_Clip;
    public AudioClip[] zombieAttack_Clips;

    public AudioSource fence_Explosion_AudioSource;
    public AudioClip fence_Explosion_Clip;

    void Awake ()
    {
        MakeSingleton();
    }

    void MakeSingleton ()
    {
        if (instance != null)
        {
            Destroy (gameObject);
        } else
        {
            instance = this;
            DontDestroyOnLoad (gameObject);
        }
    }

    public void GunSound (int index)
    {
        playerAttack_AudioSource.PlayOneShot(gunSounds[index], 1.0f);
    }

    public void MeleeAttackSound ()
    {
        playerAttack_AudioSource.PlayOneShot(meleeSound, 1.0f);
    }

    public void ZombieRiseSound ()
    {
        zombie_Rise_AudioSource.PlayOneShot(zombie_Rise_Clip, 1.0f);
    }

    public void ZombieDieSound ()
    {
        zombie_Die_AudioSource.PlayOneShot(zombieDie_Clip, 1.0f);
    }

    public void ZombieAttackSound ()
    {
        int index = Random.Range(0, zombieAttack_Clips.Length);
        zombie_Attack_AudioSource.PlayOneShot(zombieAttack_Clips[index], 1.0f);
    }

    public void FenceExplosion ()
    {
        fence_Explosion_AudioSource.PlayOneShot(fence_Explosion_Clip, 1.0f);
    }














































}
