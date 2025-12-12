using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    CharacterController characterController;

    public UIManager uiManager;
    public Slider staminaBar;
    public MemoryManager memoryManager;
    

    [Header("Speeds")]
    public float walkSpeed = 5.0f;
    public float runSpeed = 15.0f;
    private float currentSpeed;

    [Header("Memory Text")]
    public GameObject memoryPanel;
    public GameObject memory1;
    public GameObject memory2;
    public GameObject memory3;
    public GameObject memory4;
    public GameObject memory5;

    [Header("Stamina System")]
    public float maxStamina = 20;
    private float currentStamina;
    public float staminaLoss = 5;
    public float staminaRegenRate = 5;
    public bool currentlyRunning = false;
    private bool exhausted = false;

    //Gravity
    private float gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private Vector3 verticalVelocity;

    public float maxHealth = 100;
    public float currentHealth;
    AudioManager SFX;

    //Health indicator
    public Image healthIndicator;

    //for switching speed
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SFX = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        characterController = GetComponent<CharacterController>();

        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        UpdateStaminaBar();

        currentHealth = maxHealth;

        SFX.AmbienceSource.Play();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && exhausted == false)
        {
            if(currentStamina > 0)
            {
                currentSpeed = runSpeed;
                currentlyRunning = true;
                currentStamina -= staminaLoss * Time.deltaTime;
                Debug.Log("running");
                UpdateStaminaBar();
            }
            else
            {
                currentStamina = 0;
                exhausted = true;
            }
        }

        else
        {
            currentSpeed = walkSpeed;
            currentlyRunning = false;

            if(currentStamina < maxStamina)
            {
                currentStamina += staminaRegenRate * Time.deltaTime;
                Debug.Log("regen");
                UpdateStaminaBar();
            }
            else
            {
                currentStamina = maxStamina;
                exhausted = false;
            }
        }
        // open pause menu
        if (Input.GetKey(KeyCode.Escape)) {

            uiManager.ShowMenu(1);
        }

        // E to open Inventory
        if (Input.GetKey(KeyCode.E))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (UIManager.isMenuOpen && UIManager.currentMenu == 0)
                {
                    uiManager.HideMenu(0);
                }
                else if (!UIManager.isMenuOpen)
                {
                    InventoryManager.Instance.ListItems();
                    uiManager.ShowMenu(0);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            memoryManager.ClosePopup();
        }


        MovePlayer();

        ApplyGravity();

        UpdateHealthIndicator();
    }

    void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * x + transform.forward * y;

        characterController.Move(moveDirection * currentSpeed * Time.deltaTime);

        
    }

    private void ApplyGravity()
    {
        if (characterController.isGrounded)
        {
            if (verticalVelocity.y < 0)
                verticalVelocity.y = -2f;
        }
        else
        {
            verticalVelocity.y += gravity * gravityMultiplier * Time.deltaTime;
        }

        characterController.Move(verticalVelocity * Time.deltaTime);

    }

    void UpdateStaminaBar()
    {
        staminaBar.value = currentStamina;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Memory1"))
            memoryManager.CollectMemory(memory1);

        else if (other.CompareTag("Memory2"))
            memoryManager.CollectMemory(memory2);

        else if (other.CompareTag("Memory3"))
            memoryManager.CollectMemory(memory3);

        else if (other.CompareTag("Memory4"))
            memoryManager.CollectMemory(memory4);

        else if (other.CompareTag("Memory5"))
            memoryManager.CollectMemory(memory5);


       
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        uiManager.ShowDeathScreen();
        
    }

    void UpdateHealthIndicator()
    {
        float transparerncy = 1f - (currentHealth / 100f);
        Color color = Color.white;
        color.a = transparerncy;
        healthIndicator.color = color;  
    }

}
