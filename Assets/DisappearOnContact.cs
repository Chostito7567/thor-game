using UnityEngine;

public class DisappearOnContact : MonoBehaviour
{
    public int hitCounter = 3;
    // This function will be called when the object enters the trigger zone
    void OnTriggerEnter2D(Collider2D other)
    {
        hitCounter -= 1;
        if(hitCounter == 0){
            // Destroy(gameObject); reworked to end scene
        }
        Destroy(other.gameObject);
    }
}
