using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour
{

    private Queue<string> lines;
    public TextMeshProUGUI dialoguetext;
    public GameObject dialogueBox;
    public float speed = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        lines = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        
        Time.timeScale = 0f;
        lines.Clear();

        foreach (string line in lines)
        {
              lines.Enqueue(line);  
        }

        GotoNextLine();
    }

    public void GotoNextLine()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            if (lines.Count == 0)
            {
                EndDialogue();
                return;
            }

            string line = lines.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeOutDialogue(line));

        }
    }



    void EndDialogue()
    {
        dialogueBox.SetActive(false);
        Time.timeScale = 1.0f;
    }
    
    IEnumerator TypeOutDialogue(string line)
    {
        dialoguetext.text = "";
        foreach(char c in line.ToCharArray())
        {
            dialoguetext.text += c;
            yield return new WaitForSecondsRealtime(speed);
        }
    }
    
}
