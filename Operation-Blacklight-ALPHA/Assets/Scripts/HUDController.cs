using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    // A - General Variables
    public GameObject player;
    public GameObject gameOverHUD;
    public GameObject youWinHUD;
    private bool gameOver;
    private bool gameWin;

    // B - Health Variables
    public GameObject health;
    private Text healthText;
    private int playerHealth;
    private int playerCurrentHealth;

    // C - Dash Variables
    private Text dashText;
    public GameObject dash;

    void Start()
    {
        // B - Health Variable Initialization
        playerHealth = player.GetComponent<PlayerController>().playerHealth;
        playerCurrentHealth = player.GetComponent<PlayerController>().playerCurrentHealth;
        healthText = health.GetComponent<Text>();
        dashText = dash.GetComponent<Text>();
        playerCurrentHealth = playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // A - Check Game Over Status from PlayerController
        gameOver = player.GetComponent<PlayerController>().gameOver;

        // A - Check Game Win Status from PlayerController
        gameWin = player.GetComponent<PlayerController>().gameWin;

        // B - Health HUD Implementation
        healthText.text = "Health: " + playerCurrentHealth;

        if (playerCurrentHealth >= 5 )
        {
            healthText.color = Color.green;
        } 
        else if (playerCurrentHealth <= 4 && playerCurrentHealth >= 2)
        {
            healthText.color = Color.yellow;
        } 
        else if (playerCurrentHealth == 1) 
        {
            healthText.color = Color.red;
        } 
        else if (playerCurrentHealth == 0) 
        {
            healthText.color = Color.black;
        }

        if (gameOver == true)
        {
            gameOverHUD.SetActive(true);
        }

        if (gameWin== true)
        {
            youWinHUD.SetActive(true);
        }
    }

    // B - To Handle Changing Player Health Variable
    public void changeHealth(int newHealth)
    {
        playerCurrentHealth = newHealth;
    }
}
