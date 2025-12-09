using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    AudioManager audioManager;

    public bool isOpen = false;
    public float openAngle = 90f;
    public float openSpeed = 2f;

    private Quaternion closedRot;
    private Quaternion openRot;


    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void Start()
    {
        closedRot = transform.localRotation;
        openRot = Quaternion.Euler(0, openAngle, 0) * closedRot;
    }
    public void ToggleDoor()
    {
        isOpen = !isOpen;
        StopAllCoroutines();
        StartCoroutine(AnimateDoor());
        audioManager.PlaySFX(audioManager.doorOpen);
    }

    IEnumerator AnimateDoor()
    {
        Quaternion target = isOpen ? openRot : closedRot;
        Quaternion start = transform.localRotation;

        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime * openSpeed;
            transform.localRotation = Quaternion.Slerp(start, target, t);
            yield return null;
        }
    }
}
