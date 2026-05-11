using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public int shardReward = 5;
    public float knockbackForce = 5f;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage, Vector2 hitDirection)
    {
        currentHealth -= damage;
        GetComponent<Guard>().canMove = false;
        //Apply Knockback
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(-hitDirection * knockbackForce, ForceMode2D.Impulse);

        if(currentHealth <=0)
        {
            Die();
        }
        Invoke("StartMoving", 0.5f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void StartMoving()
    {
        GetComponent<Guard>().canMove = true;
    }

    void Die()
    {
        FindFirstObjectByType<GameManager>().AddElderShards(shardReward);
       // GameManager.Instance.AddElderShards(shardReward);

        Debug.Log("Enemy defeated +" + shardReward + " shards");

        Destroy(gameObject);
    }
}
