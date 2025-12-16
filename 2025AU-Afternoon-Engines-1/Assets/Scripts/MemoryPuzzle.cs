using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MemoryPuzzle : MonoBehaviour
{

    [SerializeField] TMP_InputField userAnswer;
    [SerializeField] GameObject puzzleTrigger;
    [SerializeField] GameObject puzzleBarrier;

    string puzzleAnswer = "NOSTALGIA";
    public TextMeshProUGUI hintText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        userAnswer.onEndEdit.AddListener(checkAnswer);
    }

    void checkAnswer(string answer)
    {
        if (answer.ToUpper() == puzzleAnswer.ToUpper())
        {
            hintText.text = "Correct.";
            Destroy(puzzleTrigger.gameObject);
            Destroy(puzzleBarrier.gameObject);
            //makes memory accessible
            Time.timeScale = 1.0f;
            gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }

        else
        {
            hintText.text = "Incorrect";
            userAnswer.text = "";
        }


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            gameObject.SetActive(false);
            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;


        }
    }
}
