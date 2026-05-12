using UnityEngine;
using UnityEngine.Rendering;

public class ElderShardPickup : MonoBehaviour
{
    public int shardValue = 1;
    public float floatSpeed = 2f;
    public float floatHeight = 0.25f;
    public float rotationSpeed = 90f;

    [SerializeField] private float respawnTime = 5f;

    [Header("Dependencies")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D collision;

    private bool _isCollected;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.Instance.AddElderShards(shardValue);

            ToggleState(false);
            _isCollected = true;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
    }

    private float _capturedTime;
    // Update is called once per frame
    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        DoRespawnTimer();
    }

    private void DoRespawnTimer()
    {
        if (_isCollected)
        {
            _capturedTime += Time.deltaTime;
            if (_capturedTime > respawnTime)
            {
                _capturedTime = 0f;
                _isCollected = false;
                ToggleState(true);
            }
        }
    }

    private void ToggleState(bool state)
    {
        collision.enabled = state;
        spriteRenderer.enabled = state;
    }
}
