
using UnityEngine;

public class FallingSpinningBrick : MonoBehaviour
{

    private Rigidbody2D rb;
    private Vector3 startPosition;

    [Range(5f, 50f)]
    public float maxDistance = 15f; // distance before it resets
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        // check if the object has fallen too far from its start position
        if (Vector3.Distance(transform.position, startPosition) > maxDistance)
        {
            transform.position = startPosition;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag ("Player"))
        {
            other.GetComponent<PlayerMovement>().Reset();
        }
    }

}
