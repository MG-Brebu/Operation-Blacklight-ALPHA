using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // A - Fire Control Variables
    public bool isFiring;
    public ProjectileController projectile;
    public float projectileSpeed;
    public float rateOfFire;
    private float fireCounter;
    public bool fireReady = true;
    public Transform firePoint;

    // To Handle Initialization
    void Start()
    {
        
    }

    // To Handle non-Frame-Sensitive Operations
    void Update()
    {
        // A - If weapon is firing, create new projectile respecting set rate of fire
        if (fireReady)
        {
            if (isFiring)
            {
                fireCounter -= Time.deltaTime;
                if (fireCounter <= 0)
                {
                    fireCounter = rateOfFire;
                    ProjectileController newProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation);
                    newProjectile.projectileSpeed = projectileSpeed;
                    fireReady = false;
                }
            }
            else
            {
                fireCounter = 0;
            }
        }
    }
}
