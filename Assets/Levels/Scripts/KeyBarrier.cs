using UnityEngine;

public class KeyBarrier : MonoBehaviour
{
    public int requiredShards = 30;
    public GameObject barrierVisual;

    private bool opened = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !opened)
        {
            if (GameManager.Instance.elderShards >= requiredShards)
            {
                opened = true;

                GameManager.Instance.elderShards -= requiredShards;

                Destroy(barrierVisual);
            }
        }
    }
}