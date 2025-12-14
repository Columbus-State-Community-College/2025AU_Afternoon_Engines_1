using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    [SerializeField] GameObject monsterPrefab;
    [SerializeField ] GameObject secondSpawn;
    GameObject clone;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame

   


    public void SpawnMonster()
    {
   
         clone = Instantiate(monsterPrefab, transform.position, transform.rotation);
        
    }

    public void TeleportMonster()
    {

        Instantiate(monsterPrefab, secondSpawn.transform.position, secondSpawn.transform.rotation);
    }
}
