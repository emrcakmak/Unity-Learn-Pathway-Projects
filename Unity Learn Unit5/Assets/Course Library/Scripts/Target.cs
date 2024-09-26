using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    private float minSpeed = 12f;
    private float maxSpeed = 16f;
    private float maxTorque = 10f;
    private float xRange = 4;
    private float ySpaenPos = -6;
    private Rigidbody rb;
    public int point = 5;
    private GameManager gm;

    public ParticleSystem explosionParticle;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gm = FindObjectOfType<GameManager>();
        rb.AddForce(RandomForce(), ForceMode.Impulse);
        rb.AddTorque(RandomTorque(), RandomTorque(),RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
    }

    
    void Update()
    {
        
    }

    //private void OnMouseDown()
    //{
    //    if (gm.isGameActive&&gm.isPaused==false)
    //    {
    //        Destroy(gameObject);
    //        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
    //        gm.UpdateScore(point);
    //    }

    //}
    private void OnMouseEnter()
    {
        if (gm.isGameActive)
       {
          Destroy(gameObject);
         Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
       gm.UpdateScore(point);
       }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad") && gm.isGameActive)
        {
            gm.ChangeLives(-1);
        }
           
        
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpaenPos);
    }
}
