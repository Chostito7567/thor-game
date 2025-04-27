using UnityEngine;

public class ZombieFollowAndDisappear : MonoBehaviour
{
    public Transform player;     // Assign player in Inspector
    public float speed = 2f;     // Movement speed
    public float stoppingDistance = 0.5f; // Stops when close
    public int hitCounter = 3;   // How many hits before enemy disappears

    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > stoppingDistance)
            {
                // Move towards player
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;

                // Optional: Face the player (2D flip)
                Vector3 scale = transform.localScale;
                scale.x = player.position.x > transform.position.x ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
                transform.localScale = scale;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        hitCounter -= 1;

        if (hitCounter <= 0)
        {
            Destroy(gameObject); // Enemy disappears after enough hits
        }

        Destroy(other.gameObject); // Destroys the object it collides with
    }
}
