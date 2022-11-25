using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollisionController : MonoBehaviour
{
    // A - Handles Collision Trigger
    private void OnTriggerEnter(Collider collision)
    {
        // If Collision is with a Projectile, Delete it
        if (collision.gameObject.tag == "Projectile")
        {
            Debug.Log("Collision with wall");
            Destroy(collision.gameObject);
        }
    }
}
