using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingMove : MonoBehaviour
{
    public float width;
    public float speed = 3;

    private float startingPoint;

    // Start is called before the first frame update
    void Start()
    {
        this.startingPoint = this.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = this.transform.position;
        if (position.x < this.startingPoint - this.width) {
            position.x += this.width;
        }
        position.x -= this.speed * Time.deltaTime;
        this.transform.position = position;
    }
}
