using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Transform playerTransform;










    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;
    }

    void LateUpdate()
    {

        if (GameplayController.instance.gameGoal != GameplayController.GameGoal.DEFEND_FENCE && 
            GameplayController.instance.gameGoal != GameplayController.GameGoal.GAME_OVER)
        {
            if (playerTransform)
            {

                Vector3 temp = transform.position;
                temp.x = playerTransform.position.x;
                transform.position = temp;

            }
        }


    }












}
