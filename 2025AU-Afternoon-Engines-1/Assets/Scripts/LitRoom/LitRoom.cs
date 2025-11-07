using UnityEngine;

public class LitRoom : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) EnemyController.isInLightZone = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) EnemyController.isInLightZone = false;
    }
}
