using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Transform player;
    public static NavMeshAgent agent;
    public static Animator animator;

    public static bool isInLightZone = false;
    AudioManager audioManager;
    GameObject memory5; //for final chase
 

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
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
        animator.SetBool("Walking", true);
        agent.SetDestination(player.position);
        if (MemoryCollectiing.finalMemorycollected)
        {
            agent.speed = 8;
            animator.speed = 2f;
           
        }
    }

    private void StopMoving()
    {
        if (!agent.enabled) return;

        agent.isStopped = true;
        animator.SetBool("Walking", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightZ"))
        {
            isInLightZone = true;
            Debug.Log("[ENEMY] Entered light zone — stopping.");
        }

        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(5f);
            }
        }
    }

   



    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LightZ"))
        {
            Debug.Log("Left trigger");
            isInLightZone = false;
            Debug.Log("[ENEMY] Exited light zone — resuming chase.");
        }

    }
}
