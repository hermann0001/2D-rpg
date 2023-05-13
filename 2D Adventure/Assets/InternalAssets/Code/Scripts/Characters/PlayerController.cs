using System.Collections;
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
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public VectorValue startingPosition;
    public PlayerInputActions playerControls;
    private InputAction move;
    public static InputAction interact;


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
        rb = GetComponent<Rigidbody2D>();

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
        return;
    }
}


