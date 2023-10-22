using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

/*
 * EntityStateBehavior 
 * Determines movement state to check if entity is either on the ground, or freefalling.
 * Has a property called "Landed" which is an event that fires whenever the player transitions from FreeFall -> Grounded
 * 
 */

public class EntityStateBehavior : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private BoxCollider2D collider;

    public EntityMovementChanged movementChanged = new EntityMovementChanged();
    public UnityEvent landed = new UnityEvent();
    public UnityEvent freefall = new UnityEvent();

    [SerializeField, ReadOnly]
    private MovementState _state;
    public MovementState state
    {
        get { return this._state; }
        private set { 
            this._state = value;
            this.movementChanged.Invoke(value);
        }
    }

    private void Start()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
        this.collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        // Check distance from nearest ground
        RaycastHit2D grounded = Physics2D.Raycast(this.rigidbody.position, Vector2.down, (collider.bounds.extents.y + 1f), LayerMask.GetMask("Platform"));
        Debug.DrawRay(this.transform.position, Vector2.down * (grounded.distance), Color.red);

        MovementState lastState = this.state;

        this.state = (grounded.distance == 0 || grounded.distance > (collider.bounds.extents.y + .5f)) ? MovementState.FreeFall : MovementState.Grounded;

        // If transitioned, invokes landed event
        if (lastState == MovementState.FreeFall && this.state == MovementState.Grounded)
            this.landed.Invoke();
        else
            this.freefall.Invoke();
    }
}
