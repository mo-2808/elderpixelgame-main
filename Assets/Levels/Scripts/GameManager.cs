using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int elderShards = 0;

    public TextMeshProUGUI elderShardText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateText();
    }

    public void AddElderShards(int amount)
    {
        elderShards += amount;
        UpdateText();

        Debug.Log("Elder Shards: " + elderShards);
    }

    void UpdateText()
    {
        elderShardText.text = elderShards.ToString();
    }
}