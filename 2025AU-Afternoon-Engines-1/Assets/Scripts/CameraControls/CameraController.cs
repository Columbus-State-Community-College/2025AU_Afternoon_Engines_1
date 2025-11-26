using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player;
    public float mouseSensitivity = 5.0f;
    float verticalRotation = 0f; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() 
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
}
