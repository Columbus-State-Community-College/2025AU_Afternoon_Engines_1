using TMPro;
using UnityEngine;

public class MemoryDialogue : MonoBehaviour
{

    DialougeSystem dialouge;
    public string[] lines;
    [SerializeField] GameObject dialougeBox;
    


    private void Awake()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            dialouge = GetComponent<DialougeSystem>();
            //Time.timeScale = 0f;
            Debug.Log("trigger hit");
            dialougeBox.SetActive(true);
            dialouge.dialougeText.text = string.Empty;
            dialouge.dialougeLines[dialouge.currentIndex] = lines[dialouge.currentIndex];

        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            dialouge = null;
            
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && dialouge != null) 
        {
            dialouge.StartDialouge();
        
        }
    }
}
