using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class DialogueTrigger : MonoBehaviour
{

    public string[] lines;

    private void Start()
    {
        FindFirstObjectByType<DialougeSystem>().StartDialouge(lines);
    }
    
        
    





}
