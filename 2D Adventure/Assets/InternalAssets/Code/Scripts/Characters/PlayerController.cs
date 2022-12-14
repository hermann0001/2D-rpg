using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;

    Vector2 movementInput;
    public Rigidbody2D rb;
    public ContactFilter2D movementFilter;
    public Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
 
    void Update()
    {
        float horizontal = movementInput.x;
        float vertical = movementInput.y;
        animator.SetFloat("Horizontal", movementInput.x);
        animator.SetFloat("Vertical", movementInput.y);
        animator.SetFloat("Speed", movementInput.sqrMagnitude);

        if (horizontal != 0 || vertical != 0)
        {
            animator.SetFloat("lastMoveX", horizontal);
            animator.SetFloat("lastMoveY", vertical);
        }
    }

    void FixedUpdate()
    {
        //if movement input is != 0 try to move
        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);

            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));

                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }
        }
    }

    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(
        direction,
        movementFilter,
        castCollisions,
        moveSpeed * Time.fixedDeltaTime + collisionOffset);

        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}
