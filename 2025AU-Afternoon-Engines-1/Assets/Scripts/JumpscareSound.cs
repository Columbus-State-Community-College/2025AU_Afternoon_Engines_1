using UnityEngine;

public class JumpscareSound : MonoBehaviour
{
    AudioManager SFX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private void Start()
    {
        SFX = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SFX.PlayJumpScare(SFX.clockSFX);
            Destroy(gameObject);
        }
    }
}
