using UnityEngine;

public class FinalDialogue : MonoBehaviour
{
    public string[] finalDialogue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        
    }
    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindFirstObjectByType<DialougeSystem>().StartDialouge(finalDialogue);

        }


                
            
        
       
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            FindFirstObjectByType<MemoryManager>().CheckForWin();
        }
    }
}
