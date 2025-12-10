using UnityEngine;

public class FlashlightColllision : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Entered flashlight range");
            EnemyController.agent.speed = 0;
            EnemyController.animator.speed = 0;

        } 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Exited flashlight range");
            EnemyController.agent.speed = 4;
            EnemyController.animator.speed = 1f;

        }
    }
}
