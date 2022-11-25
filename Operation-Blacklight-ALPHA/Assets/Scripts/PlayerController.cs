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
    private SceneSwitchController sceneSwitch;
    public bool gameOver = false;
    public bool gameWin = false;


    // B - Player Weapon Variables
    public WeaponController weapon;

    // C - Player Health Variables
    public int playerHealth;
    public int playerCurrentHealth;
    public GameObject hud;

    // D - Player Animation Variables
    public Animator animator;

    // To Handle Initialization
    void Start()
    {
        // A - Initialize Interaction Variables
        playerRB = GetComponent<Rigidbody>();
        playerCamera = FindObjectOfType<Camera>();
        sceneSwitch = FindObjectOfType<SceneSwitchController>();

        // C - Initiailize Health Variables
        playerCurrentHealth = playerHealth;
    }

    // To Handle non-Frame-Sensitive Operations
    void Update()
    {
        // Update() only runs if game is not over and player has not won
        if (gameOver == false && gameWin == false) {
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

            // B - Call gameOver method if Player Health Reaches 0
            if (playerCurrentHealth <= 0)
            {
                GameOver();
            }

            // C - Implement Animations
            animator.SetBool("Forward", Input.GetKey(KeyCode.W));
            animator.SetBool("Backward", Input.GetKey(KeyCode.S));
            animator.SetBool("Left", Input.GetKey(KeyCode.A));
            animator.SetBool("Right", Input.GetKey(KeyCode.D));
            animator.SetBool("Shoot", Input.GetMouseButton(0));
        }
    }

    // To Handle Frame-Sensitive Operations
    private void FixedUpdate()
    {
        // FixedUpdate() only runs if game is not over and player has not won
        if (gameOver == false);
        {
            // A - WASD Movement Implementation
            playerRB.velocity = playerMoveVelocity;
        }
    }

    // B - To Handle Damage to Health Pool & Damage Alert
    public void DamagePlayer(int damage)
    {
        playerCurrentHealth -= damage;
        hud.GetComponent<HUDController>().changeHealth(playerCurrentHealth);
    }

    // B - To Handle Game Over State
    public void GameOver()
    {
        gameOver = true;
        animator.SetBool("GameOver", true);
        Time.timeScale = 0.5f;
        Debug.Log(gameOver);
    }

    // A - To Handle Collisions
    private void OnCollisionEnter(Collision collision)
    {
        // If collision with finish zone, move to next scene
        if (collision.gameObject.tag == "FinishZone")
        {
            Debug.Log("Finish Zone Hit");
            sceneSwitch.GetComponent<SceneSwitchController>().nextLevel();
        }

        // If collision with finish zone, move to next scene
        if (collision.gameObject.tag == "WinZone")
        {
            Debug.Log("Win Zone Hit");
            gameWin = true;
        }
    }
}
