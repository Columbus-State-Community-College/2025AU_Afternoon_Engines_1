using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MemoryPuzzle : MonoBehaviour
{

    [SerializeField] TMP_InputField userAnswer;
    [SerializeField] GameObject puzzleTrigger;

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
            Destroy(puzzleTrigger.gameObject); //makes memory accessible
            Time.timeScale = 1.0f;
            gameObject.SetActive(false);
        }

        else
        {
            hintText.text = "Incorrect";
            userAnswer.text = "";
        }


    }

    public void ExitPuzzle()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
