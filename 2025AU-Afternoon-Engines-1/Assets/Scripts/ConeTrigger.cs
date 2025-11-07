using UnityEngine;

public class ConeTrigger : MonoBehaviour
{
    public MeshCollider coneCollider;
    public Transform Camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coneCollider = GetComponent<MeshCollider>();
        coneCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.transform.position;
        if (Input.GetKeyDown(KeyCode.F) && FlashlightSystem.isFlashlightWorking) 
        { 
            coneCollider.enabled = !coneCollider.enabled;
        }
    }
}
