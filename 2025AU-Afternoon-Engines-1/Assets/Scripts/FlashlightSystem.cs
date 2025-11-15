using TMPro;
using UnityEngine;

public class FlashlightSystem : MonoBehaviour
{
    public static bool isFlashlightWorking;
    private bool FlashlightRunning;
    public float FlashlightTotalPwr = 100f;
    public float FlashlightDrainRate = 10f;
    private float FlashlightPwr;
    public Vector3 normalScale = new Vector3(1f, 1f, 1f);
    public Vector3  hiddenScale = new Vector3(0f, 0f, 0f);
    private bool isShowing = false;

    public float flashlightIntensity = 1000f; //change when lighting is set
    public float minIntensity = 50f;

    public Light flashlight;
    public GameObject coneCollider;
    public TextMeshProUGUI flashlightPWRtext;
    AudioManager SFX;


    private void Awake()
    {
        SFX = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();  
    }
    void Start()
    {
        flashlight = GetComponent<Light>();
        FlashlightPwr = FlashlightTotalPwr;
        flashlight.enabled = false;
        FlashlightRunning = false;
        isFlashlightWorking = true;
        coneCollider.gameObject.transform.localScale = hiddenScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isFlashlightWorking)
        {
            SFX.PlaySFX(SFX.flashlightSFX);
          if(isShowing)
            {
                coneCollider.gameObject.transform.localScale = hiddenScale;
            }

          else
            {
                coneCollider.gameObject.transform.localScale = normalScale;
            }

            isShowing = !isShowing;
            flashlight.enabled = !flashlight.enabled;
            
           FlashlightRunning = flashlight.enabled;


        
        
        
        }

        if (FlashlightRunning && flashlight.intensity > minIntensity)
        {
            flashlight.intensity -= FlashlightDrainRate * Time.deltaTime;
            Debug.Log("draining power");
            UpdateFlashlightUI();
        }

        if (flashlight.intensity <= minIntensity)
        {
            isFlashlightWorking = false;
            flashlight.enabled = false;
            FlashlightRunning = false;
            Debug.Log("out of power");
        }

           
 
    }


    private void UpdateFlashlightUI()
    {
        flashlightPWRtext.text = "Power: " + Mathf.FloorToInt(flashlight.intensity).ToString() + "%";
    }


}

