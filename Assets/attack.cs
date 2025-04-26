using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public GameObject thunderPrefab; // Assign your thunder prefab here
    public float bulletSpeed = 10f;   // Speed at which the thunder flies
    public Transform firePoint;      // Where the bullet comes out from

    void Start()
    {
        // Set the firePoint position to a static value
        if (firePoint == null)
        {
            // Set it to a static position relative to the PlayerShip
            firePoint = new GameObject("FirePoint").transform;
            firePoint.position = new Vector3(0, 1, 0); // Change these values to whatever static position you want
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            Shoot();
        }
    }

    // void Shoot()
    // {
    //     GameObject thunder = Instantiate(thunderPrefab, firePoint.position, Quaternion.identity);
    //     Rigidbody2D rb = thunder.GetComponent<Rigidbody2D>();
    //     if (rb != null)
    //     {
    //         rb.velocity = Vector2.up * bulletSpeed;
    //     }
    // }
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
