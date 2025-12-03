using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


// PLAYER HEALTH MANAGER

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHearts = 3;
    public int currentHearts = 3;

    [Header("UI Elements")]
    public GameObject heartPrefab; 
    public Transform heartsContainer; 
    public TextMeshProUGUI memoryCounterText; 

    [Header("Memory Collection")]
    public int memoriesCollected = 0;

    [Header("Medkit Settings")]
    public int medkitCount = 0;
    public int heartsPerMedkit = 1; // How many hearts one medkit restores
    public KeyCode useMedkitKey = KeyCode.E; // Key to use medkit

    [Header("Invincibility Frames")]
    public float invincibilityDuration = 1f;
    private float invincibilityTimer = 0f;
    private bool isInvincible = false;

    private List<GameObject> heartUIElements = new List<GameObject>();

    void Start()
    {
        currentHearts = maxHearts;
        UpdateHeartsUI();
        UpdateMemoryUI();
    }

    void Update()
    {
        
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0)
            {
                isInvincible = false;
            }
        }

        // Check for medkit usage
        if (Input.GetKeyDown(useMedkitKey))
        {
            UseMedkit();
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHearts -= damage;
        currentHearts = Mathf.Max(currentHearts, 0);

        UpdateHeartsUI();

        if (currentHearts <= 0)
        {
            Die();
        }
        else
        {
           
            isInvincible = true;
            invincibilityTimer = invincibilityDuration;
        }
    }

    public void Heal(int amount)
    {
        currentHearts += amount;
        currentHearts = Mathf.Min(currentHearts, maxHearts);
        UpdateHeartsUI();
    }

    public void AddMedkit()
    {
        medkitCount++;
        Debug.Log("Medkit picked up! Total: " + medkitCount);
    }

    public void UseMedkit()
    {
        if (medkitCount > 0 && currentHearts < maxHearts)
        {
            medkitCount--;
            Heal(heartsPerMedkit);
            Debug.Log("Medkit used! Remaining: " + medkitCount);
        }
        else if (medkitCount <= 0)
        {
            Debug.Log("No medkits available!");
        }
        else if (currentHearts >= maxHearts)
        {
            Debug.Log("Health is already full!");
        }
    }

    public void CollectMemory()
    {
        memoriesCollected++;
        UpdateMemoryUI();
        Debug.Log("Memory collected! Total: " + memoriesCollected);
    }

    void UpdateMemoryUI()
    {
        if (memoryCounterText != null)
        {
            memoryCounterText.text = "Memories: " + memoriesCollected;
        }
    }

    void UpdateHeartsUI()
    {
        // Clear existing hearts
        foreach (GameObject heart in heartUIElements)
        {
            Destroy(heart);
        }
        heartUIElements.Clear();

        // Create new hearts based on current health
        for (int i = 0; i < maxHearts; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartsContainer);
            heartUIElements.Add(heart);

            // Disable/gray out hearts that are lost
            if (i >= currentHearts)
            {
                Image heartImage = heart.GetComponent<Image>();
                if (heartImage != null)
                {
                    Color c = heartImage.color;
                    c.a = 0.3f; // Make it semi-transparent
                    heartImage.color = c;
                }
            }
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
        // Add death logic here 
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }
}