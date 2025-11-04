using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Transform player;

    private NavMeshAgent agent;

    public static bool PlayerInLitRoom;
    
    void Awake()
    {
        player = GameObject.Find("Player").transform;
        
        agent = GetComponent<NavMeshAgent>();

    }

    void Update()
    {

        if (!PlayerInLitRoom) ChasePlayer();

        if (PlayerInLitRoom) StopMoving();
    }

    private void ChasePlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);
    }

    private void StopMoving()
    {
        agent.isStopped = true;
    }

}
