using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private GameObject player;

    public bool isTrgeted = false;

    public bool isBoss = false;
    public int miniEnemySpawn;
    private SpawnManager spawnmanager;
    public bool isMinyonSpawn=false;
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();

        if(isBoss)
            spawnmanager = FindObjectOfType<SpawnManager>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (isBoss)
        {
            if (!isMinyonSpawn)
            {
                isMinyonSpawn = true;
                spawnmanager.SpawnMiniEnemy(miniEnemySpawn);
                
            }
                

        }

        Vector3 moveDir = new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z).normalized;
        rb.AddForce(moveDir * moveSpeed);


        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }


}
