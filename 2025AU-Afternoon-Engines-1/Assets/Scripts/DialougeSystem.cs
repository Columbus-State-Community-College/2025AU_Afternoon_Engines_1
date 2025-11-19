using UnityEngine;
using TMPro;
using System.Collections;

public class DialougeSystem : MonoBehaviour
{
    public TextMeshProUGUI dialougeText;
    public string[] dialougeLines;
    public float textSpeed;

    private int currentIndex;

    AudioManager SFX;


    private void Awake()
    {
        SFX = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    void Start()
    {
        Time.timeScale = 0f;
        dialougeText.text = string.Empty;
        StartDialouge();
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
       
    }

    void StartDialouge()
    {
        currentIndex = 0;
        StartCoroutine(TypeoutDialouge());
    }

    // coroutine "types" the lines

    IEnumerator TypeoutDialouge()
    {
        foreach (char c in dialougeLines[currentIndex].ToCharArray())
        {
            SFX.PlaySFX(SFX.dialougeClick);
            dialougeText.text += c;

            yield return new WaitForSecondsRealtime(textSpeed);

        }
    }

    void goToNextLine()
    {
        if (currentIndex < dialougeLines.Length -1)
        {
            currentIndex++;
            dialougeText.text = string.Empty;
            StartCoroutine(TypeoutDialouge());
        }

        else
        {
            gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }
}
  