using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    // A - Projectile Interaction Variables
    public float projectileSpeed;
    private float existCounter = 2;
    public int projectileDamage;

    // To Handle Initialization
    void Start()
    {
        
    }

    // To Handle non-Frame-Sensitive Operations
    void Update()
    {
        // A - Projectile Movement
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);

        // A - Count down existCounter and Destroy Projectile if existCounter <= 0
        existCounter -= Time.deltaTime;
        if (existCounter <= 0)
        {
            Destroy(gameObject);
        }
    }

    // A - To Handle Collision Detection
    private void OnCollisionEnter(Collision collide)
    {
        // If collision with enemy, damage enemy and destroy projectile
        if (collide.gameObject.tag == "Enemy")
        {
            collide.gameObject.GetComponent<EnemyController>().DamageEnemy(projectileDamage);
            Destroy(gameObject);
        }

        // If collision with player, damage player and destroy projectile
        if (collide.gameObject.tag == "Player")
        {
            collide.gameObject.GetComponent<PlayerController>().DamagePlayer(projectileDamage);
            Destroy(gameObject);
            Debug.Log("Player Collide");
        }

        // If collision with breakable prop, damage breakable prop and destroy projectile
        if (collide.gameObject.tag == "BreakableProp")
        {
            collide.gameObject.GetComponent<BreakablePropController>().DamageProp(projectileDamage);
            Destroy(gameObject);
        }
    }
}
