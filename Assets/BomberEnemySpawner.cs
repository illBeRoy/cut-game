using System.Collections;
using UnityEngine;

public class BomberEnemySpawner : MonoBehaviour
{
    public BomberEnemy enemy;
    public float spawnInterval = 4;

    void Start()
    {
        StartCoroutine("SpawnEnemySometimes");
        
    }

    IEnumerator SpawnEnemySometimes()
    {
        for(; ; )
        {
            var enemyClone = Instantiate<BomberEnemy>(enemy, transform.position, transform.rotation);

            enemyClone.bombDroppingInterval = Random.Range(1.5f, 5);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void Update()
    {
    }
}
