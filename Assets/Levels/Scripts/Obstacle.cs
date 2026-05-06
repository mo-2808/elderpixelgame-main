using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{

    private Rigidbody2D rb;
    private bool hasFallen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; // stays still at start
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().Reset();

            // trigger falling when player touches it
            Drop();
        }
    }

    public void Drop()
    {
        if (!hasFallen)
        {
            rb.bodyType = RigidbodyType2D.Dynamic; // makes it fall
            hasFallen = true;
        }
    }
}
