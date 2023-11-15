using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float shootForce;
    [SerializeField] private Rigidbody rb;

    private float trueDamage;

    

    

    public void Init(float chargePercent, Vector3 fireDirection)
    {
       
        rb.AddForce(shootForce * chargePercent * fireDirection, ForceMode.Impulse);
        trueDamage = chargePercent * damage;

    }

    private void OnCollisionEnter(Collision other)
    {
        print(other.transform.name + ", " + other.transform.root.name);
       
        if (other.transform.root.TryGetComponent(out IDamagable hitTarget))
        {
            print(other.transform.tag);
            switch (other.transform.tag)
            {
                case "Head":
                    trueDamage *= 2;
                    break;
                case "Limb":
                    trueDamage *= 0.8f;
                    break;

                
            }

            print("has taken damage: " + trueDamage);
            hitTarget.TakeDamage(trueDamage);
        }

        Destroy(gameObject);
    }
}
