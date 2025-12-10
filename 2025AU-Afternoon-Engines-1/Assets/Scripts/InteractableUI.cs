using UnityEngine;
using TMPro;

public class InteractableUI : MonoBehaviour
{

    public GameObject interactText;
    public TextMeshProUGUI text;
    public float fadeSpeed = 8f;

    private CanvasGroup canvasGroup;
    private bool showing = false;

    void Awake()
    {
        if (interactText != null)
        {
            interactText.SetActive(true);
        }

        canvasGroup = interactText.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = interactText.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0f;
    }

    void Update()
    {
        float targetAlpha = showing ? 1f : 0f;
        canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, Time.deltaTime * fadeSpeed);
    }

    public void Show(string message)
    {
        text.text = message;
        showing = true;
    }

    public void Hide()
    {
        showing = false;
    }
}
