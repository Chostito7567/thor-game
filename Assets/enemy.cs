using UnityEngine;

public class ZombieFollowAndDisappear : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    public float stoppingDistance = 0.5f;
    public int hitCounter = 3;

    private RandomEnemySpawner spawner; // reference to spawner

    void Start()
    {
        // Find the spawner in the scene (you can tag it if you want too)
        spawner = FindObjectOfType<RandomEnemySpawner>();
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > stoppingDistance)
            {
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;

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
            if (spawner != null)
            {
                spawner.EnemyDefeated(gameObject); // tell the spawner this enemy died!
            }
            Destroy(gameObject);
        }

        Destroy(other.gameObject);
    }
}
