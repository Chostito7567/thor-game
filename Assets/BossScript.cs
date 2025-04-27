using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class BossController : MonoBehaviour
{
    // [Header("References")]
    // [Tooltip("Drag your Player GameObject here")]
    public Transform player;

    // [Header("Movement")]
    // [Tooltip("Movement speed (units/sec)")]
    public float speed = 2f;
    // [Tooltip("How close before boss stops moving")]
    public float stoppingDistance = 0.0f;

    [Header("Boss Stats")]
    [Tooltip("Number of hits boss can take")]
    public int hitCounter = 3;
    [Tooltip("Name of the scene to load when boss dies (leave blank to just destroy boss)")]
    public string nextSceneName = "Trophy-Thing";

    // Internals
    private bool _isDead = false;
    private SpriteRenderer _spriteRenderer;
    private Collider2D    _collider2d;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2d     = GetComponent<Collider2D>();
    }

    public float moveSpeed = 15f;  // Speed at which the sprite moves
    public float moveInterval = 0f; // Time interval between random positions
    public Vector2 minRange = new Vector2(0f, 0f);  // Minimum range for random movement
    public Vector2 maxRange = new Vector2(20f, 20f);    // Maximum range for random movement

    private Vector2 targetPosition;
    private bool isMoving = false;



    void OnTriggerEnter2D(Collider2D other)
    {
        // Only react to objects tagged "Projectile"
        ///if (!other.CompareTag("Projectile"))
            //return;

        // Prevent multiple triggers after death
        if (_isDead)
            return;

        hitCounter--;
        Debug.Log($"Boss hit! Remaining life: {hitCounter}");

        Destroy(other.gameObject);

        if (hitCounter <= 0)
        {
            _isDead = true;
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
