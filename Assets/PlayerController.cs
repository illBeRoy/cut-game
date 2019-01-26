using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PhysicsObject))]
[RequireComponent(typeof(Shooter))]
public class PlayerController : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;

    private PhysicsObject physicsObject;
    private Shooter shooter;
    private bool isClimbing = false;

    // Start is called before the first frame update
    void Start()
    {
        this.physicsObject = this.GetComponent<PhysicsObject>();
        this.shooter = this.GetComponent<Shooter>();

        currentHealth = startingHealth;
    }

    internal void TakeHit(float damage)
    {
        currentHealth -= (int)(damage * 20f);
        healthSlider.value = currentHealth;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.ToString() == "Climbable")
        {
            this.isClimbing = true;
            this.physicsObject.MoveUp();
            this.physicsObject.disableRigidBody();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag.ToString() == "Climbable")
        {
            this.isClimbing = false;
            this.physicsObject.enableRigidBody();
        }
    }

    void Update()
    {
        this.HandleControls();
    }
    
    void LateUpdate()
    {
        this.HandleControls();
    }

    void HandleControls()
    {
        if (this.isClimbing) {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                this.physicsObject.MoveUp();
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                this.physicsObject.MoveDown();
            }
        }
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
