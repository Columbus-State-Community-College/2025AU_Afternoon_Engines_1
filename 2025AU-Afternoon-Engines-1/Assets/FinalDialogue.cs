using Unity.VisualScripting;
using UnityEngine;

public class FinalDialogue : MonoBehaviour
{
    public string[] finalDialogue;
    AudioManager audioManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        GetComponent<MemoryManager>();
        GetComponent<DialougeSystem>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

   


    // Update is called once per frame
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (MemoryManager.popupClosed)
            {
                FindFirstObjectByType<DialougeSystem>().StartDialouge(finalDialogue);
                Destroy(gameObject);
                

             
               







            }

            

            
           

        }


                
            
        
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioManager.PlaySFX(audioManager.doorClose);
        }
    }

    








}
