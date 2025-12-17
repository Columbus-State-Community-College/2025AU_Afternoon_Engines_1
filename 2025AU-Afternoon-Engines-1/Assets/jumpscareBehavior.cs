using UnityEngine;
using System.Collections;

public class JumpscareEnemy : MonoBehaviour
{
    [Header("Jumpscare Settings")]
    [SerializeField] private AudioClip jumpscareSound;
    [SerializeField] private float jumpscareDuration = 1.5f;
    [SerializeField] private float scareScaleMultiplier = 2f; // Enemy grows when jumpscaring

    [Header("Spawn Settings")]
    [SerializeField] private float minSpawnDelay = 10f;
    [SerializeField] private float maxSpawnDelay = 30f;
    [SerializeField] private float spawnDistance = 5f; // Distance in front of player
    [SerializeField] private float despawnTime = 3f;

    [Header("Enemy Settings")]
    [SerializeField] private GameObject enemyPrefab; // Your 3D enemy model
    [SerializeField] private float lungeSpeed = 10f;
    [SerializeField] private float triggerDistance = 1.5f;

    [Header("Camera Shake")]
    [SerializeField] private bool enableCameraShake = true;
    [SerializeField] private float shakeIntensity = 0.3f;

    private Transform player;
    private Camera playerCamera;
    private AudioSource audioSource;
    private GameObject currentEnemy;
    private bool isActive = false;
    private bool jumpscareTriggered = false;
    private Vector3 originalScale;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerCamera = Camera.main;
        audioSource = gameObject.AddComponent<AudioSource>();

        if (enemyPrefab != null)
        {
            originalScale = enemyPrefab.transform.localScale;
        }

        StartCoroutine(SpawnRoutine());
    }

    void Update()
    {
        if (isActive && !jumpscareTriggered && currentEnemy != null)
        {
            // Lunge towards player
            Vector3 direction = (player.position - currentEnemy.transform.position).normalized;
            currentEnemy.transform.position += direction * lungeSpeed * Time.deltaTime;

            // Always face the player
            currentEnemy.transform.LookAt(player);

            // Check distance for jumpscare
            float distance = Vector3.Distance(currentEnemy.transform.position, player.position);
            if (distance <= triggerDistance)
            {
                TriggerJumpscare();
            }
        }
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(delay);

            if (!isActive)
            {
                SpawnEnemy();
            }
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null || playerCamera == null) return;

        // Spawn directly where player is looking
        Vector3 spawnPosition = playerCamera.transform.position + playerCamera.transform.forward * spawnDistance;

        // Optional: Adjust height to be at player eye level or slightly above
        spawnPosition.y = player.position.y + 1f;

        // Instantiate the enemy
        currentEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        currentEnemy.transform.LookAt(player);

        isActive = true;
        jumpscareTriggered = false;

        StartCoroutine(DespawnTimer());
    }

    IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(despawnTime);

        if (!jumpscareTriggered)
        {
            DespawnEnemy();
        }
    }

    void TriggerJumpscare()
    {
        if (currentEnemy == null) return;

        jumpscareTriggered = true;

        // Make enemy larger and right in player's face
        currentEnemy.transform.position = playerCamera.transform.position + playerCamera.transform.forward * 0.5f;
        currentEnemy.transform.localScale = originalScale * scareScaleMultiplier;
        currentEnemy.transform.LookAt(playerCamera.transform);

        // Play sound
        if (jumpscareSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(jumpscareSound);
        }

        // Camera shake
        if (enableCameraShake)
        {
            StartCoroutine(CameraShake());
        }

        StartCoroutine(EndJumpscare());
    }

    IEnumerator CameraShake()
    {
        Vector3 originalPos = playerCamera.transform.localPosition;
        float elapsed = 0f;

        while (elapsed < jumpscareDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeIntensity;
            float y = Random.Range(-1f, 1f) * shakeIntensity;

            playerCamera.transform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        playerCamera.transform.localPosition = originalPos;
    }

    IEnumerator EndJumpscare()
    {
        yield return new WaitForSeconds(jumpscareDuration);

        DespawnEnemy();
    }

    void DespawnEnemy()
    {
        if (currentEnemy != null)
        {
            Destroy(currentEnemy);
        }

        isActive = false;
    }
}