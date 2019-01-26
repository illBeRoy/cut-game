using System.Collections;
using UnityEngine;

public class BomberEnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnInterval = 4;
    public float waitOffset = 0;

    void Start()
    {
        StartCoroutine("SpawnEnemySometimes");
        
    }

    IEnumerator SpawnEnemySometimes()
    {
        yield return new WaitForSeconds(waitOffset);
        for(; ; )
        {
            var gameObjectClone  = Instantiate(enemy, transform.position, transform.rotation);
            var enemyClone = gameObjectClone.GetComponent<BomberEnemy>();

            enemyClone.bombDroppingInterval = Random.Range(1.5f, 5);
            enemyClone.speed = Random.Range(1.5f, 5);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

}
