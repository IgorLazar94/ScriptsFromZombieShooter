using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartPool : MonoBehaviour
{
    public static SmartPool instance;



    private List<GameObject> bullet_Fall_Fx = new List<GameObject>();
    private List<GameObject> bullet_Prefabs = new List<GameObject>();
    private List<GameObject> rocket_Bullet_Prefabs = new List<GameObject>();

    public GameObject[] zombies;
    private float y_Spawn_Pos_Min = -3.7f, y_Spawn_Pos_Max = -0.36f;
    private Camera mainCamera;

 private void Start ()
    {
        mainCamera = Camera.main;
        InvokeRepeating("StartSpawningZombies", 1f, Random.Range(1f, 5f));
    }

    void Awake()
    {
        MakeInstance();
    }

    void OnDisable ()
    {
        instance = null;
    }

    // Update is called once per frame
    void MakeInstance ()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    public void CreateBulletAndBulletFall (GameObject bullet, GameObject bulletFall, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject temp_Bullet = Instantiate(bullet);
            GameObject temp_Bullet_Fall = Instantiate(bulletFall);


            bullet_Prefabs.Add(temp_Bullet);
            bullet_Fall_Fx.Add(temp_Bullet_Fall);

            bullet_Prefabs[i].SetActive(false);
            bullet_Fall_Fx[i].SetActive(false);
        }

    } // create bullet and bullet fall

    public void CreateRocket (GameObject rocket, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject temp_Rocket_Bullet = Instantiate(rocket);

            rocket_Bullet_Prefabs.Add(temp_Rocket_Bullet);

            rocket_Bullet_Prefabs[i].SetActive(false);

        }

    } // create rocket

    public GameObject SpawnBulletFallFx (Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < bullet_Fall_Fx.Count; i++)
        {

            if (!bullet_Fall_Fx[i].activeInHierarchy)
            {
                bullet_Fall_Fx[i].SetActive(true);
                bullet_Fall_Fx[i].transform.position = position;
                bullet_Fall_Fx[i].transform.rotation = rotation;

                return bullet_Fall_Fx[i];

            } // spawn bullet fall Fx
        }
        return null;
    }

    public void SpawnBullet (Vector3 position, Vector3 direction, Quaternion rotation, NameWeapon weaponName)   
    {

        if (weaponName != NameWeapon.ROCKET)
        {
            for (int i = 0; i < bullet_Prefabs.Count; i++)
            {
                if (!bullet_Prefabs[i].activeInHierarchy)
                {
                    bullet_Prefabs[i].SetActive(true);
                    bullet_Prefabs[i].transform.position = position;
                    bullet_Prefabs[i].transform.rotation = rotation;

                    // GET THE BULLET SCRIPT
                    bullet_Prefabs[i].GetComponent<BulletController>().SetDirection(direction);


                    //SET BULLET DAMAGE
                    SetBulletDamage(weaponName, bullet_Prefabs[i]);

                    break;
                }

            }

        } else
        {
            for (int i = 0; i < rocket_Bullet_Prefabs.Count; i++)
            {

                if (!rocket_Bullet_Prefabs[i].activeInHierarchy)
                {

                    rocket_Bullet_Prefabs[i].SetActive(true);
                    rocket_Bullet_Prefabs[i].transform.position = position;
                    rocket_Bullet_Prefabs[i].transform.rotation = rotation;

                    // GET THE BULLET SCRIPT
                    rocket_Bullet_Prefabs[i].GetComponent<BulletController>().SetDirection(direction);

                    //SET BULLET DAMAGE
                    SetBulletDamage(weaponName, rocket_Bullet_Prefabs[i]);
                    break;
                }

            }

        }

    }

    void SetBulletDamage (NameWeapon weaponName, GameObject bullet)
    {
        switch (weaponName)
        {
            case NameWeapon.PISTOL:
                bullet.GetComponent<BulletController>().damage = 1;
                break;

            case NameWeapon.MP5:
                bullet.GetComponent<BulletController>().damage = 2;
                break;

            case NameWeapon.M3:
                bullet.GetComponent<BulletController>().damage = 3;
                break;

            case NameWeapon.AK:
                bullet.GetComponent<BulletController>().damage = 4;
                break;

            case NameWeapon.AWP:
                bullet.GetComponent<BulletController>().damage = 8;
                break;

            case NameWeapon.ROCKET:
                bullet.GetComponent<BulletController>().damage = 8;
                break;



        }


    }

    void  StartSpawningZombies ()
    {
        if (GameplayController.instance.gameGoal == GameplayController.GameGoal.DEFEND_FENCE)
        {
            float xPos = mainCamera.transform.position.x;
            xPos += 15f;
            float yPos = Random.Range(y_Spawn_Pos_Min, y_Spawn_Pos_Max);

            Instantiate(zombies[Random.Range(0, zombies.Length)], new Vector3(xPos, yPos, 0), Quaternion.identity);

        }

        if (GameplayController.instance.gameGoal == GameplayController.GameGoal.KILL_ZOMBIES ||
            GameplayController.instance.gameGoal == GameplayController.GameGoal.TIMER_COUNTDOWN ||
            GameplayController.instance.gameGoal == GameplayController.GameGoal.WALK_TO_GOAL_STEPS)
        {
            float xPos = mainCamera.transform.position.x;

            if (Random.Range(0, 2) > 0)
            {
                xPos += Random.Range(10f, 15f);

            } else 
            { 
                xPos -= Random.Range(10f, 15f);
            }

            float yPos = Random.Range(y_Spawn_Pos_Min, y_Spawn_Pos_Max);

            Instantiate(zombies[Random.Range(0, zombies.Length)], new Vector3(xPos, yPos, 0), Quaternion.identity);
        }
        if (GameplayController.instance.gameGoal == GameplayController.GameGoal.GAME_OVER)
        {
            CancelInvoke("StartSpawningZombies");
        }



    }



}
