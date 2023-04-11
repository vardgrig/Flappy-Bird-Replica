using UnityEngine;

public class PipeDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
            Destroy(collision.gameObject);
    }
}
