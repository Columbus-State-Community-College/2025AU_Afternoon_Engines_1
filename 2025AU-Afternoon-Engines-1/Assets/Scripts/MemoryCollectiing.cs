using UnityEngine;

public class MemoryCollectiing : MonoBehaviour
{

    //script for progressing memory collection
    // prevents player from running around and collecting everything at once


    [SerializeField] GameObject memory1;
    [SerializeField] GameObject memory2;
    [SerializeField] GameObject memory3;
    [SerializeField] GameObject memory4;
    [SerializeField] GameObject memory5;

    [SerializeField] GameObject dialougePanel;


    private void Start()
    {
        // hides all memory besides the first 
        memory2.SetActive(false);
        memory3.SetActive(false);
        memory4.SetActive(false);
        memory5.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
            if(other.CompareTag("Memory1"))
        {
            memory2.SetActive(true);
            Debug.Log("memory 2 has spawned");
            
        }
        if (other.CompareTag("Memory2"))
        {
            memory3.SetActive(true);
            Debug.Log("memory 3 has spawned");
        }
        if (other.CompareTag("Memory3"))
        {
            memory4.SetActive(true);
            Debug.Log("memory 4 has spawned");
        }
        if (other.CompareTag("Memory4"))
        {
            memory5.SetActive(true);
            Debug.Log("memory 5 has spawned");
        }

       
    }
}
