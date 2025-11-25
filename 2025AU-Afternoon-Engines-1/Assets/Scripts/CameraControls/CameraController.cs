using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player;
    public float mouseSensitivity = 5.0f;
    float verticalRotation = 0f; 
    UIManager UIManager;

    [Header("Head Bob")]
    public float stepHeight = 0.05f;
    public float stepFrequency = 10f;
    public float sprintMultiplier = 1.5f;

    private Vector3 startLocalPos;
    private float bobTimer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        startLocalPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update() 
    {
        // Disable mouse look if a menu is open
        if (UIManager.isMenuOpen == true)
        {
            return;
        } else {
            MouseLook();
            HandleHeadBob();
        }
            
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
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        if (isMoving)
        {
            bobTimer += Time.deltaTime * (isRunning ? sprintMultiplier : 1f) * stepFrequency;

            float yOffset = stepHeight * Mathf.PingPong(bobTimer, 1f) * 2f;
            transform.localPosition = startLocalPos + new Vector3(0, yOffset, 0);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, startLocalPos, Time.deltaTime * 5f);
            bobTimer = 0f;
        }
    }
}
