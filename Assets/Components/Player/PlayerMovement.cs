using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public bool movement = true;

    private EntityStateBehavior movementState;

    [SerializeField]
    private PlayerProperties properties;

    private Rigidbody2D rigidbody;
    public GameObject playerFlip;
    public bool facingLeft = false;

    public float speed = 1.5f;
    public float jumpPower = 250f;
    public float drag = 0.3f;

    public bool CanMove = true;
    public bool CanJump = true;

    public UnityEvent jumpEvent = new UnityEvent();

    [SerializeField]
    private bool JumpCooldown = false;

    [SerializeField, ReadOnly]
    private bool _IsJumping = false; 
    public bool IsJumping
    { get { return _IsJumping; } private set { _IsJumping = value; } }

    [SerializeField, ReadOnly]
    private bool _IsMovingHorizontal;
    public bool IsMovingHorizontal
    {
        get { return _IsMovingHorizontal; }
        private set { this._IsMovingHorizontal = value; }
    }

    private void Start()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
        this.movementState = GetComponent<EntityStateBehavior>();

        this.speed = properties.speed;
        this.jumpPower = properties.jumpPower;
        this.drag = properties.drag;

        this.movementState.landed.AddListener(() =>
        {
            this.CanJump = true;
            this.IsJumping = false;

            this.rigidbody.velocity = Vector2.zero;
            StartCoroutine(InitJumpCooldown());
        });

    }

    public void Move(Vector2 input)
    {
        if (input.x == 0)
            // If no input, resets X velocity to avoid sliding
            this.rigidbody.velocity = new Vector2(0, this.rigidbody.velocity.y);
        else
        {
            float computedSpeed = this.speed;
            // Resimulates physics when using MovePosition
            if (this.movementState.state == MovementState.FreeFall)
            {
                this.rigidbody.velocity = new Vector2(this.rigidbody.velocity.x, (this.rigidbody.velocity.y + ((Physics2D.gravity.y * (Time.fixedDeltaTime)) * this.rigidbody.gravityScale)));
                computedSpeed = this.speed * .5f;
            }

            // MovePosition translates the position of the GameObject with respect of collision.
            this.rigidbody.MovePosition(this.rigidbody.position + (this.rigidbody.velocity * Time.fixedDeltaTime) + new Vector2(input.x * (computedSpeed * this.drag), 0));
        }



    }

    // Jump Logic
    public void Jump()
    {
        if (this.JumpCooldown) return;
        if (this.IsJumping) return;
        if (!this.CanJump) return;
        this.IsJumping = true;
        this.CanJump = false;

        this.JumpCooldown = true;
        this.rigidbody.AddForce(new Vector2(0, this.jumpPower), ForceMode2D.Impulse);
        this.jumpEvent.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            this.Jump();
        }

        if (Input.GetKeyDown(KeyCode.A) && !facingLeft)
        {
            playerFlip.transform.Rotate(0, -180, 0);
            facingLeft = true;
        }

        if (Input.GetKeyDown(KeyCode.D) && facingLeft)
        {
            facingLeft = false;
            playerFlip.transform.Rotate(0, 180, 0);
        }
    }

    private void FixedUpdate()
    {
        // GetAxisRaw does not have smoothing in input.
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");

        this.IsMovingHorizontal = hInput != 0;

        Move(new Vector2(hInput, vInput));

    }

    // Jump Cooldown logic
    private IEnumerator InitJumpCooldown()
    {
        this.JumpCooldown = true;
        yield return new WaitForSeconds(properties.jumpCooldown);
        this.JumpCooldown = false;
    }
}
