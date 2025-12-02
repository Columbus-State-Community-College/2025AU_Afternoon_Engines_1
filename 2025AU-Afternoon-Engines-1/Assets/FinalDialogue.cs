using UnityEngine;

public class FinalDialogue : MonoBehaviour
{
    public string[] finalDialogue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        GetComponent<MemoryManager>();
        GetComponent<DialougeSystem>();
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
                if (DialougeSystem.dialogueEnded)
                {
                    FindFirstObjectByType<MemoryManager>().CheckForWin();
                }







            }

            

            
           

        }


                
            
        
       
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            
        }
    }



}
