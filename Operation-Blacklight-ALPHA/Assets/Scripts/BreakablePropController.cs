using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePropController : MonoBehaviour
{
    // A - Prop Health Variables
    public int propHealth;
    private int propCurrentHealth;
    public Renderer propRenderer;
    private float damageAlertTime = 0.1f;
    private float damageAlertCounter;
    private Color propColor;

    // To Handle Initialization
    void Start()
    {
        // A - Initiailize Health Variables
        propCurrentHealth = propHealth;
        propRenderer = GetComponent<Renderer>();
        propColor = propRenderer.material.GetColor("_Color");
    }

    // To Handle non-Frame-Sensitive Operations
    void Update()
    {
        // A - Destroy Prop if Health <= 0
        if (propCurrentHealth <= 0)
        {
            Destroy(gameObject);
        }

        // A - Reset Player Color when Alert Counter Reaches 0
        if (damageAlertCounter > 0)
        {
            damageAlertCounter -= Time.deltaTime;
            if (damageAlertCounter <= 0)
            {
                propRenderer.material.SetColor("_Color", propColor);
            }
        }
    }

    // A - To Handle Damage to Health Pool & Damage Alert
    public void DamageProp(int damage)
    {
        propCurrentHealth -= damage;
        damageAlertCounter = damageAlertTime;
        propRenderer.material.SetColor("_Color", Color.white);
    }
}
