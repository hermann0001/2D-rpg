using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;

    Vector2 movementInput;
    Rigidbody2D rb; 
    public ContactFilter2D movementFilter;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }

    private void FixedUpdate(){
        //if movement input is != 0 try to move
        if(movementInput != Vector2.zero){
            bool success = TryMove(movementInput);

            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));

                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }

            if (movementInput.y < 0 && movementInput.x == 0)
                animator.SetBool("isMovingDown", success);
            if (movementInput.y > 0 && movementInput.x == 0)
                animator.SetBool("isMovingUp", success);
            if (movementInput.x < 0 && movementInput.y == 0)
                animator.SetBool("isMovingLeft", success);
            if (movementInput.x > 0 && movementInput.y == 0)
                animator.SetBool("isMovingRight", success);
        }
        else
        {
            animator.SetBool("isMovingDown", false);
            animator.SetBool("isMovingUp", false);
            animator.SetBool("isMovingLeft", false);
            animator.SetBool("isMovingRight", false);
        }
    }

    private bool TryMove(Vector2 direction){
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

    void OnMove(InputValue movementValue){
        movementInput = movementValue.Get<Vector2>();
    }
}
