using UnityEngine;
using TMPro;
using System.Collections;

public class DialougeSystem : MonoBehaviour
{
    public TextMeshProUGUI dialougeText;
    
    public string[] dialougeLines;
    public float textSpeed;
    public GameObject dialogueBox;
   
   

    public int currentIndex;

    AudioManager SFX;



    private void Awake()
    {
        dialogueBox.SetActive(false);
        SFX = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (dialougeText.text == dialougeLines[currentIndex])
            {
                goToNextLine();
            }


            else
            {
                StopAllCoroutines();
                dialougeText.text = dialougeLines[currentIndex];
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) //skip all dialogue 
        {
            StopAllCoroutines();
            dialogueBox.SetActive(false);
            Time.timeScale = 1.0f;


        }
    }

    public void StartDialouge(string[] lines)
    {
        dialougeLines = lines;
        currentIndex = 0;
        dialogueBox.SetActive(true);
       Time.timeScale = 0f;
        StartCoroutine(TypeoutDialouge());
    }

    // coroutine "types" the lines

    IEnumerator TypeoutDialouge()
    {
        dialougeText.text = "";
        foreach (char c in dialougeLines[currentIndex].ToCharArray())
        {
            SFX.PlaySFX(SFX.dialougeClick);
            dialougeText.text += c;

            yield return new WaitForSecondsRealtime(textSpeed);

        }
    }

    public void goToNextLine()
    {
        if (currentIndex < dialougeLines.Length -1)
        {
            currentIndex++;
            dialougeText.text = string.Empty;
            StartCoroutine(TypeoutDialouge());
        }

        else
        {
            dialogueBox.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }
}
  