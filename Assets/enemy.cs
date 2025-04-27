using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ZombieFollowAndDisappear : MonoBehaviour
{
    [Header("References")]
    public Transform player;

    [Header("Movement")]
    public float speed = 2f;
    public float stoppingDistance = 0.5f;

    [Header("Boss Settings")]
    public int hitCounter = 3;
    public int bossFlag;          // 1 if boss
    public string nextSceneName = "Trophy-Thing";  // Set this in Inspector for boss only

    // How far below the camera to spawn
    public float spawnOffsetY = -10f;

    private bool isSleeping = false;
    private SpriteRenderer spriteRenderer;
    private Collider2D col2d;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col2d = GetComponent<Collider2D>();
    }

    void Start()
    {
        if (bossFlag == 1)
            StartCoroutine(BossSleepRoutine());
    }

    void Update()
    {
        if (player == null || (bossFlag == 1 && isSleeping))
            return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > stoppingDistance)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            transform.position += dir * speed * Time.deltaTime;

            Vector3 scale = transform.localScale;
            scale.x = player.position.x > transform.position.x
                      ? Mathf.Abs(scale.x)
                      : -Mathf.Abs(scale.x);
            transform.localScale = scale;
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
        hitCounter--;
        Destroy(other.gameObject);

        if (hitCounter <= 0)
        {
            if (bossFlag == 1)
            {
                // Boss died → load next scene
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                // Regular NPC or no scene specified → just destroy
                Destroy(gameObject);
            }
        }
    }
}
