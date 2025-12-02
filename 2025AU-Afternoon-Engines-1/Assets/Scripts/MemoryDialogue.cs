using UnityEngine;

public class MemoryDialogue : MonoBehaviour
{
    public string[] lines;
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
        if (other.CompareTag("Player"))
        {
            FindFirstObjectByType<DialougeSystem>().StartDialouge(lines);
            Destroy(gameObject);
        }
    }

    
}
