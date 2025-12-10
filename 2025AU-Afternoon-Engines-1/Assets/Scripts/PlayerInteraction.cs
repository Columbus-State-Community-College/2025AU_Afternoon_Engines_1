using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // Info for raycast
    private Camera cam;
    [SerializeField]
    private float distance = 5f;
    [SerializeField]
    private LayerMask mask;

    public InteractableUI interactableUI;

    private Interactable currentInteractable;

    AudioManager audioManager;

    void Start()
    {
        cam = Camera.main;
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

    }

    void Update()
    {

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        Interactable interactable = null;

        if (Physics.Raycast(ray, out hit, distance, mask))
        {
            interactable = hit.collider.GetComponent<Interactable>();

        }
            
        if (interactable != null)
        {
            if (currentInteractable != interactable)
            {
                currentInteractable?.OnLookExit(interactableUI);
                currentInteractable = interactable;
            }

            currentInteractable.OnLookEnter(interactableUI);

            if (Input.GetMouseButtonDown(0))
            {
                currentInteractable.OnLookExit(interactableUI);
                interactable.BaseInteract();
                audioManager.PlaySFX(audioManager.itemPickup);

            }

        } else
        {
            if (currentInteractable != null)
            {   
                currentInteractable.OnLookExit(interactableUI);
                currentInteractable = null;
            }
        }


        

    }
}
