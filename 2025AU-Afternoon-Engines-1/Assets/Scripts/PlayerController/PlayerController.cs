using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    CharacterController characterController;

    public UIManager uiManager;

    [Header("Speeds")]
    public float walkSpeed = 5.0f;
    public float runSpeed = 15.0f;
    private float currentSpeed;

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
            }
            else
            {
                currentStamina = maxStamina;
                exhausted = false;
            }
        }

        if (Input.GetKey(KeyCode.Escape)) {

            Application.Quit();
        }

        // E to open Inventory
        if (Input.GetKey(KeyCode.E))
        {
            InventoryManager.Instance.ListItems();
            uiManager.ShowInventoryMenu();
        }


        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * x + transform.forward * y;

        characterController.Move(moveDirection * currentSpeed * Time.deltaTime);
    }
}
