using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

	public List<WeaponController> weapons_Unlocked;
	public WeaponController[] total_Weapons; 

	[HideInInspector]
	public WeaponController current_Weapon;
	private int current_Weapon_Index;

	private TypeControlAttack current_Type_Control; 

	private PlayerArmController[] armController; 

	private PlayerAnimations playerAnim;

	private bool isShooting;

	public GameObject meleeDamagePoint;


	public void Awake ()
    {
		playerAnim = GetComponent<PlayerAnimations>();
		LoadActiveWeapons();
		current_Weapon_Index = 1;

	}
	public void Start () {
				armController = GetComponentsInChildren<PlayerArmController>();

        ChangeWeapon(weapons_Unlocked[1]);

		playerAnim.SwitchWeaponAnimation(
			(int)weapons_Unlocked[current_Weapon_Index].defaultConfig.typeWeapon);

    }

    void LoadActiveWeapons ()
    {
		weapons_Unlocked.Add(total_Weapons[0]);	

        for (int i = 1; i < total_Weapons.Length; i++)
        {
			weapons_Unlocked.Add(total_Weapons[i]);
        }
    }

	public void SwitchWeapon()
	{
		current_Weapon_Index++;
		//Debug.Log(current_Weapon_Index + " - текущий индекс оружия");
		//Debug.Log((int)weapons_Unlocked[current_Weapon_Index].defaultConfig.typeWeapon + " - тип оружия");
		current_Weapon_Index = (current_Weapon_Index >= weapons_Unlocked.Count) ? 0 : current_Weapon_Index;

		playerAnim.SwitchWeaponAnimation(
			(int)weapons_Unlocked[current_Weapon_Index].defaultConfig.typeWeapon);  // здесь полная жопа
        ChangeWeapon(weapons_Unlocked[current_Weapon_Index]);
	}

	void ChangeWeapon (WeaponController newWeapon)
    {
		if (current_Weapon)
			current_Weapon.gameObject.SetActive(false);
			
			current_Weapon = newWeapon;
			current_Type_Control = newWeapon.defaultConfig.typeControl;		

			newWeapon.gameObject.SetActive(true);

			if (newWeapon.defaultConfig.typeWeapon == TypeWeapon.TwoHand)
            {
                for (int i = 0; i < armController.Length; i++)
                {
					armController[i].ChangeToTwoHand();
                }
            } else
            {
				for (int i = 0; i < armController.Length; i++)
				{
					armController[i].ChangeToOneHand();
				}
			}

        
    }


	public void Attack ()
    {
		if (current_Type_Control == TypeControlAttack.Hold)
        {
			current_Weapon.CallAttack();
        } else if (current_Type_Control == TypeControlAttack.Click)
        {
			if (!isShooting)
            {
				current_Weapon.CallAttack();
				isShooting = true;
            }
        }
    }

	public void ResetAttack ()
    {
		isShooting=false;
    }

	void AllowCollisionDetection ()
    {
		meleeDamagePoint.SetActive(true);
    }
	void DenyCollisionDetection()
	{
		meleeDamagePoint.SetActive(false);
	}













}
