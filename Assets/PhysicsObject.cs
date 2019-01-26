using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsObject : MonoBehaviour {
    [Header("Movement")]
    public float maxSpeed = 7;
    public float jumpForce = 7;
    public float climbSpeed = 3;
    public Vector2 initialFacingDirection = Vector2.right;
    public AudioClip jumpingSound;

    [Header("Physics")]
    public float minGroundNormalY = .65f;
    public float gravityModifier = 1f;

    private Vector2 targetVelocity;
    private bool grounded;
    private Vector2 groundNormal;
    private Rigidbody2D rb2d;
    private Vector2 velocity;
    private ContactFilter2D contactFilter;
    private RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    private List<RaycastHit2D> hitBufferList = new List<RaycastHit2D> (16);
    private const float minMoveDistance = 0.001f;
    private const float shellRadius = 0.01f;
    private Vector2 facingDirection;
    private float gravityScale = 0f;
    private AudioSource audioSource;

    public void MoveRight()
    {
        this.targetVelocity.x = this.maxSpeed;
        this.facingDirection = Vector2.right;
    }

    public void MoveLeft()
    {
        this.targetVelocity.x = -this.maxSpeed;
        this.facingDirection = Vector2.left;
    }

    public void MoveUp()
    {
        this.UpdateMovement(new Vector2(0, 0.08f), true);
    }

    public void MoveDown()
    {
        this.UpdateMovement(new Vector2(0, -0.08f), true);
    }

    public void Jump()
    {
        if (this.grounded) {
            if (this.audioSource != null && this.jumpingSound != null) {
                this.audioSource.PlayOneShot(this.jumpingSound);
            }
            this.velocity.y = this.jumpForce;
        }
    }

    public void disableRigidBody() {
        this.gravityModifier = 0f;
        this.rb2d.gravityScale = 0f;
    }

    public void enableRigidBody()
    {
        this.gravityModifier = 1f;
        this.rb2d.gravityScale = this.gravityScale;
    }

    public Vector2 GetFacingDirection() {
        return this.facingDirection;
    }

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D> ();
        this.audioSource = this.GetComponent<AudioSource>();
        this.gravityScale = this.rb2d.gravityScale;
    }


    void Start () 
    {
        this.facingDirection = this.initialFacingDirection;
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask (Physics2D.GetLayerCollisionMask (gameObject.layer));
        contactFilter.useLayerMask = true;
    }
    
    void Update () 
    {
        targetVelocity = Vector2.zero;
    }


    void LateUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;

        grounded = false;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2 (groundNormal.y, -groundNormal.x);

        Vector2 move = moveAlongGround * deltaPosition.x;

        UpdateMovement(move, false);

        move = Vector2.up * deltaPosition.y;

        UpdateMovement(move, true);
        UpdateFacingDirection();
    }

    private void UpdateMovement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > minMoveDistance) 
        {
            int count = rb2d.Cast (move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear ();
            for (int i = 0; i < count; i++) {
                hitBufferList.Add (hitBuffer [i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++) 
            {
                Vector2 currentNormal = hitBufferList [i].normal;
                if (currentNormal.y > minGroundNormalY) 
                {
                    grounded = true;
                    if (yMovement) 
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot (velocity, currentNormal);
                if (projection < 0) 
                {
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList [i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }


        }

        rb2d.position = rb2d.position + move.normalized * distance;
    }

    private void UpdateFacingDirection() {
        Vector3 localScale = this.gameObject.transform.localScale;

        if (this.facingDirection.x > 0) {
            localScale.x = Mathf.Abs(localScale.x);
        } else if (this.facingDirection.x < 0) {
            localScale.x = -Mathf.Abs(localScale.x);
        }

        this.gameObject.transform.localScale = localScale;
    }
}
