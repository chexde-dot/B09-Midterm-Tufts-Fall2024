using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float destroyHeight = -5f; // Height at which the projectile should be destroyed
    public int scoreValue = 1; // Score to add when hitting a Dock

    private void FixedUpdate()
    {
        // Check if the projectile's y position is below the destroyHeight
        if (transform.position.y < destroyHeight)
        {
            Destroy(gameObject); // Destroy the projectile
            
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        // Check if the collided object has the tag "Dock"
        if (collision.gameObject.CompareTag("Dock"))
        {
            // Increase live saved by 1
            GameHandler gameHandler = FindObjectOfType<GameHandler>();
            if (gameHandler != null)
            {
                gameHandler.IncreaseLiveSaved(1); // Adjust the number as needed
            }

            Destroy(gameObject); // Destroy the projectile after hitting the Dock
        }
        /*else if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }*/
    }
}