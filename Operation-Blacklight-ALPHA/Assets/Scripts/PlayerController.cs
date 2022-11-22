using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // A - Player Interaction Variables
    public float playerSpeed;
    private Rigidbody playerRB;
    private Vector3 playerMove;
    private Vector3 playerMoveVelocity;
    private Camera playerCamera;

    // B - Player Weapon Variables
    public WeaponController weapon;

    // C - Player Health Variables
    public int playerHealth;
    private int playerCurrentHealth;
    public Renderer playerRenderer;
    private float damageAlertTime = 0.1f;
    private float damageAlertCounter;
    private Color playerColor;

    // To Handle Initialization
    void Start()
    {
        // A - Initialize Interaction Variables
        playerRB = GetComponent<Rigidbody>();
        playerCamera = FindObjectOfType<Camera>();

        // C - Initiailize Health Variables
        playerCurrentHealth = playerHealth;
        playerRenderer = GetComponent<Renderer>();
        playerColor = playerRenderer.material.GetColor("_Color");
    }

    // To Handle non-Frame-Sensitive Operations
    void Update()
    {
        // A - WASD Movement Initialization
        playerMove = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        playerMoveVelocity = playerMove * playerSpeed;

        // A - Mouse Look Rotation Implementation
        Ray playerCameraRay = playerCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(playerCameraRay, out rayLength))
        {
            Vector3 playerLookPoint = playerCameraRay.GetPoint(rayLength);
            Debug.DrawLine(playerCameraRay.origin, playerLookPoint, Color.red);

            transform.LookAt(new Vector3(playerLookPoint.x, transform.position.y, playerLookPoint.z));
        }

        // B - Weapon Fire Control
        if (Input.GetMouseButtonDown(0))
        {
            weapon.isFiring = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            weapon.isFiring = false;
            weapon.fireReady = true;
        }

        // B - Destroy Player if Health <= 0
        if (playerCurrentHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Game Over");
            Time.timeScale = 0;
        }

        // B - Reset Player Color when Alert Counter Reaches 0
        if (damageAlertCounter > 0 )
        {
            damageAlertCounter -= Time.deltaTime;
            if (damageAlertCounter <= 0)
            {
                playerRenderer.material.SetColor("_Color", playerColor);
            }
        }
    }

    // To Handle Frame-Sensitive Operations
    private void FixedUpdate()
    {
        // A - WASD Movement Implementation
        playerRB.velocity = playerMoveVelocity;
    }

    // B - To Handle Damage to Health Pool & Damage Alert
    public void DamagePlayer(int damage)
    {
        playerCurrentHealth -= damage;
        damageAlertCounter = damageAlertTime;
        playerRenderer.material.SetColor("_Color", Color.white);
    }
}
