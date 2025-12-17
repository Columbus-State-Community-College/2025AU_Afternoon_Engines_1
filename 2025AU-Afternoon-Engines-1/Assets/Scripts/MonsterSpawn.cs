using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    [SerializeField] GameObject monsterPrefab;
    [SerializeField] bool despawnOnMemoryCollect = true;

    private GameObject spawnedMonster;
    private bool hasSpawned = false;


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (hasSpawned) return;


        SpawnMonster();
    }

    public void SpawnMonster()
    {

        spawnedMonster = Instantiate(monsterPrefab, transform.position, transform.rotation);
        hasSpawned = true;

    }

    public void DespawnEnemy()
    {
        if (spawnedMonster != null)
        {
            Destroy(spawnedMonster);
            spawnedMonster = null;
            hasSpawned = false;
        }
    }

}
