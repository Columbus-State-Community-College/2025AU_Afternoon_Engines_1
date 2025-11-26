using UnityEngine;

public class FlashlightCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Entered flashlight range");

            // Get the EnemyController component from the enemy that entered
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null && enemy.agent != null)
            {
                enemy.agent.speed = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Exited flashlight range");

            // Get the EnemyController component from the enemy that exited
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null && enemy.agent != null)
            {
                enemy.agent.speed = 4;
            }
        }
    }
}