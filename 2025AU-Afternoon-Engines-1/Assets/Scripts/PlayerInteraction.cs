using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // Info for raycast
    private Camera cam;
    [SerializeField]
    private float distance = 5f;
    [SerializeField]
    private LayerMask mask;

    void Start()
    {
        cam = Camera.main;

    }

    void Update()
    {
        // Casts ray from center of screen outward, can interact with object if ray hits
        // object on interact layer

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        // Debug for testing ray
        Debug.DrawRay(ray.origin, ray.direction * distance);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance, mask))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                // Put message reference (from interactable) here

                // Left click to pick up item
                if (Input.GetMouseButtonDown(0))
                {
                    interactable.BaseInteract();
                }

            }
        }

    }
}
