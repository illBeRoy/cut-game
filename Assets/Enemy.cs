using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject explosion;

    internal void TakeProjectileHit()
    {
        var scoreObject = GameObject.Find("Score");
        var scoreComponent = scoreObject.GetComponent<Score>();
        scoreComponent.addScore(100);
        Explode();
    }

    public void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}

