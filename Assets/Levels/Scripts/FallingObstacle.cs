using UnityEngine;

public class FallingObstacle : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasFallen = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; // stays still at start
    }

    public void Drop()
    {
        if (!hasFallen)
        {
            rb.bodyType = RigidbodyType2D.Dynamic; // gravity makes it fall
            hasFallen = true;
        }
    }
}
