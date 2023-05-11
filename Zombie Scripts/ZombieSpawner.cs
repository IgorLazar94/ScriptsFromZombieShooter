using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject ZombiePrefab;
    public Transform spawnPoint;
    public GameObject fx_Shred;

    private GameObject zombie;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;
        fx_Shred.SetActive(true);

        AudioManager.instance.ZombieRiseSound();

        zombie = Instantiate(ZombiePrefab, spawnPoint.position, Quaternion.identity);

        if (zombie.transform.position.x < player.position.x)
        {
            zombie.transform.localScale = new Vector3(-1f, 1f, 1f);
        } else if (zombie.transform.position.x > player.position.x)  
        {
            zombie.transform.localScale = new Vector3(1f, 1f, 1f);

        }
        StartCoroutine(WaitDeactivate());

    }

    IEnumerator WaitDeactivate()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));

        gameObject.SetActive(false);
    }


}
