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
    //public CommentaryScript commentaryScript; // Reference to the CommentaryScript

    //private bool touchedGroundItem = false;

    void Start()
    {
        currentLife = maxLife;
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //originalColor = spriteRenderer.color;
        UpdateLifeCountText();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cactus"))
        {
            TakeDamage();
            //StartCoroutine(FlashSprite());
            PlayHitSound(); // Play the hit sound
            UpdateLifeCountText();
        }
    }

    void TakeDamage()
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
        /* if (commentaryScript != null)
        {
            commentaryScript.UpdateCommentaryText("Oops, looks like someone tripped over their own shoelaces! Let's try that again, shall we?");
            StartCoroutine(ClearCommentaryAfterDelay(4f)); // Clear commentary after 4 seconds
        } */
    }

    /* IEnumerator ClearCommentaryAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (commentaryScript != null)
        {
            commentaryScript.UpdateCommentaryText(""); // Clear commentary text
        }
    }

    IEnumerator FlashSprite()
    {
        spriteRenderer.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    } */

    void UpdateLifeCountText()
    {
        lifeCountText.text = currentLife.ToString() + " lives";
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
