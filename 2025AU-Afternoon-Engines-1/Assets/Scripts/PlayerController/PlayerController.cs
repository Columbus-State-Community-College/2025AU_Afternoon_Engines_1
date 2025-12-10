using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;

    [Header("Speeds")]
    public float walkSpeed = 5.0f;
    public float runSpeed = 15.0f;
    private float currentSpeed;

    [Header("Stamina System")]
    public float maxStamina = 20;
    private float currentStamina;
    public float staminaLoss = 5;
    public float staminaRegenRate = 5;
    private bool exhausted = false;

    [Header("Cursor Settings")]
    private bool cursorUnlocked = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        currentStamina = maxStamina;

        // Lock cursor at start
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Toggle cursor with C key
        if (Input.GetKeyDown(KeyCode.C))
        {
            cursorUnlocked = !cursorUnlocked;

            if (cursorUnlocked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        // Stamina and running
        if (Input.GetKey(KeyCode.LeftShift) && exhausted == false)
        {
            if (currentStamina > 0)
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
            if (currentStamina < maxStamina)
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

        // Movement
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.right * x + transform.forward * y;
        characterController.Move(moveDirection * currentSpeed * Time.deltaTime);
    }
}