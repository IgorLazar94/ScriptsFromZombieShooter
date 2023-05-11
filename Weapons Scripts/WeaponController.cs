using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum NameWeapon
{
    MELEE,
    PISTOL,
    MP5,
    M3,
    AK,
    AWP,
    FIRE,
    ROCKET
}


public class WeaponController : MonoBehaviour
{

    public DefaultConfig defaultConfig;
    public NameWeapon nameWp;


    protected PlayerAnimations playerAnim;
    protected float lastShot;

    public int gunIndex;
    public int currentBullet;
    public int bulletMax;
    Text bulletMaxUI;

    private void Awake()
    {
        bulletMaxUI = GameObject.Find("RemainingAmmunition").GetComponent<Text>();
        playerAnim = GetComponentInParent<PlayerAnimations>();
        currentBullet = bulletMax;
    }


    void Update()
    {
        bulletMaxUI.text = currentBullet.ToString();
    }


    public void CallAttack()
    {
        if (Time.time > lastShot + defaultConfig.fireRate)
        {
            if (currentBullet > 0)
            {
                ProcessAttack();
                //animate shoot
                playerAnim.AttackAnimation();

                lastShot = Time.time;
                currentBullet--;
            }
            else
            {
                // play no ammo sound
            }
        }
    }

    public virtual void ProcessAttack()
    {
        AudioManager.instance.MeleeAttackSound();
    }








































































}
