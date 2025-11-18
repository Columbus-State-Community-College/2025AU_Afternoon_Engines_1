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
    

    [Header("Speeds")]
    public float walkSpeed = 5.0f;
    public float runSpeed = 15.0f;
    private float currentSpeed;

    [Header("Memory Text")]
    public TextMeshProUGUI memory1;
    public TextMeshProUGUI memory2;
    public TextMeshProUGUI memory3;
    public TextMeshProUGUI memory4;
    public TextMeshProUGUI memory5;

    [Header("Stamina System")]
    public float maxStamina = 20;
    private float currentStamina;
    public float staminaLoss = 5;
    public float staminaRegenRate = 5;
    // private bool currentlyRunning = false;
    private bool exhausted = false;
     //for switching speed
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        UpdateStaminaBar();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && exhausted == false)
        {
            if(currentStamina > 0)
            {
                currentSpeed = runSpeed;
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
            InventoryManager.Instance.ListItems();
            uiManager.ShowMenu(0);
        }


        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * x + transform.forward * y;

        characterController.Move(moveDirection * currentSpeed * Time.deltaTime);
    }

    void UpdateStaminaBar()
    {
        staminaBar.value = currentStamina;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Memory1"))
        {
           memory1.gameObject.SetActive(true);
           memory2.gameObject.SetActive(false);
            memory3.gameObject.SetActive(false);
            memory4.gameObject.SetActive(false);
            memory5.gameObject.SetActive(false);
        }
        if (other.CompareTag("Memory2"))
        {
            memory2.gameObject.SetActive(true);
            memory3.gameObject.SetActive(false);
            memory4.gameObject.SetActive(false);
            memory5.gameObject.SetActive(false);
            memory1.gameObject.SetActive(false);
        }

        if (other.CompareTag("Memory3"))
        {
            memory2.gameObject.SetActive(false);
            memory3.gameObject.SetActive(true);
            memory4.gameObject.SetActive(false);
            memory5.gameObject.SetActive(false);
            memory1.gameObject.SetActive(false);
        }

        if (other.CompareTag("Memory4"))
        {
            memory2.gameObject.SetActive(false);
            memory3.gameObject.SetActive(false);
            memory4.gameObject.SetActive(true);
            memory5.gameObject.SetActive(false);
            memory1.gameObject.SetActive(false);
        }

        if (other.CompareTag("Memory5"))
        {
            memory2.gameObject.SetActive(false);
            memory3.gameObject.SetActive(false);
            memory4.gameObject.SetActive(false);
            memory5.gameObject.SetActive(true);
            memory1.gameObject.SetActive(false);
        }
    }
}
