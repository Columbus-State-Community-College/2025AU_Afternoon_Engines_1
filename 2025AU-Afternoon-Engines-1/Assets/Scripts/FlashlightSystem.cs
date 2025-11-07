using UnityEngine;

public class FlashlightSystem : MonoBehaviour
{
    public static bool isFlashlightWorking;
    private bool FlashlightRunning;
    public float FlashlightTotalPwr = 100f;
    public float FlashlightDrainRate = 10f;
    private float FlashlightPwr;

    public float flashlightIntensity = 1000f; //change when lighting is set
    public float minIntensity = 50f;

    public Light flashlight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flashlight = GetComponent<Light>();
        FlashlightPwr = FlashlightTotalPwr;
        flashlight.enabled = false;
        FlashlightRunning = false;
        isFlashlightWorking = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isFlashlightWorking)
        { 
            flashlight.enabled = !flashlight.enabled;
            FlashlightRunning = flashlight.enabled;


        
        
        
        }

        if (FlashlightRunning && flashlight.intensity > minIntensity)
        {
            flashlight.intensity -= FlashlightDrainRate * Time.deltaTime;
            Debug.Log("draining power");
        }

        if (flashlight.intensity <= minIntensity)
        {
            isFlashlightWorking = false;
            flashlight.enabled = false;
            FlashlightRunning = false;
            Debug.Log("out of power");
        }

           
 
    }


}

