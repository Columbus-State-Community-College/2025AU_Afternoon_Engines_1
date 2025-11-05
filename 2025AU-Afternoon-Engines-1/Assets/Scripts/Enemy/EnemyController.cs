using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;

    private bool isInLightZone = false;

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isInLightZone)
        {
            StopMoving();
        }
        else
        {
            ChasePlayer();
        }
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
}
