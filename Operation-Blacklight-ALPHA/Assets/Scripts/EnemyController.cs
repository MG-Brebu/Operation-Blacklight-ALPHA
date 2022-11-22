using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // A - Enemy Interaction Variables
    private Rigidbody enemyRB;
    public float enemySpeed;
    public PlayerController player;

    // B - Enemy Fire Variables
    public ProjectileController projectile;
    public float projectileSpeed;
    public float rateOfFire;
    private float fireCounter;
    public Transform firePoint;

    // C - Enemy Health Variables
    public int enemyHealth;
    private int enemyCurrentHealth;
    public Renderer enemyRenderer;
    private float damageAlertTime = 0.1f;
    private float damageAlertCounter;
    private Color enemyColor;

    // To Handle Initialization
    void Start()
    {
        // A - Initialize Interaction Variables
        enemyRB = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerController>();

        // C - Initiailize Health Variables
        enemyCurrentHealth = enemyHealth;
        enemyRenderer = GetComponent<Renderer>();
        enemyColor = enemyRenderer.material.GetColor("_Color");
    }

    // To Handle non-Frame-Sensitive Operations
    void Update()
    {
        // A - Look at Player
        transform.LookAt(player.transform);

        // B - Fire Projectile at Player
        fireCounter -= Time.deltaTime;
        if (fireCounter <= 0)
        {
            fireCounter = rateOfFire;
            ProjectileController newProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation);
            newProjectile.projectileSpeed = projectileSpeed;
        }

        // C - Destroy Enemy if Health <= 0
        if (enemyCurrentHealth <= 0)
        {
            Destroy(gameObject);
        }

        // C - Reset Enemy Color when Alert Counter Reaches 0
        if (damageAlertCounter > 0)
        {
            damageAlertCounter -= Time.deltaTime;
            if (damageAlertCounter <= 0)
            {
                enemyRenderer.material.SetColor("_Color", enemyColor);
            }
        }
    }
    
    // C - To Handle Damage to Health Pool
    public void DamageEnemy(int damage)
    {
        enemyCurrentHealth -= damage;
        damageAlertCounter = damageAlertTime;
        enemyRenderer.material.SetColor("_Color", Color.white);
    }    
}
