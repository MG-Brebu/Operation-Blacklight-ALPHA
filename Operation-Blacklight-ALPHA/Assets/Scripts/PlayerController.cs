using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // A - Player Interaction Variables
    public float playerSpeed;
    private Rigidbody playerRB;
    private Vector3 currentPlayerMove;
    private Vector3 playerMove;
    private Vector3 smoothInputVelocity;
    public float smoothInputSpeed = .2f;
    public Vector3 playerMoveVelocity;
    private Camera playerCamera;

    // B - Player Weapon Variables
    public WeaponController weapon;

    // C - Player Health Variables
    public int playerHealth;
    public int playerCurrentHealth;

    // D - Player Animation Variables
    public Animator animator;

    // To Handle Initialization
    void Start()
    {
        // A - Initialize Interaction Variables
        playerRB = GetComponent<Rigidbody>();
        playerCamera = FindObjectOfType<Camera>();

        // C - Initiailize Health Variables
        playerCurrentHealth = playerHealth;
    }

    // To Handle non-Frame-Sensitive Operations
    void Update()
    {
        // A - WASD Movement Initialization
        playerMove = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        currentPlayerMove = Vector3.SmoothDamp(currentPlayerMove, playerMove, ref smoothInputVelocity, smoothInputSpeed);
        playerMoveVelocity = currentPlayerMove * playerSpeed;

        // A - Mouse Look Rotation Implementation
        Ray playerCameraRay = playerCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(playerCameraRay, out rayLength))
        {
            Vector3 playerLookPoint = playerCameraRay.GetPoint(rayLength);
            Debug.DrawLine(playerCameraRay.origin, playerLookPoint, Color.red);

            Vector3 lookAt = new Vector3(playerLookPoint.x, transform.position.y, playerLookPoint.z);
            transform.LookAt(lookAt);
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

        // C - Implement Animations
        animator.SetBool("Forward", Input.GetKey(KeyCode.W));
        animator.SetBool("Backward", Input.GetKey(KeyCode.S));
        animator.SetBool("Left", Input.GetKey(KeyCode.A));
        animator.SetBool("Right", Input.GetKey(KeyCode.D));
        animator.SetBool("Shoot", Input.GetMouseButton(0));
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
    }
}
