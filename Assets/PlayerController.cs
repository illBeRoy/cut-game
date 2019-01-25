using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicsObject))]
public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 7;
    public float jumpForce = 7;

    private PhysicsObject physicsObject;

    // Start is called before the first frame update
    void Start()
    {
        this.physicsObject = this.GetComponent<PhysicsObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0) {
            this.physicsObject.MoveRight(this.maxSpeed);
        }

        if (Input.GetAxis("Horizontal") < 0) {
            this.physicsObject.MoveLeft(this.maxSpeed);
        }

        if (Input.GetButton("Jump")) {
            this.physicsObject.Jump(this.jumpForce);
        }
    }
}
