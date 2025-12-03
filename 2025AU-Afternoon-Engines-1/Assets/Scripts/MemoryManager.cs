using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MemoryManager : MonoBehaviour
{
    [Header("Memory UI")]
    public GameObject memoryPanel;
    public List<GameObject> memoryPages;
    public GameObject memButtons;
    public GameObject exitMemoryText;

    private List<int> collectedIndexes = new List<int>();
    private List<GameObject> activeMemories = new List<GameObject>();
    private int currentIndex = 0;
    public static bool popupClosed = false;

    [Header("Win Screen")]
    public GameObject winScreen;

    [Header("Time Freeze Settings")]
    [SerializeField] private bool freezeTimeWhenViewing = true;

    private void Update()
    {
        CheckForWin();
    }

    public void CollectMemory(GameObject memoryPage)
    {
        int index = memoryPages.IndexOf(memoryPage);
        if (index == -1) return;

        if (!collectedIndexes.Contains(index))
            collectedIndexes.Add(index);

        memoryPanel.SetActive(true);
        memoryPage.SetActive(true);
        exitMemoryText.SetActive(true);
        popupClosed = false;

        // Freeze time when viewing memory
        if (freezeTimeWhenViewing)
        {
            Time.timeScale = 0f;
            Debug.Log("[MEMORY MANAGER] Time frozen - viewing memory");
        }
    }

    public void ClosePopup()
    {
        memoryPanel.SetActive(false);
        exitMemoryText.SetActive(false);

        foreach (GameObject mem in memoryPages)
            mem.SetActive(false);

        popupClosed = true;

        // Unfreeze time when closing popup
        if (freezeTimeWhenViewing)
        {
            Time.timeScale = 1f;
            Debug.Log("[MEMORY MANAGER] Time restored - popup closed");
        }
    }

    public void OpenMemoryPanel()
    {
        memoryPanel.SetActive(true);
        memButtons.SetActive(true);

        activeMemories.Clear();
        foreach (int i in collectedIndexes)
        {
            memoryPages[i].SetActive(false);
            activeMemories.Add(memoryPages[i]);
        }

        if (activeMemories.Count > 0)
        {
            currentIndex = 0;
            activeMemories[currentIndex].SetActive(true);
        }

        // Freeze time when opening memory panel
        if (freezeTimeWhenViewing)
        {
            Time.timeScale = 0f;
            Debug.Log("[MEMORY MANAGER] Time frozen - memory panel opened");
        }
    }

    public void CloseMemoryPanel()
    {
        memoryPanel.SetActive(false);
        memButtons.SetActive(false);

        if (activeMemories.Count > 0)
            activeMemories[currentIndex].SetActive(false);

        // Unfreeze time when closing panel
        if (freezeTimeWhenViewing)
        {
            Time.timeScale = 1f;
            Debug.Log("[MEMORY MANAGER] Time restored - memory panel closed");
        }
    }

    public void NextMemory()
    {
        if (activeMemories.Count == 0) return;

        activeMemories[currentIndex].SetActive(false);
        currentIndex = (currentIndex + 1) % activeMemories.Count;
        activeMemories[currentIndex].SetActive(true);
    }

    public void PreviousMemory()
    {
        if (activeMemories.Count == 0) return;

        activeMemories[currentIndex].SetActive(false);
        currentIndex = (currentIndex - 1 + activeMemories.Count) % activeMemories.Count;
        activeMemories[currentIndex].SetActive(true);
    }

    public void CheckForWin()
    {
        if (memoryPages.Count == collectedIndexes.Count)
        {
            if (popupClosed)
            {
                winScreen.SetActive(true);
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}