﻿using System.Collections;
using UnityEngine;

public class FollowerEnemySpawner : MonoBehaviour
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
            var gameObjectClone = Instantiate(enemy, transform.position, transform.rotation);
            var enemyClone = gameObjectClone.GetComponent<Follower>();

            enemyClone.speed= Random.Range(.5f, 4f);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
