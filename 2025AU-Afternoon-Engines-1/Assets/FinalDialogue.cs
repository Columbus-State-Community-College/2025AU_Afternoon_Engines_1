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
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (MemoryManager.popupClosed)
            {
                audioManager.PlaySFX(audioManager.doorClose);
                FindFirstObjectByType<DialougeSystem>().StartDialouge(finalDialogue);
                //Destroy(gameObject);
                

             
               







            }

            

            
           

        }


                
            
        
       
    }


    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
            audioManager.AmbienceSource.Stop();
            audioManager.PlayJumpScare(audioManager.finalChase);
        }
    }










}
