using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPower : MonoBehaviour
{
    private Transform target;
    private float speed = 15f;
    private float aliveTime = 7f;
    private float rocketPower = 20f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 modeDir = (target.transform.position - transform.position).normalized;
            transform.position += modeDir * speed * Time.deltaTime;
            transform.LookAt(target);
        }


        if (target == null)
        {
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (target != null)
        {


            if (collision.gameObject.CompareTag(target.tag))
            {
                Rigidbody rbTarget = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 away = -collision.contacts[0].normal;// bu ne

                rbTarget.AddForce(away * rocketPower, ForceMode.Impulse);

            }
        }


    }

    public void Fire(Transform newTarget)
    {
        target = newTarget;
        Destroy(gameObject, aliveTime);
    }
}
