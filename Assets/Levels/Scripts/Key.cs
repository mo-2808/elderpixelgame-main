using UnityEngine;

public class Key : MonoBehaviour
{
    private Collider2D collider;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.parent = collision.transform;
            collider.isTrigger = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
