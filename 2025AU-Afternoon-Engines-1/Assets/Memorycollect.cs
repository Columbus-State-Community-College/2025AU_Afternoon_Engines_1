using UnityEngine.AI;
using UnityEngine;

public class MemoryCollectible : MonoBehaviour
{
    [Header("Memory Settings")]
    public string memoryName = "Memory";

    [Header("Lights and Obstacles Controlled by this Collectable")]
    public GameObject[] spotlights;
    public NavMeshObstacle[] lightObstacles;
    public Collider[] lightTriggers; // For regular light zone triggers
    public GameObject[] teleportTriggerObjects; // NEW - For teleport triggers (entire GameObjects)

    [Header("Audio")]
    [SerializeField] private AudioClip collectionSound;
    [SerializeField] private float soundVolume = 1f;

    private void Awake()
    {
        // Make sure all start off
        foreach (var light in spotlights)
        {
            if (light != null)
                light.SetActive(false);
        }

        foreach (var obstacle in lightObstacles)
        {
            if (obstacle != null)
                obstacle.enabled = false;
        }

        foreach (var trigger in lightTriggers)
        {
            if (trigger != null)
                trigger.enabled = false;
        }

        // Disable teleport trigger GameObjects
        foreach (var teleportTrigger in teleportTriggerObjects)
        {
            if (teleportTrigger != null)
                teleportTrigger.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"[MEMORY] {memoryName} being collected...");

            // Turn on all spotlights
            foreach (var light in spotlights)
            {
                if (light != null)
                    light.SetActive(true);
            }

            // Enable all obstacles
            foreach (var obstacle in lightObstacles)
            {
                if (obstacle != null)
                    obstacle.enabled = true;
            }

            // Enable all light zone triggers
            foreach (var trigger in lightTriggers)
            {
                if (trigger != null)
                {
                    trigger.enabled = true;
                    Debug.Log($"[MEMORY] Light trigger enabled: {trigger.gameObject.name}");
                }
            }

            // Enable all teleport trigger GameObjects
            foreach (var teleportTrigger in teleportTriggerObjects)
            {
                if (teleportTrigger != null)
                {
                    teleportTrigger.SetActive(true);
                    Debug.Log($"[MEMORY] Teleport trigger enabled: {teleportTrigger.name}");
                }
            }

            // Update memory counter
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.CollectMemory();
            }

            // Notify MemoryManager
            MemoryTracker memoryTracker = Object.FindFirstObjectByType<MemoryTracker>();
            if (memoryTracker != null)
            {
                memoryTracker.NotifyMemoryCollected(); // Use the instance of MemoryTracker to call the method
                Debug.Log("[MEMORY] MemoryManager notified!");
            }
            else
            {
                Debug.LogError("[MEMORY] MemoryManager NOT FOUND!");
            }

            // Play collection sound
            if (collectionSound != null)
            {
                AudioSource.PlayClipAtPoint(collectionSound, transform.position, soundVolume);
            }

            // Disable the collectable itself
            gameObject.SetActive(false);
        }
    }
}