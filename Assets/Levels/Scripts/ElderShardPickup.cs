using UnityEngine;
using UnityEngine.Rendering;

public class ElderShardPickup : MonoBehaviour
{
    public int shardValue = 1;
    public float floatSpeed = 2f;
    public float floatHeight = 0.25f;
    public float rotationSpeed = 90f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.Instance.AddElderShards(shardValue);
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
