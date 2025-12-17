using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeypadSystem : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private Button[] numberButtons;
    [SerializeField] private Button clearButton;
    [SerializeField] private Button enterButton;

    [Header("Settings")]
    [SerializeField] private string correctCode = "1234";
    [SerializeField] private int maxDigits = 4;

    [Header("Door Reference")]
    [SerializeField] private GameObject door;
    [SerializeField] private float doorOpenDistance = 3f;
    [SerializeField] private float doorOpenSpeed = 2f;

    private string enteredCode = "";
    private bool isDoorOpen = false;
    private Vector3 doorClosedPosition;
    private Vector3 doorOpenPosition;

    void Start()
    {
        // Store door positions
        if (door != null)
        {
            doorClosedPosition = door.transform.position;
            doorOpenPosition = doorClosedPosition + Vector3.up * doorOpenDistance;
        }

        // Setup number buttons (0-9)
        for (int i = 0; i < numberButtons.Length && i < 10; i++)
        {
            int number = i;
            numberButtons[i].onClick.AddListener(() => OnNumberPressed(number));
        }

        // Setup clear and enter buttons
        if (clearButton != null)
            clearButton.onClick.AddListener(OnClearPressed);

        if (enterButton != null)
            enterButton.onClick.AddListener(OnEnterPressed);

        UpdateDisplay();
    }

    void Update()
    {
        // Animate door opening/closing
        if (door != null && isDoorOpen)
        {
            door.transform.position = Vector3.Lerp(
                door.transform.position,
                doorOpenPosition,
                Time.deltaTime * doorOpenSpeed
            );
        }
    }

    void OnNumberPressed(int number)
    {
        if (enteredCode.Length < maxDigits)
        {
            enteredCode += number.ToString();
            UpdateDisplay();
        }
    }

    void OnClearPressed()
    {
        enteredCode = "";
        UpdateDisplay();
        displayText.color = Color.white;
    }

    void OnEnterPressed()
    {
        if (enteredCode.Length == maxDigits)
        {
            if (enteredCode == correctCode)
            {
                // Correct code
                displayText.text = "ACCESS GRANTED";
                displayText.color = Color.green;
                OpenDoor();
                Invoke(nameof(ResetKeypad), 2f);
            }
            else
            {
                // Wrong code
                displayText.text = "ACCESS DENIED";
                displayText.color = Color.red;
                Invoke(nameof(OnClearPressed), 1.5f);
            }
        }
    }

    void UpdateDisplay()
    {
        if (displayText != null)
        {
            // Show asterisks for security
            displayText.text = new string('*', enteredCode.Length);

            // Or show actual numbers (less secure):
            // displayText.text = enteredCode;
        }
    }

    void OpenDoor()
    {
        isDoorOpen = true;
        Debug.Log("Door opened!");
    }

    void ResetKeypad()
    {
        enteredCode = "";
        UpdateDisplay();
        displayText.color = Color.white;
    }

    // Optional: Close door after delay
    public void CloseDoor()
    {
        if (door != null)
        {
            door.transform.position = doorClosedPosition;
            isDoorOpen = false;
        }
    }
}