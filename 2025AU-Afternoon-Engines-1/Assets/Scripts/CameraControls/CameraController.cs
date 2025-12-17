using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player;
    public float mouseSensitivity = 5.0f;
    float verticalRotation = 0f;

    // Make sure to assign this in the Inspector or find it in Start
    public UIManager UIManager;

    [Header("Head Bob")]
    public float stepHeight = 0.05f;
    public float stepFrequency = 10f;
    public float sprintMultiplier = 1.5f;
    private Vector3 startLocalPos;
    private float bobTimer = 0f;

    [Header("Footsteps")]
    public AudioClip footstepClip;
    public float sprintPitch = 1.3f;
    private bool footstepsPlaying = false;
    private AudioSource footstepSource;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        startLocalPos = transform.localPosition;

        footstepSource = GetComponent<AudioSource>();
        if (footstepSource == null)
        {
            footstepSource = gameObject.AddComponent<AudioSource>();
        }

        footstepSource.clip = footstepClip;
        footstepSource.loop = true;
        footstepSource.playOnAwake = false;
        footstepSource.pitch = 1f;

        // Try to find UIManager if not assigned
        if (UIManager == null)
        {
            UIManager = FindObjectOfType<UIManager>();
        }
    }

    void Update()
    {
        // Disable mouse look if a menu is open
        // Add null check to prevent errors
        if (UIManager == null || (!UIManager.isMenuOpen || MemoryManager.popupClosed))
        {
            MouseLook();
        }

        HandleHeadBob();
    }

    public void MouseLook()
    {
        float x = Input.GetAxis("Mouse X") * mouseSensitivity;
        float y = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // vertical rotation
        verticalRotation -= y;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * verticalRotation;

        //horizontal rotation
        Player.Rotate(Vector3.up * x);
    }

    void HandleHeadBob()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        bool isMoving = (x != 0 || z != 0);

        PlayerController playerController = GetComponentInParent<PlayerController>();
        bool isRunning = playerController != null && playerController.currentlyRunning;

        if ((UIManager != null && UIManager.isMenuOpen) || !MemoryManager.popupClosed || !isMoving)
        {
            StopFootsteps();
            ResetHeadBob();
            return;
        }

        bobTimer += Time.deltaTime * (isRunning ? sprintMultiplier : 1f) * stepFrequency;
        float yOffset = stepHeight * Mathf.PingPong(bobTimer, 1f) * 2f;
        transform.localPosition = startLocalPos + new Vector3(0, yOffset, 0);

        if (!footstepsPlaying && footstepClip != null)
        {
            footstepSource.pitch = isRunning ? sprintPitch : 1f;
            footstepSource.Play();
            footstepsPlaying = true;
        }
        else if (footstepsPlaying)
        {
            footstepSource.pitch = isRunning ? sprintPitch : 1f;
        }
    }

    public void StopFootsteps()
    {
        if (footstepSource != null && footstepSource.isPlaying)
        {
            footstepSource.Stop();
            footstepsPlaying = false;
        }
    }

    void ResetHeadBob()
    {
        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            startLocalPos,
            Time.deltaTime * 8f
        );
        bobTimer = 0f;
    }
}