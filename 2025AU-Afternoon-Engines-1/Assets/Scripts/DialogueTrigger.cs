using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class DialogueTrigger : MonoBehaviour
{

    public string[] lines;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    private void Start()
    {
        
        audioManager.PlaySFX(audioManager.doorClose);
        FindFirstObjectByType<DialougeSystem>().StartDialouge(lines);
    }
    
        
    





}
