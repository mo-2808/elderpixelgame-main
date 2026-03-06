using UnityEngine;

public class Door : MonoBehaviour
{
    private Collider2D collider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Key"))
        {
            Debug.Log("Door");
            collider.enabled = false;
        }
    }


    void OnCollisionExit2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
        }
    }
}
