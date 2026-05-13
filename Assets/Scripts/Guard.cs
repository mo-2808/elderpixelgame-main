using UnityEngine;

public class Guard : MonoBehaviour
{
    public float speed;
    private float direction = 1;

    public bool canMove = true;

    public Transform point1;
    public Transform point2;
    private Vector2 directionVector;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        directionVector = point2.position - point1.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove == false) return;

        transform.Translate(directionVector * speed * direction * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Barrier"))
        {
            direction = -direction;
        }
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().Reset();
        }
    }

    public void OnDied()
    {
        canMove = false;
        Debug.Log("Guard Died");
    }

    public void OnRespawn()
    {
        canMove = true;
        Debug.Log("Guard Respawned");
    }
}
