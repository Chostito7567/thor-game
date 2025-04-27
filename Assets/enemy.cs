using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ZombieFollowAndDisappear : MonoBehaviour
{
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
            Vector3 dir = (player.position - transform.position).normalized;
            transform.position += dir * speed * Time.deltaTime;

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

    private IEnumerator BossSleepRoutine()
    {
        isSleeping = true;
        spriteRenderer.enabled = false;
        if (col2d != null) col2d.enabled = false;

        Vector3 offScreenPos = transform.position;
        offScreenPos.y += spawnOffsetY;
        transform.position = offScreenPos;

        yield return new WaitForSeconds(15f);

        Vector3 spawnPos = player.position;
        spawnPos.y += spawnOffsetY;
        spawnPos.x = transform.position.x;
        transform.position = spawnPos;

        spriteRenderer.enabled = true;
        if (col2d != null) col2d.enabled = true;

        isSleeping = false;
        bossFlag = 0;
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
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage();
            }
            Die();
        }
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
