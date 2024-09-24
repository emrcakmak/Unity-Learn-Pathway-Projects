using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody rb;
    public float moveSpeed=5f;
    public bool hasPowerUp = false;
    private float pushBackPower = 30f;
    public PowerUpType currentPoweUp;


    public GameObject PowerUpIndecator;
    public GameObject Rocket;

    public float smashTime;
    public float smashPower;
    public float smashRadius;
    private float baseY;
    public float smashSpeed;
    public bool isSmash = false;
    public bool isGameEnd = false;
  
    void Start()
    {
        currentPoweUp = PowerUpType.None;
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -5)
        {
            isGameEnd = true;
        }
        if (!isSmash)
        {
            float moveVaertical = Input.GetAxis("Vertical");
            float moveHorizontal = Input.GetAxis("Horizontal");
            Vector3 moveDir = new Vector3(moveHorizontal, 0, moveVaertical);
            rb.AddForce(moveDir * moveSpeed);
        }



        if (hasPowerUp&& Input.GetKeyDown(KeyCode.Space))
        {
            hasPowerUp = false;
            if (currentPoweUp == PowerUpType.Rockets)
            {
                var enemy = FindObjectOfType<EnemyController>();

                GameObject a = Instantiate(Rocket, transform.position, Rocket.transform.rotation);
                a.GetComponent<RocketPower>().Fire(enemy.transform);
            }

            if (currentPoweUp == PowerUpType.Smash)
            {
                StartCoroutine(Smash());

            }

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!hasPowerUp)
        {
            if (other.CompareTag("PowerUp"))
            {
                hasPowerUp = true;
                currentPoweUp = other.GetComponent<PoweUpGem>().powerUp;
                Destroy(other.gameObject);
                PowerUpIndecator.SetActive(true);
                StartCoroutine("ResetPowerUp");
            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy")&& currentPoweUp == PowerUpType.PushBack)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();

            enemyRb.AddForce((collision.gameObject.transform.position - transform.position)* pushBackPower, ForceMode.Impulse);

        }
    }




    IEnumerator ResetPowerUp()
    {
        yield return new WaitForSeconds(7f);
        currentPoweUp = PowerUpType.None;
        hasPowerUp = false;
        PowerUpIndecator.SetActive(false);
    }

    IEnumerator Smash()
    {
        isSmash = true;
        var enemies = FindObjectsOfType<EnemyController>();

        baseY = transform.position.y;

        float JumpTime = Time.time + smashTime;

        while (Time.time < JumpTime)
        {

            rb.velocity = new Vector2(0, smashSpeed);
            yield return null;
        }
        while (transform.position.y > baseY)
        {
            rb.velocity = new Vector2(0, -smashSpeed*2);
            yield return null;
        }

        for(int i = 0; i < enemies.Length; i++)
        {
            if(enemies!=null)
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(smashPower, transform.position, smashRadius, 0f, ForceMode.Impulse);
        }

        isSmash = false;
    }
   
}
