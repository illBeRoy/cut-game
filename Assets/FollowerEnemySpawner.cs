using System.Collections;
using UnityEngine;

public class FollowerEnemySpawner : MonoBehaviour
{
    public Follower enemy;
    public float spawnInterval = 4;
    void Start()
    {
        StartCoroutine("SpawnEnemySometimes");
        
    }

    IEnumerator SpawnEnemySometimes()
    {
        for(; ; )
        {
            var enemyClone = Instantiate<Follower >(enemy, transform.position, transform.rotation);

            enemyClone.speed= Random.Range(1f, 5f);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
