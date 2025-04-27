using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieFollowAndDisappear : MonoBehaviour
{

    public string deathScene = "Freaky_Loki"; // ← Set this in Inspector OR hardcode your next scene name
    public Transform player;
    public float speed = 2f;
    public float stoppingDistance = 0.5f;

    private bool isDead = false;
    private GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (player != null && !isDead)
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
        if (isDead) return;

        if (other.CompareTag("Bullet"))
        {
            Die();
            Destroy(other.gameObject); // destroy the bullet too
        }
        else if (other.CompareTag("Player"))
        {
            Debug.Log("You died!");

            LoadDeathScene();

            // if (player != null)
            // {
            //     player.TakeDamage();
            // }
            // Die();
        }
    }

    public void LoadDeathScene()
    {
        SceneManager.LoadScene(deathScene);
    }

    void Die()
    {
        isDead = true;

        if (gm != null)
        {
            gm.AddKill(); // +1 kill
        }

        Destroy(gameObject);
    }
}