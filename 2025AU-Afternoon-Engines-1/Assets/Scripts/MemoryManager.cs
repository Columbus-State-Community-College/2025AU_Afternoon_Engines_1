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
    public static bool popupClosed = true;
    
    
    [Header("Win Screen")]
    public GameObject winScreen;

    private void Update()
    {
        //CheckForWin();
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
    }


    public void ClosePopup()
    {
        memoryPanel.SetActive(false);
        exitMemoryText.SetActive(false);


        foreach (GameObject mem in memoryPages)
            mem.SetActive(false);

        popupClosed = true;
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
    }

    public void CloseMemoryPanel()
    {
        memoryPanel.SetActive(false);
        memButtons.SetActive(false);

        if (activeMemories.Count > 0)
            activeMemories[currentIndex].SetActive(false);
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

            winScreen.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;




        }
    }
}
