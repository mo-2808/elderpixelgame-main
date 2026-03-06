using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private void OTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // go to next level
            SceneController.instance.NextLevel();
        }
    }
}
