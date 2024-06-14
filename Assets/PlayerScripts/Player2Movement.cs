using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2Movement : MonoBehaviour // this script is placed on the movementManager GameObject. the movementManager GameObject centralises the player movement.
{
   
    public GameObject player1;
    public GameObject player2;
    public Rigidbody2D rb1;
    public Rigidbody2D rb2;

    public GameObject spatulaPrefab;
    public float SpatulaSpeed = 50f;
    public float movementSpeed = 7f;

    private DirectionCheck direction1;
    private DirectionCheck direction2;

    public float jumpForce = 0.5f;
    public float jumpCooldown = 0.5f;
    private float lastJumpTimePlayer1;
    private float lastJumpTimePlayer2;

    // Start is called before the first frame update
    void Start()
    {
        rb1 = player1.GetComponent<Rigidbody2D>();
        rb2 = player2.GetComponent<Rigidbody2D>();

        direction1 = new DirectionCheck(false, false, false, false);
        direction2 = new DirectionCheck(false, false, false, false);
    }

    private void Update()
    {
        HandleMovement();
        HandleActions();
        UpdateDirectionChecks();
    }

    private void HandleMovement()
    {
        Vector2 input1 = Vector2.zero;
        Vector2 input2 = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
        {
            rb1.velocity = new Vector2(rb1.velocity.x, jumpForce);
            Debug.Log("w key pressed");
        }
        if (Keyboard.current.aKey.isPressed)
        {
            input1 += Vector2.left;
            direction1.SetDirection(Direction.Left);
        }
        if (Keyboard.current.sKey.isPressed)
        {
            input1 += Vector2.down;
            direction1.SetDirection(Direction.Down);
        }
        if (Keyboard.current.dKey.isPressed)
        {
            input1 += Vector2.right;
            direction1.SetDirection(Direction.Right);
        }

        if (Keyboard.current.upArrowKey.isPressed)
        {
            rb2.velocity = new Vector2(rb2.velocity.x, jumpForce);
            Debug.Log("up arrow pressed");
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            input2 += Vector2.left;
            direction2.SetDirection(Direction.Left);
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            input2 += Vector2.right;
            direction2.SetDirection(Direction.Right);
        }
        if (Keyboard.current.downArrowKey.isPressed)
        {
            input2 += Vector2.down;
            direction2.SetDirection(Direction.Down);
        }
        input1.Normalize();
        input2.Normalize();

        rb1.velocity = new Vector2(input1.x * movementSpeed, rb1.velocity.y);
        rb2.velocity = new Vector2(input2.x * movementSpeed, rb2.velocity.y);
    }

    private void HandleActions()
    {
        if (Keyboard.current.leftShiftKey.wasPressedThisFrame)
        {
            FireProjectile(rb1, direction1);
        }
        if (Keyboard.current.rightShiftKey.wasPressedThisFrame)
        {
            FireProjectile(rb2, direction2);
        }
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }
    }

    private void FireProjectile(Rigidbody2D rb, DirectionCheck directionCheck)
    {
        GameObject projectile = Instantiate(spatulaPrefab, rb.position, Quaternion.identity);
        Rigidbody2D rbProjectile = projectile.GetComponent<Rigidbody2D>();
        rbProjectile.velocity = GetProjectileDirection(directionCheck) * SpatulaSpeed;
    }

    private Vector2 GetProjectileDirection(DirectionCheck directionCheck)
    {
        if (directionCheck.Up) return Vector2.up;
        if (directionCheck.Down) return Vector2.down;
        if (directionCheck.Left) return Vector2.left;
        if (directionCheck.Right) return Vector2.right;
        return Vector2.zero;
    }

    private void UpdateDirectionChecks()
    {
        direction1.Up = Keyboard.current.wKey.isPressed;
        direction1.Down = Keyboard.current.sKey.isPressed;
        direction1.Left = Keyboard.current.aKey.isPressed;
        direction1.Right = Keyboard.current.dKey.isPressed;

        direction2.Up = Keyboard.current.upArrowKey.isPressed;
        direction2.Down = Keyboard.current.downArrowKey.isPressed;
        direction2.Left = Keyboard.current.leftArrowKey.isPressed;
        direction2.Right = Keyboard.current.rightArrowKey.isPressed;
    }

    private struct DirectionCheck
    {
        public bool Up, Down, Left, Right;

        public DirectionCheck(bool up, bool down, bool left, bool right)
        {
            Up = up;
            Down = down;
            Left = left;
            Right = right;
        }

        public void SetDirection(Direction direction)
        {
            Up = direction == Direction.Up;
            Down = direction == Direction.Down;
            Left = direction == Direction.Left;
            Right = direction == Direction.Right;
        }
    }

    private enum Direction
    {
        Up, Down, Left, Right
    }
}

/*
public GameObject player1;
public GameObject player2;
public Rigidbody2D rb1;
public Rigidbody2D rb2;

public GameObject spatulaPrefab;
public float SpatulaSpeed = 50f;
public float groundCheckRadius = 0.1f;
public LayerMask groundLayer;
public float movementSpeed = 7f;

private DirectionCheck direction;

private bool isGrounded1;
private bool isGrounded2;
private DirectionCheck direction1;
private DirectionCheck direction2;

public float jumpForce = 0.5f;
public float jumpCooldown = 0.5f;
private float lastJumpTime;
private float lastJumpTimePlayer1;
private float lastJumpTimePlayer2;



// Start is called before the first frame update
void Start()
{
    rb1 = player1.GetComponent<Rigidbody2D>();
    rb2 = player2.GetComponent<Rigidbody2D>();

    direction1 = new DirectionCheck(false, false, false, false);
    direction2 = new DirectionCheck(false, false, false, false);
}

private void Update()
{
    HandleMovement();
    HandleActions();

    UpdateDirectionChecks();
    CheckGrounded();
}

private void HandleMovement()
{
    Vector2 input1 = Vector2.zero;
    Vector2 input2 = Vector2.zero;

    if (Keyboard.current.wKey.isPressed && IsGrounded(rb1))
    {
        rb1.velocity = new Vector2(rb1.velocity.x, jumpForce);
        Debug.Log("w key pressed");
    }
    if (Keyboard.current.aKey.isPressed)
    {
        input1 += Vector2.left;
        direction1.SetDirection(Direction.Left);

    }
    if (Keyboard.current.sKey.isPressed)
    {
        input1 += Vector2.down;
        direction1.SetDirection(Direction.Down);
    }
    if (Keyboard.current.dKey.isPressed)
    {
        input1 += Vector2.right;
        direction1.SetDirection(Direction.Right);
    }

    if (Keyboard.current.spaceKey.isPressed && IsGrounded(rb2))
    {
        rb2.velocity = new Vector2(rb2.velocity.x, jumpForce);
        Debug.Log("up arrow pressed");
    }
    if (Keyboard.current.leftArrowKey.isPressed)
    {
        input2 += Vector2.left;
        direction2.SetDirection(Direction.Left);
        Debug.Log("left arrow pressed");
    }
    if (Keyboard.current.rightArrowKey.isPressed)
    {
        input2 += Vector2.right;
        direction2.SetDirection(Direction.Right);

    }
    if (Keyboard.current.downArrowKey.isPressed)
    {
        input2 += Vector2.down;
        direction2.SetDirection(Direction.Down);
    }
    input1.Normalize();
    input2.Normalize();

    rb1.velocity = new Vector2(input1.x * movementSpeed, rb1.velocity.y);
    rb2.velocity = new Vector2(input2.x * movementSpeed, rb2.velocity.y);
}

private void HandleActions()
{
    if (Keyboard.current.leftShiftKey.wasPressedThisFrame)
    {
        FireProjectile(rb1, direction1);
    }
    if (Keyboard.current.rightShiftKey.wasPressedThisFrame)
    {
        FireProjectile(rb2, direction2);
    }
    if (Keyboard.current.escapeKey.wasPressedThisFrame)
    {
        Application.Quit();
    }
}

private void CheckGrounded()
{
    isGrounded1 = IsGrounded(rb1);
    isGrounded2 = IsGrounded(rb2);
}
private bool IsGrounded(Rigidbody2D player)
{
    Collider2D collider = player.GetComponent<Collider2D>();
    Vector2 groundCheckPosition = new Vector2(collider.bounds.center.x, collider.bounds.min.y - groundCheckRadius);
    Debug.DrawRay(groundCheckPosition, Vector2.down * groundCheckRadius, Color.green);
    bool grounded = Physics2D.OverlapCircle(groundCheckPosition, groundCheckRadius, groundLayer) != null && Time.time - lastJumpTime > jumpCooldown;
    return grounded;
}
private void FireProjectile(Rigidbody2D rb, DirectionCheck directionCheck)
{
    GameObject projectile = Instantiate(spatulaPrefab, rb.position, Quaternion.identity);
    Rigidbody2D rbProjectile = projectile.GetComponent<Rigidbody2D>();
    rbProjectile.velocity = GetProjectileDirection(directionCheck) * SpatulaSpeed;
}
private Vector2 GetProjectileDirection(DirectionCheck directionCheck)
{
    if (directionCheck.Up) return Vector2.up;
    if (directionCheck.Down) return Vector2.down;
    if (directionCheck.Left) return Vector2.left;
    if (directionCheck.Right) return Vector2.right;
    return Vector2.zero;
}
private void OnCollisionEnter2D(Collision2D collision)
{

    if (collision.gameObject.CompareTag("Ground"))
    {
        if (gameObject.tag == "Player1")
        {
            isGrounded1 = true;
        }
        else if (gameObject.tag == "Player2")
        {
            isGrounded2 = true;
        }
    }
}
private void OnCollisionExit2D(Collision2D collision)
{

    if (collision.gameObject.CompareTag("Ground"))
    {
        if (gameObject.tag == "Player1")
        {
            isGrounded1 = false;
        }
        else if (gameObject.tag == "Player2")
        {
            isGrounded2 = false;
        }
    }
}

private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Ground"))
    {
        if (gameObject.tag == "Player1")
        {
            isGrounded1 = true;
        }
    }
    if (collision.CompareTag("Ground"))
    {
        if (gameObject.tag == "Player2")
        {
            isGrounded2 = true;
        }
    }

}

private void OnTriggerExit2D(Collider2D collision)
{
    //check if the collsion is happening with a game object with "ground" tag.
    if (collision.CompareTag("Ground"))
    {
        if (gameObject.tag == "Player1")
        {
            isGrounded1 = false;
        }
    }
    if (collision.CompareTag("Ground"))
    {
        if (gameObject.tag == "Player2")
        {
            isGrounded2 = false;
        }
    }
}

private void UpdateDirectionChecks()
{
    // Player 1 direction update
    direction1.Up = Keyboard.current.wKey.isPressed;
    direction1.Down = Keyboard.current.sKey.isPressed;
    direction1.Left = Keyboard.current.aKey.isPressed;
    direction1.Right = Keyboard.current.dKey.isPressed;

    // Player 2 direction update
    direction2.Up = Keyboard.current.upArrowKey.isPressed;
    direction2.Down = Keyboard.current.downArrowKey.isPressed;
    direction2.Left = Keyboard.current.leftArrowKey.isPressed;
    direction2.Right = Keyboard.current.rightArrowKey.isPressed;
}


private struct DirectionCheck
{
    public bool Up, Down, Left, Right;

    public DirectionCheck(bool up, bool down, bool left, bool right)
    {
        Up = up;
        Down = down;
        Left = left;
        Right = right;
    }

    public void SetDirection(Direction direction)
    {
        Up = direction == Direction.Up;
        Down = direction == Direction.Down;
        Left = direction == Direction.Left;
        Right = direction == Direction.Right;
    }
}

private enum Direction
{
    Up, Down, Left, Right
}
}

/* private void Movement()
{
    Vector2 Input = new Vector2();
    if (gameObject.tag == "Player1")
    {

        if (Keyboard.current.wKey.isPressed && OnGround == true)
        {

            rb1.AddForce(JumpHeight * Vector2.up, ForceMode2D.Impulse);
            direction.Up = true;
            direction.Down = false;
            direction.Left = false;
            direction.Right = false;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            Input += Vector2.left;
            direction.Up = false;
            direction.Down = false;
            direction.Left = true;
            direction.Right = false;
        }

        if (Keyboard.current.dKey.isPressed)
        {
            Input += Vector2.right;
            direction.Up = false;
            direction.Down = false;
            direction.Left = false;
            direction.Right = true;
        }

        if (Keyboard.current.sKey.isPressed)
        {
            direction.Up = false;
            direction.Down = true;
            direction.Left = false;
            direction.Right = false;

        }

        if (Keyboard.current.leftShiftKey.isPressed)
        {
            Destroy(SpatulaObject);
            SpatulaGenerator();
        }

        if (Keyboard.current.escapeKey.isPressed)
        {
            Application.Quit();
        }

        Input.Normalize();
        rb1.velocity = Input * MovementSpeed;
    }
    if (gameObject.tag == "Player2")
    {
        if (key.upArrowKey.isPressed && OnGround == true)
        {
            rb2.AddForce(JumpHeight * Vector2.up, ForceMode2D.Impulse);
            direction.Up = true;
            direction.Down = false;
            direction.Left = false;
            direction.Right = false;


        }

        if (key.leftArrowKey.isPressed)
        {
            Input += Vector2.left;
            direction.Up = false;
            direction.Down = false;
            direction.Left = true;
            direction.Right = false;
        }

        if (key.rightArrowKey.isPressed)
        {
            Input += Vector2.right;
            direction.Up = false;
            direction.Down = false;
            direction.Left = false;
            direction.Right = true;
        }

        if (key.downArrowKey.isPressed)
        {
            direction.Up = false;
            direction.Down = true;
            direction.Left = false;
            direction.Right = false;

        }
        if (key.rightShiftKey.isPressed)
        {
            Destroy(SpatulaObject);
            SpatulaGenerator();
        }

        if (key.escapeKey.isPressed)
        {
            Application.Quit();
        }





        Input = Input.normalized;
        rb2.velocity = Input * MovementSpeed;
    }

}

private Vector2 Spatula()
{
    Vector2 SpatulaDirection = new Vector2();
    Vector2 SpatulaVelocity = new Vector2();
   // float Fixed = 0.25f;


    if (direction.Up)
    {
        SpatulaDirection = Vector2.up;
    }

    if (direction.Down)
    {
        SpatulaDirection = Vector2.down;
    }

    if (direction.Right)
    {
        SpatulaDirection = Vector2.right;
    }

    if (direction.Left)
    {
        SpatulaDirection = Vector2.left;
    }

    SpatulaVelocity = SpatulaDirection * SpatulaSpeed  ;

    return SpatulaVelocity;

}

private void SpatulaGenerator()
{
    SpatulaObject = Instantiate(spatula, rb2.position, Quaternion.identity);
    // SpatulaObject.AddComponent<Rigidbody2D>();
    Rigidbody2D rbS = SpatulaObject.GetComponent<Rigidbody2D>();
    Transform transformS = SpatulaObject.transform;


    rbS.velocity = Spatula();
}
private struct DirectionCheck
{
    public bool Up, Down, Left, Right;

    public DirectionCheck(bool up, bool down, bool left, bool right)
    {
        Up = up;
        Down = down;
        Left = left;
        Right = right;

    }

}

// Update is called once per frame
void Update()
{

}


private void OnCollisionEnter2D(Collision2D collision)
{

    OnGround = true;


}

void FixedUpdate()
{
    // Vector2 position = rb2.position;
    // position.y -= groundCheckRadius;
    // Collider2D hit = Physics2D.OverlapPoint(position, groundLayer);
    //// isGrounded = hit != null;
    // if (isGrounded)
    // {
    // Prevent the object from moving through the platform
    //     rb2.velocity = new Vector2(rb2.velocity.x, 0);
    // }

    Movement();
}

private void OnCollisionExit2D(Collision2D collision)
{
    OnGround = false;
    if (collision.gameObject == gameObject.CompareTag("Enemy"))
    {
        OnGround = true;
    }
}
} */