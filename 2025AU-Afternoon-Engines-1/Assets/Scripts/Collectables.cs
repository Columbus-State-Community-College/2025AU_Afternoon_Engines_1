using UnityEngine;
using UnityEngine.AI;

public class Collectable : MonoBehaviour
{
    [Header("Lights and Obstacles Controlled by this Collectable")]
    public GameObject[] spotlights;          // Assign Spotlight1, Spotlight2, etc.
    public NavMeshObstacle[] lightObstacles; // Assign LightZ1, LightZ2, etc.
    public GameObject memoryNotes;
    AudioManager SFX;// for notes that appear when memory is picked uo

    private void Awake()
    {
        memoryNotes.SetActive(false);
        SFX = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        // Make sure all start off
        foreach (var light in spotlights)
        {
            if (light != null)
                light.SetActive(false);
        }

        foreach (var obstacle in lightObstacles)
        {
            if (obstacle != null)
                obstacle.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Turn on all spotlights
            foreach (var light in spotlights)
            {
                if (light != null)
                    light.SetActive(true);
            }

            // Enable all obstacles
            foreach (var obstacle in lightObstacles)
            {
                if (obstacle != null)
                    obstacle.enabled = true;
            }

            // Disable the collectable itself
            gameObject.SetActive(false);
            openNote();


            
        }
    }

    void openNote()
    {
        memoryNotes.SetActive(true);
        SFX.PlaySFX(SFX.memoryPage);


    }
}
