using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 1f;

    public Rigidbody2D rb;
    public Animator animator;
    public Joystick joystick;

    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        if (joystick.Horizontal >= .2f || joystick.Horizontal <= -.2f)
            movement.x = joystick.Horizontal;
        else if (joystick.Vertical >= .2f || joystick.Vertical <= -.2f)
            movement.y = joystick.Vertical;
        else
        {
            movement.x = 0;
            movement.y = 0;
        }

        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (horizontal != 0 || vertical != 0)
        {
            animator.SetFloat("lastMoveX", horizontal);
            animator.SetFloat("lastMoveY", vertical);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
