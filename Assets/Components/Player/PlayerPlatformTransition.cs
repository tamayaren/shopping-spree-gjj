using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformTransition : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private BoxCollider2D collider;
    private Rigidbody2D rigidbody;
    private EntityStateBehavior movementState;
    
    private void Start()
    {
        this.playerMovement = GetComponent<PlayerMovement>();
        this.collider = GetComponent<BoxCollider2D>();
        this.movementState = GetComponent<EntityStateBehavior>();
        this.rigidbody = GetComponent<Rigidbody2D>();

        this.playerMovement.jumpEvent.AddListener(() =>
        {
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.up, 32, LayerMask.GetMask("Platform"));
            Debug.DrawRay(this.transform.position, Vector2.up * (hit.distance), Color.red);

            if (!ReferenceEquals(hit.collider, null))
            {
                Physics2D.IgnoreCollision(collider, hit.collider);
                StartCoroutine(SetCollidePlatform(hit.collider.gameObject));
            }
        });
    }

    private IEnumerator SetCollidePlatform(GameObject platform)
    {
        BoxCollider2D platformCollider = platform.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(collider, platformCollider);
        
        while (this.transform.position.y < (platform.transform.position.y))
        {
            Physics2D.IgnoreCollision(collider, platformCollider);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        Physics2D.IgnoreCollision(collider, platformCollider, false);
    }

    private IEnumerator AutoResetCollider(BoxCollider2D platformCollider)
    {
        this.rigidbody.AddForce(new Vector2(0, -60), ForceMode2D.Impulse);
        Physics2D.IgnoreCollision(collider, platformCollider);
        yield return new WaitForSeconds(.5f);

        Physics2D.IgnoreCollision(collider, platformCollider, false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, 5, LayerMask.GetMask("Platform"));

            if (hit.collider.gameObject && hit.collider.gameObject.tag != "Ground")
            {
                StartCoroutine(AutoResetCollider(hit.collider.gameObject.GetComponent<BoxCollider2D>()));
            }
        }
    }
}
