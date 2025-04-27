using System.Collections;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float moveSpeed = 15f;  // Speed at which the sprite moves
    public float moveInterval = 0f; // Time interval between random positions
    public Vector2 minRange = new Vector2(-5f, -5f);  // Minimum range for random movement
    public Vector2 maxRange = new Vector2(5f, 2f);    // Maximum range for random movement

    private Vector2 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        // Start the random movement coroutine
        StartCoroutine(MoveRandomly());
    }

    IEnumerator MoveRandomly()
    {
        // Run the random movement indefinitely
        while (true)
        {
            // Choose a random position within the specified range
            targetPosition = new Vector2(Random.Range(minRange.x, maxRange.x), 
                                         Random.Range(minRange.y, maxRange.y));

            // Reset the moving flag to allow movement
            isMoving = true;

            // Move the sprite to the random position smoothly
            while (isMoving)
            {
                // Move the sprite smoothly towards the target position
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                
                // If the sprite reaches the target position, stop moving
                if (Vector2.Distance(transform.position, targetPosition) <= 0.1f)
                {
                    isMoving = false;
                }

                yield return null; // Wait for the next frame
            }

            // Wait for a while before selecting the next random target
            yield return new WaitForSeconds(moveInterval);
        }
    }
}
