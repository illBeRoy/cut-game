using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerEnemySpawner : MonoBehaviour
{
    public FollowerEnemy enemy;
    public float spawnInterval = 4;
    void Start()
    {
        StartCoroutine("SpawnEnemySometimes");
        
    }

    IEnumerator SpawnEnemySometimes()
    {
        for(; ; )
        {
            var enemyClone = Instantiate<FollowerEnemy >(enemy, transform.position, transform.rotation);

            enemyClone.speed= Random.Range(1f, 5f);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
