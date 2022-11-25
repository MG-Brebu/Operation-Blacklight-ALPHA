using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePropController : MonoBehaviour
{
    // A - Prop Interaction Variables
    private Rigidbody propRB;

    // B - Prop Health Variables
    public int propHealth;
    private int propCurrentHealth;
    private Renderer propRenderer;
    private float damageAlertTime = 0.1f;
    private float damageAlertCounter;
    private Color propColor;

    // To Handle Initialization
    void Start()
    {
        // A - Initialize Interaction Variables
        propRB = GetComponent<Rigidbody>();

        // B - Initiailize Health Variables
        propCurrentHealth = propHealth;
        propRenderer = GetComponent<Renderer>();
        propColor = propRenderer.material.GetColor("_Color");
    }

    // To Handle non-Frame-Sensitive Operations
    void Update()
    {
        // B - Destroy Prop if Health <= 0
        if (propCurrentHealth <= 0)
        {
            Destroy(gameObject);
        }

        // B - Reset Player Color when Alert Counter Reaches 0
        if (damageAlertCounter > 0)
        {
            damageAlertCounter -= Time.deltaTime;
            if (damageAlertCounter <= 0)
            {
                propRenderer.material.SetColor("_Color", propColor);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            propCurrentHealth -= 1;
            damageAlertCounter = damageAlertTime;
            propRenderer.material.SetColor("_Color", Color.white);
            Destroy(collision.gameObject);
        }
    }
}
