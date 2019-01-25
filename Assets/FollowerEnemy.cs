using UnityEngine;

public class FollowerEnemy : MonoBehaviour
{
    public float speed = 4;
    public float triggerDistance = 1;
    public GameObject explosion;

    private Transform target;
    

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        if(Vector2.Distance(target.transform.position, transform.position) < triggerDistance)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
