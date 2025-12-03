using System.Buffers;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Transform player;
    public NavMeshAgent agent;
    private bool isInLightZone = false;

    [Header("Difficulty Scaling")]
    [SerializeField] private float baseSpeed = 3.5f;
    [SerializeField] private float speedIncreasePerMemory = 0.5f;
    [SerializeField] private float baseAcceleration = 8f;
    [SerializeField] private float accelerationIncreasePerMemory = 1f;
    [SerializeField] private float maxSpeed = 10f;

    [Header("Light Zone Teleportation")]
    [SerializeField] private float teleportDistance = 15f;
    [SerializeField] private float teleportCooldown = 2f;
    private float lastTeleportTime = -999f;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip teleportSound;
    [SerializeField] private AudioClip aggressionIncreaseSound;
    [SerializeField] private AudioClip chaseSound;
    [SerializeField] private bool loopChaseSound = true;

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

        // Set initial values
        agent.speed = baseSpeed;
        agent.acceleration = baseAcceleration;

        // Setup audio source if not assigned
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }

    void Start()
    {
        // Subscribe to memory collection event
        MemoryTracker memoryTracker = Object.FindFirstObjectByType<MemoryTracker>();
        if (memoryTracker != null)
        {
            memoryTracker.OnMemoryCollected += IncreaseAggression;
            UpdateAggression(memoryTracker.GetCollectedMemoryCount());
        }
        else
        {
            Debug.LogWarning("[ENEMY] MemoryManager not found!");
        }

        // Start chase sound if assigned
        if (chaseSound != null && loopChaseSound)
        {
            audioSource.clip = chaseSound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    void Update()
    {
        if (isInLightZone)
        {
            StopMoving();
            TryTeleportAway();
        }
        else
        {
            ChasePlayer();
        }
    }

    private void IncreaseAggression()
    {
        MemoryTracker memoryTracker = Object.FindFirstObjectByType<MemoryTracker>();
        if (memoryTracker != null)
        {
            UpdateAggression(memoryTracker.GetCollectedMemoryCount());
        }

        // Play aggression increase sound
        PlaySound(aggressionIncreaseSound);
    }

    private void UpdateAggression(int memoryCount)
    {
        float newSpeed = Mathf.Min(baseSpeed + (speedIncreasePerMemory * memoryCount), maxSpeed);
        float newAcceleration = baseAcceleration + (accelerationIncreasePerMemory * memoryCount);

        agent.speed = newSpeed;
        agent.acceleration = newAcceleration;

        Debug.Log($"[ENEMY] Aggression increased! Speed: {newSpeed}, Acceleration: {newAcceleration}");
    }

    private void TryTeleportAway()
    {
        // Check if cooldown has passed
        if (Time.time - lastTeleportTime < teleportCooldown)
            return;

        // Find a random position away from the light
        Vector3 randomDirection = Random.insideUnitSphere * teleportDistance;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, teleportDistance, NavMesh.AllAreas))
        {
            TeleportToPosition(hit.position, Quaternion.identity);
            Debug.Log("[ENEMY] Teleported away from light zone!");
        }
    }

    /// <summary>
    /// Public method to teleport enemy to a specific position. Called by EnemyTeleportTrigger.
    /// </summary>
    public void TeleportToPosition(Vector3 position, Quaternion rotation)
    {
        if (agent == null) return;

        // Disable agent before teleporting
        agent.enabled = false;

        // Teleport
        transform.position = position;
        transform.rotation = rotation;

        // Re-enable agent after a frame to ensure NavMesh syncs
        agent.enabled = true;

        // Reset path to prevent weird movement
        agent.ResetPath();

        lastTeleportTime = Time.time;
        isInLightZone = false;

        // Play teleport sound
        PlaySound(teleportSound);
    }

    private void ChasePlayer()
    {
        if (!agent.enabled) return;
        agent.isStopped = false;
        agent.SetDestination(player.position);
    }

    private void StopMoving()
    {
        if (!agent.enabled) return;
        agent.isStopped = true;
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightZ"))
        {
            isInLightZone = true;
            Debug.Log("[ENEMY] Entered light zone — stopping.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LightZ"))
        {
            isInLightZone = false;
            Debug.Log("[ENEMY] Exited light zone — resuming chase.");
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from event to prevent memory leaks
        MemoryTracker memoryTracker = Object.FindFirstObjectByType<MemoryTracker>();
        if (memoryTracker != null)
        {
            memoryTracker.OnMemoryCollected -= IncreaseAggression;
        }
    }
}