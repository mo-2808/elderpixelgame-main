using JetBrains.Annotations;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int elderShards = 0;
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

    public void AddElderShards(int amount)
    {
        elderShards += amount;
        Debug.Log("Elder Shards: " + elderShards);
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
