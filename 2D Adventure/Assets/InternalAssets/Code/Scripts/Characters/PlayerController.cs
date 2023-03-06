using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.01f;

    Vector2 movementInput;
    private Rigidbody2D rb;
    public ContactFilter2D movementFilter;
    private Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public SceneTransition startingPosition;


    private bool moveLeft;
    private bool moveRight;
    private bool moveUp;
    private bool moveDown;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        moveLeft = false;
        moveRight = false;
        moveUp = false;
        moveDown = false;
        transform.position = startingPosition.playerInitialValue;
    }

    void Update()
    {
        MovementPlayer();

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

    private void MovementPlayer()
    {
        if (moveLeft)
        {
            movementInput = new Vector2(-1, 0);
        }
        else if (moveRight)
        {
            movementInput = new Vector2(1, 0);
        }
        else if (moveUp)
        {
            movementInput = new Vector2(0, 1);
        }
        else if (moveDown)
        {
            movementInput = new Vector2(0, -1);
        }
        else
        {
            movementInput = Vector2.zero;
        }
    }

    //OnPointer<Down:Press; Up:Release><Direction>
    public void OnPointerDownLeft()
    {
        moveLeft = true;
    }

    public void OnPointerUpLeft()
    {
        moveLeft = false;
    }

    public void OnPointerDownRight()
    {
        moveRight = true;
    }

    public void OnPointerUpRight()
    {
        moveRight = false;
    }
    public void OnPointerUpDown()
    {
        moveDown = false;
    }
    public void OnPointerDownDown()
    {
        moveDown = true;
    }

    public void OnPointerDownUp()
    {
        moveUp = true;
    }

    public void OnPointerUpUp()
    {
        moveUp = false;
    }
}
