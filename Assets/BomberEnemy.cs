using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberEnemy : MonoBehaviour
{
    public float speed = 4;
    public float bombDroppingInterval = 1;

    public GameObject explosion;
    public GameObject bomb;

    private Transform target;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;

        StartCoroutine("DropBombSometimes");
    }

    void Update()
    {
        Fly();

    }
    
    IEnumerator DropBombSometimes()
    {
        for(; ; )
        {
            yield return new WaitForSeconds(bombDroppingInterval);
            DropBomb();
        }
    }

    private void DropBomb()
    {
        
        var bombClone = Instantiate(bomb, new Vector2(transform.position.x, transform.position.y - 0.8f), transform.rotation);
        bombClone.transform.Rotate(0, 0, 180);
    }

    private void Fly()
    {
        float hieght = Mathf.PingPong(Time.timeSinceLevelLoad, 2f) - 1f;
        var destination = new Vector2(target.position.x, transform.position.y + (hieght * 3));
        transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }
}
