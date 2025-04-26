using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed = 5f; // You can adjust this in the Inspector

    // Update is called once per frame
    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal"); // Detects Left/Right Arrow or A/D
        Vector3 move = new Vector3(moveInput, 0f, 0f); // Move only along X-axis
        transform.position += move * speed * Time.deltaTime;
    }
}
