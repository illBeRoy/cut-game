using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PhysicsObject))]
[RequireComponent(typeof(Shooter))]
[RequireComponent(typeof(SpriteAnimator))]
public class PlayerController : MonoBehaviour
{
    [Header("Health Components")]
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public AudioClip deathSound;

    [Header("Gun Wiring")]
    public Transform shootingPointFront;
    public Transform shootingPointUp;

    private PhysicsObject physicsObject;
    private SpriteAnimator spriteAnimator;
    private Shooter shooter;
    private bool isClimbing = false;
    private bool isLookingUp = false;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        this.physicsObject = this.GetComponent<PhysicsObject>();
        this.shooter = this.GetComponent<Shooter>();
        this.spriteAnimator = this.GetComponent<SpriteAnimator>();

        currentHealth = startingHealth;
    }

    internal void TakeHit(float damage)
    {
        if (this.isDead) {
            return;
        }

        currentHealth -= (int)(damage * 40f);
        healthSlider.value = currentHealth;
        if (currentHealth <= 0) {
            this.Die();
        }
    }

    void Die()
    {
        this.isDead = true;
        this.spriteAnimator.SetSpriteSheet("Dead");
        this.GetComponent<AudioSource>().PlayOneShot(this.deathSound);
        this.physicsObject.disableRigidBody();
        this.physicsObject.enabled = false;
        GameObject.FindObjectOfType<GameManager>().EndGame();
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
        if (!this.isDead) {
            this.HandleControls();
        }
    }
    
    void LateUpdate()
    {
        if (!this.isDead) {
            this.HandleControls();
            this.UpdateAnimations();
        }
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

        if (Input.GetAxisRaw("Vertical") > 0) {
            this.isLookingUp = true;
            this.shooter.SetShootingPoint(this.shootingPointUp);
        } else {
            this.isLookingUp = false;
            this.shooter.SetShootingPoint(this.shootingPointFront);
        }

        if (Input.GetButton("Fire1") && !this.isClimbing) {
            if (this.isLookingUp) {
                this.shooter.Shoot(Vector2.up);
            } else {
                this.shooter.Shoot(this.physicsObject.GetFacingDirection());
            }
        }
    }

    void UpdateAnimations()
    {
        if (this.physicsObject.IsWalking()) {
            this.spriteAnimator.SetSpriteSheet(GetActualAnimationName("Walk"));
        } else if (!this.physicsObject.IsOnGround()) {
            this.spriteAnimator.SetSpriteSheet(GetActualAnimationName("Jump"));
        } else {
            this.spriteAnimator.SetSpriteSheet(GetActualAnimationName("Idle"));
        }
    }

    private string GetActualAnimationName(string animation)
    {
        string directionString = "Front";
        if (this.isLookingUp) {
            directionString = "Up";
        }

        return String.Format("{0} {1}", animation, directionString);
    }
}
