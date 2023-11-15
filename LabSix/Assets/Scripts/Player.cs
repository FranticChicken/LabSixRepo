using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private WeaponBase myWeapon;
    private bool weaponShootToggle;

    private void Start()
    {
        InputManager.Init(this);
        InputManager.EnableInGame();
    }

    public void Shoot()
    {
        float ammoAmount = myWeapon.returnMaxAmmo();
        print(ammoAmount);
        if (ammoAmount > 0) 
        {
            
            //print("I shot: " + InputManager.GetCameraRay());
            weaponShootToggle = !weaponShootToggle;
            if (weaponShootToggle) myWeapon.StartShooting();
            else myWeapon.StopShooting();

        }
    }
}
