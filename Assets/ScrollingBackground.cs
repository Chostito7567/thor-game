using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float speed = 0.1f;
    [SerializeField] private Renderer bgRenderer;

    void Update()
    {
        // Scroll the background down so characters seem like they're falling
        bgRenderer.material.mainTextureOffset -= new Vector2(0, speed * Time.deltaTime); // Move downward
    }
}
