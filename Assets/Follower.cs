using System;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public float speed = 4;
    public float triggerDistance = 1;

    private Transform target;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 localScale = this.transform.localScale;
        if (this.target.position.x < this.transform.position.x) {
            localScale.x = -Mathf.Abs(localScale.x);
        } else {
            localScale.x = Mathf.Abs(localScale.x);
        }
        this.transform.localScale = localScale;

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        if(Vector2.Distance(target.transform.position, transform.position) < triggerDistance)
        {
            var enemy = GetComponent<Enemy>();
            enemy.Explode();
        }
    }
}
