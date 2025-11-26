using UnityEngine;

/// <summary>
/// Teleports enemy to a specific location when they enter this trigger.
/// GameObject can be disabled initially and enabled by MemoryCollectible.
/// </summary>
public class EnemyTeleportTrigger : MonoBehaviour
{
    [Header("Teleport Settings")]
    [Tooltip("Where should the enemy teleport to?")]
    public Transform teleportDestination;

    [Header("Audio")]
    [SerializeField] private AudioClip teleportSound;
    [SerializeField] private float soundVolume = 1f;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the enemy entered this trigger
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null)
        {
            TeleportEnemy(enemy);
        }
    }

    private void TeleportEnemy(EnemyController enemy)
    {
        if (teleportDestination == null)
        {
            Debug.LogError("[TELEPORT TRIGGER] No teleport destination set!");
            return;
        }

        // Use the enemy's public teleport method
        enemy.TeleportToPosition(teleportDestination.position, teleportDestination.rotation);

        Debug.Log($"[TELEPORT TRIGGER] Enemy teleported to {teleportDestination.name}");

        // Play teleport sound at destination
        if (teleportSound != null)
        {
            AudioSource.PlayClipAtPoint(teleportSound, teleportDestination.position, soundVolume);
        }
    }

    // Visual helper in editor
    private void OnDrawGizmos()
    {
        if (teleportDestination != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, teleportDestination.position);
            Gizmos.DrawWireSphere(teleportDestination.position, 0.5f);
        }
    }
}