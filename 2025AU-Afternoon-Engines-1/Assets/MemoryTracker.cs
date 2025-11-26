using UnityEngine;

public class MemoryTracker : MonoBehaviour
{
    // Event system for enemy aggression
    public delegate void MemoryCollectedHandler();
    public event MemoryCollectedHandler OnMemoryCollected;

    private int collectedMemories = 0;

    [Header("Memory Tracking")]
    [SerializeField] private int totalMemories = 5; // Adjust to your game's total

    private void Awake()
    {
        // Ensure only one MemoryManager exists
        if (Object.FindObjectsByType<MemoryTracker>(FindObjectsSortMode.None).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    public void NotifyMemoryCollected()
    {
        collectedMemories++;
        Debug.Log($"[MEMORY MANAGER] Memory collected! Total: {collectedMemories}/{totalMemories}");

        // Trigger the event to notify all subscribers (like the enemy)
        OnMemoryCollected?.Invoke();
    }

    public int GetCollectedMemoryCount()
    {
        return collectedMemories;
    }

    public int GetTotalMemories()
    {
        return totalMemories;
    }

    public float GetCompletionPercentage()
    {
        return (float)collectedMemories / totalMemories * 100f;
    }

    public bool AllMemoriesCollected()
    {
        return collectedMemories >= totalMemories;
    }

    public void ResetMemories()
    {
        collectedMemories = 0;
    }
}
