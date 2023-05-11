using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmController : MonoBehaviour {


	public Sprite one_Hand_Sprite, two_Hand_Sprite;
	private SpriteRenderer sr;

	private int current_Weapon_Index;



	// Use this for initialization
	 void Awake () {


		sr = GetComponent<SpriteRenderer>();

		// Load Active Weapons();
	    current_Weapon_Index = 1;
	}



	public void ChangeToOneHand ()
    {
		sr.sprite = one_Hand_Sprite;
    }

	public void ChangeToTwoHand()
	{
		sr.sprite = two_Hand_Sprite;
	}







	
}
