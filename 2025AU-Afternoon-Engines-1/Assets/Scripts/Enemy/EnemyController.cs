using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Transform player;
    public static NavMeshAgent agent;
    public static Animator animator;
     private MonsterSpawn spawn;
    private bool hasteleported = false;

    public static bool isInLightZone = false;
    AudioManager audioManager;
    GameObject memory5;
    private float teleportRange = 10f;
    //for final chase
 

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        spawn = GetComponent<MonsterSpawn>();
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
            if (!hasteleported) 
            {
                TeleportEnemy();
                Debug.Log("Teleported");
               
                hasteleported=true;
                
            }
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

    private void TeleportEnemy()
    {
        Vector3 teleport = new Vector3(Random.Range(-teleportRange, teleportRange), 0f, Random.Range(-teleportRange, teleportRange));
        transform.position = player.position + teleport;
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
