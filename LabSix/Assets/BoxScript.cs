using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour, IDamagable
{

    [field: SerializeField] public float Health { get; set; }
    //get set automatically fills in the properties 
    [SerializeField] private float speed;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.AddForce(Vector3.forward * speed);

        Vector3 velocity = rb.velocity;
        float currentSpeed = velocity.magnitude;
        if(currentSpeed > 5)
        {
            Vector3 normalized = velocity / currentSpeed;
            rb.velocity = normalized * 5;
        }


       
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
