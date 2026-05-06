using UnityEngine;

public class FallingObstacleTrigger : MonoBehaviour
{
    public FallingObstacle spike;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spike.Drop();
        }
    }
}
