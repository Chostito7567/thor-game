using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) // Make sure your enemies have tag Enemy
        {
            Destroy(gameObject); // Destroy bullet immediately when hitting enemy
        }
    }
}
