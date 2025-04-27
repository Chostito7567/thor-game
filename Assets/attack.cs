using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public GameObject thunderPrefab; // Assign your thunder prefab here
    public float bulletSpeed = 10f;   // Speed at which the thunder flies
    public Transform firePoint;      // Where the bullet comes out from

    public float inputDelay = 0.2f;    // Delay time before input is accepted at start
    private float delayTimer;        // Timer to track delay

    public float shootCooldown = 0.2f; // Cooldown time between shots
    private float cooldownTimer = 0.2f;  // Timer to track shooting cooldown

    void Start()
    {
        // Set the firePoint position to a static value
        if (firePoint == null)
        {
            // Set it to a static position relative to the PlayerShip
            firePoint = new GameObject("FirePoint").transform;
            firePoint.position = new Vector3(0, 1, 0); // Change these values to whatever static position you want
        }

        delayTimer = inputDelay; // Start the initial delay timer
    }

    void Update()
    {
        if (delayTimer > 0)
        {
            delayTimer -= Time.deltaTime;
            return; // Skip input until delay is over
        }

        // Cooldown timer countdown
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        // Only shoot if cooldown is 0 or less
        if (cooldownTimer <= 0 && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)))
        {
            Shoot();
            cooldownTimer = shootCooldown; // Reset the cooldown after shooting
        }
    }

    void Shoot()
    {
        GameObject thunder = Instantiate(thunderPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = thunder.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.down * bulletSpeed;
        }
    }
}
