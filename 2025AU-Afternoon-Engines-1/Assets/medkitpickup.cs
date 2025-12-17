using UnityEngine;

public class Medkit : MonoBehaviour
{
    [Header("Pickup Settings")]
    public bool autoPickup = true; // Automatically pickup on collision

    void OnTriggerEnter(Collider other)
    {
        if (autoPickup && other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.AddMedkit();
                Destroy(gameObject);
            }
        }
    }
}