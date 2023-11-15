using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable 
{
    //interfacen cannot hold any data (i.e. variables)
    //seting rules u can define onto other objects 

    public float Health { get; set; }
    //get set automatically fills in the properties 

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if(Health <= 0)
        {
            Die();
        }
    }

    public void Die();
}
