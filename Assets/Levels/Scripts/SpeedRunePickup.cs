using UnityEngine;

public class SpeedRunePickup : MonoBehaviour
{
    public float speedMultiplier = 1.5f;
    public float duration = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision
                .GetComponent<PlayerMovement>()
                .ApplySpeedBoost(speedMultiplier, duration);

                Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
