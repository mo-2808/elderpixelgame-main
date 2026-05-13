using UnityEngine;
using TMPro;

public class ElderShardUI : MonoBehaviour
{
    public TextMeshProUGUI shardText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shardText.text = GameManager.Instance.elderShards.ToString();
    }
}
