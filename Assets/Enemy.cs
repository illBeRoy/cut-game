using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject explosion;

    internal void TakeProjectileHit()
    {
        GameObject.Find("Score").GetComponent<Score>().addScore(100);
        Explode();
    }

    public void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}

