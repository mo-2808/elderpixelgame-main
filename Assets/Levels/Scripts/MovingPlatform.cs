
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 2f;

    private Rigidbody2D rb;
    private bool goingToB = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 target = goingToB ? (Vector2)pointB.position : (Vector2)pointA.position;

        rb.MovePosition(Vector2.MoveTowards(rb.position, target, moveSpeed * Time.fixedDeltaTime));

        if (Vector2.Distance(rb.position, target) < 0.05f)
        {
            goingToB = !goingToB; // switch direction
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            collision.gameObject.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
}

    
