using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int maxLife = 3;
    private int currentLife;
    private SpriteRenderer spriteRenderer;
    public Color flashColor = Color.red;
    public float flashDuration = 0.2f;
    private Color originalColor;
    public Text lifeCountText;
    public AudioSource hitSound; // Reference to the hit sound
    public AudioSource respawnSound; // Reference to the respawn sound

    void Start()
    {
        currentLife = maxLife;
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //originalColor = spriteRenderer.color;
        // UpdateLifeCountText(); <--- REMOVE THIS LINE
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cactus"))
        {
            TakeDamage();
            //StartCoroutine(FlashSprite());
            PlayHitSound(); // Play the hit sound
            // UpdateLifeCountText(); <--- REMOVE THIS LINE TOO
        }
    }

    public void TakeDamage()
    {
        currentLife--;

        if (currentLife <= 0)
        {
            Die();
            currentLife = maxLife; // Reset life count
        }
    }

    void Die()
    {
        transform.position = new Vector3(-12.74f, -3.07f, 0f);
        PlayRespawnSound(); // Play respawn sound
    }

    void PlayHitSound()
    {
        if (hitSound != null)
        {
            hitSound.Play();
        }
    }

    void PlayRespawnSound()
    {
        if (respawnSound != null)
        {
            respawnSound.Play();
        }
    }
}
