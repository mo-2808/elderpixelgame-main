using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public int shardReward = 5;
    public float knockbackForce = 5f;
    private Rigidbody2D rb;

    [SerializeField] private UnityEvent deathEvent;
    [SerializeField] private UnityEvent respawnEvent;
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
    }
    
    // Update is called once per frame
    void Update()
    {
        DoRespawnTimer();
    }

    void Die()
    {
        FindFirstObjectByType<GameManager>().AddElderShards(shardReward);
       // GameManager.Instance.AddElderShards(shardReward);

        Debug.Log("Enemy defeated +" + shardReward + " shards");

        ToggleState(false);
        deathEvent?.Invoke();
        _isDead = true;
    }

    private void Respawn()
    {
        respawnEvent?.Invoke();
    }

    private float _capturedTime;
    private bool _isDead;
    [SerializeField] private float respawnTime = 20f;
    [SerializeField] private BoxCollider2D collision;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void DoRespawnTimer()
    {
        if (_isDead)
        {
            _capturedTime += Time.deltaTime;
            if (_capturedTime > respawnTime)
            {
                _capturedTime = 0f;
                _isDead = false;
                ToggleState(true);
                Respawn();
            }
        }
    }

    private void ToggleState(bool state)
    {
        collision.enabled = state;
        spriteRenderer.enabled = state;
    }
}
