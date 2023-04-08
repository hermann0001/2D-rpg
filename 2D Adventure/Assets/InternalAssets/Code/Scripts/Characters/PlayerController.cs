using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.01f;

    
    private Vector2 movementInput;
    private Rigidbody2D rb;
    private ContactFilter2D movementFilter;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator controllerAnimator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public VectorValue startingPosition;
    public PlayerInputActions playerControls;
    private InputAction move;
    public static InputAction interact;

    private bool moveLeft;
    private bool moveRight;
    private bool moveUp;
    private bool moveDown;

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        interact = playerControls.Player.Interact;
        interact.Enable();
        interact.performed += Interact;
    }

    private void OnDisable()
    {
        move.Disable();
        interact.Disable();
    }

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        controllerAnimator = GameObject.FindGameObjectWithTag("TouchController").GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        moveLeft = false;
        moveRight = false;
        moveUp = false;
        moveDown = false;
        transform.position = startingPosition.playerInitialValue;
    }

    void Update()
    {
        //MovementPlayer();
        movementInput = move.ReadValue<Vector2>();

        float horizontal = movementInput.x;
        float vertical = movementInput.y;
        playerAnimator.SetFloat("Horizontal", movementInput.x);
        playerAnimator.SetFloat("Vertical", movementInput.y);
        playerAnimator.SetFloat("Speed", movementInput.sqrMagnitude);

        if (horizontal != 0 || vertical != 0)
        {
            playerAnimator.SetFloat("lastMoveX", horizontal);
            playerAnimator.SetFloat("lastMoveY", vertical);
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

    private void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("interact!");
    }

    //private void MovementPlayer()
    //{
    //    if (moveLeft)
    //    {
    //        movementInput = new Vector2(-1, 0);
    //    }
    //    else if (moveRight)
    //    {
    //        movementInput = new Vector2(1, 0);
    //    }
    //    else if (moveUp)
    //    {
    //        movementInput = new Vector2(0, 1);
    //    }
    //    else if (moveDown)
    //    {
    //        movementInput = new Vector2(0, -1);
    //    }
    //    else
    //    {
    //        movementInput = Vector2.zero;
    //    }
    //}

    //OnPointer<Down:Press; Up:Release><Direction>
    public void OnPointerDownLeft()
    {
        moveLeft = true;
        controllerAnimator.SetBool("left", moveLeft);
    }

    public void OnPointerUpLeft()
    {
        moveLeft = false;
        controllerAnimator.SetBool("left", moveLeft);

    }

    public void OnPointerDownRight()
    {
        moveRight = true;
        controllerAnimator.SetBool("right", moveRight);

    }

    public void OnPointerUpRight()
    {
        moveRight = false;
        controllerAnimator.SetBool("right", moveRight);

    }
    public void OnPointerUpDown()
    {
        moveDown = false;
        controllerAnimator.SetBool("down", moveDown);

    }
    public void OnPointerDownDown()
    {
        moveDown = true;
        controllerAnimator.SetBool("down", moveDown);

    }

    public void OnPointerDownUp()
    {
        moveUp = true;
        controllerAnimator.SetBool("up", moveUp);

    }

    public void OnPointerUpUp()
    {
        moveUp = false;
        controllerAnimator.SetBool("up", moveUp);
    }
}
