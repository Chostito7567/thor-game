using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Drag your Player GameObject here")]
    public Transform player;

    [Header("Movement")]
    [Tooltip("Movement speed (units/sec)")]
    public float speed = 2f;
    [Tooltip("How close before boss stops moving")]
    public float stoppingDistance = 0.5f;

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

    void Update()
    {
        if (_isDead || player == null) 
            return;

        // Move toward player until within stopping distance
        float dist = Vector3.Distance(transform.position, player.position);
        if (dist > stoppingDistance)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            transform.position += dir * speed * Time.deltaTime;

            // Flip horizontally to face the player
            Vector3 scale = transform.localScale;
            scale.x = player.position.x > transform.position.x 
                        ? Mathf.Abs(scale.x) 
                        : -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

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
