using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicsObject))]
[RequireComponent(typeof(Shooter))]
public class PlayerController : MonoBehaviour
{
    private PhysicsObject physicsObject;
    private Shooter shooter;

    // Start is called before the first frame update
    void Start()
    {
        this.physicsObject = this.GetComponent<PhysicsObject>();
        this.shooter = this.GetComponent<Shooter>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") > 0) {
            this.physicsObject.MoveRight();
        }

        if (Input.GetAxisRaw("Horizontal") < 0) {
            this.physicsObject.MoveLeft();
        }

        if (Input.GetButton("Jump")) {
            this.physicsObject.Jump();
        }

        if (Input.GetButton("Fire1")) {
            if (Input.GetAxisRaw("Vertical") > 0) {
                this.shooter.Shoot(Vector2.up);
            } else {
                this.shooter.Shoot(this.physicsObject.GetFacingDirection());
            }
        }
    }
}
