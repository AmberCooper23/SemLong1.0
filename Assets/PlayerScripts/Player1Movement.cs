using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem; //Make sure to have this installed on Unity by going to Window >> Package Manager>>Choose Unity Registery>>
   /*                            // Search for Input Systems then install
public class Player1Movement : MonoBehaviour //Attach to player 1 object
{
    
    public float runSpeed = 0.6f; // Running speed.
    public float jumpForce = 2.6f; // Jump height.

    private Rigidbody2D body; // Variable for the RigidBody2D component.
    private SpriteRenderer sr; // Variable for the SpriteRenderer component.

    private bool isGrounded; // Variable that will check if character is on the ground.
    public GameObject groundCheckPoint; // The object through which the isGrounded check is performed.
    public float groundCheckRadius; // isGrounded check radius.
    public LayerMask groundLayer; // Layer wich the character can jump on.

    private bool jumpPressed = false; // Variable that will check is "Space" key is pressed.
    private bool APressed = false; // Variable that will check is "A" key is pressed.
    private bool DPressed = false; // Variable that will check is "D" key is pressed.

    void Awake()
    {
        body = GetComponent<Rigidbody2D>(); // Setting the RigidBody2D component.
        sr = GetComponent<SpriteRenderer>(); // Setting the SpriteRenderer component.
    }

    // Update() is called every frame.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) jumpPressed = true; // Checking on "Space" key pressed.
        if (Input.GetKey(KeyCode.A)) APressed = true; // Checking on "A" key pressed.
        if (Input.GetKey(KeyCode.D)) DPressed = true; // Checking on "D" key pressed.

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * runSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pizza"))
        {
            ScoreManager.instance.AddScore(10);

            // Destroy the pizza
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Enemy1"))
        {
            // Slow down the player
            runSpeed -= 2f;
        }
        else if (other.CompareTag("Enemy2"))
        {
            ScoreManager.instance.SubtractScore(3);
        }
        else if (other.CompareTag("Enemy3"))
        {
            ScoreManager.instance.SubtractScore(5);
        }

    }

    // Update using for physics calculations.
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.transform.position, groundCheckRadius, groundLayer); // Checking if character is on the ground.

        // Left/Right movement.
        if (APressed)
        {
            body.velocity = new Vector2(-runSpeed, body.velocity.y); // Move left physics.
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z); // Rotating the character object to the left.
            APressed = false; // Returning initial value.
        }
        else if (DPressed)
        {
            body.velocity = new Vector2(runSpeed, body.velocity.y); // Move right physics.
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z); // Rotating the character object to the right.
            DPressed = false; // Returning initial value.
        }
        else body.velocity = new Vector2(0, body.velocity.y);

        // Jumps.
        if (jumpPressed && isGrounded)
        {
            body.velocity = new Vector2(0, jumpForce); // Jump physics.
            jumpPressed = false; // Returning initial value.
        }

       
    }
}
   

    #region ORIGINAL SCRIPT:

private GameObject Player1;
private Rigidbody2D rb1;

public GameObject spatula; //Place the prefab for a spatula/projectile (NB ensure that the object has the SpatulaCol script attached to it before you make
GameObject SpatulaObject; // it a Prefab.

public Vector2 JumpHeight = new Vector2(0, 5); //Change to alter the Jump Height (alter till it works for you)
public float MovementSpeed = 10f; //Change for preferance
public float SpatulaSpeed = 50f; //Recommended as spatula is a projectile

Keyboard key = Keyboard.current;
[SerializeField]
private InputAction playerOneMovement;

[SerializeField]
private TMP_Text aKeyOutput, dKeyOutput, aInput, dInput;



private bool OnGround;
private DirectionCheck direction;

private Vector2 Input;

public float groundCheckRadius = 0.1f; // Used to prevent player from jumping through shelves(not intended so all lines relating to this are commeneted out)
 public LayerMask groundLayer;
public bool isGrounded;
// Start is called before the first frame update

private void Awake()
{
    playerOneMovement.performed += PlayerOneMovement_performed;
    playerOneMovement.canceled += PlayerOneMovement_canceled;
    //playerTwoMovement.performed += PlayerTwoMovement_performed  ;
}


private void PlayerOneMovement_canceled(InputAction.CallbackContext obj)
{
    Input.x = 0;
}

private void PlayerOneMovement_performed(InputAction.CallbackContext obj)
{
    var valueInput = obj.ReadValue<float>();
    Debug.Log($"PlayerOneMovement {valueInput}");
    aInput.text = dInput.text = valueInput.ToString();
    Input += Vector2.right * valueInput;
    Input.Normalize();

}

void Start()
{
    Player1 = this.gameObject;
    rb1 = GetComponent<Rigidbody2D>();


    OnGround = true;

    direction = new DirectionCheck(false, false, false, false);
    Input = new Vector2();
}

public class PlayerController1
{
    private Vector2 Vel = new Vector2();
//Players velocity


 }



private struct DirectionCheck //Gets direction player is moving in to adjust direction of projectile
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


      void FixedUpdate()
 {
     Movement();
 
 
   Vector2 position = rb1.position;
 position.y -= groundCheckRadius;
 Collider2D hit = Physics2D.OverlapPoint(position, groundLayer);
 isGrounded = hit != null;
 if (isGrounded)
 {
 // Prevent the object from moving through the platform
   rb1.velocity = new Vector2(rb1.velocity.x, 0);
 }
  }    
private void Movement()
 {
     Vector2 Input = new Vector2();
     Debug.Log($"Keyboard: {key}");

    if (gameObject.tag == "Player1")
     {

        if (Keyboard.current.wKey.isPressed && OnGround == true)
         {

             rb1.AddForce(JumpHeight, ForceMode2D.Impulse);
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
        if (Keyboard.current.upArrowKey.isPressed && OnGround == true)
        {
            rb1.AddForce(JumpHeight * Vector2.up);
            direction.Up = true;
            direction.Down = false;
            direction.Left = false;
            direction.Right = false;

        }

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            Input += Vector2.left;
            direction.Up = false;
            direction.Down = false;
            direction.Left = true;
            direction.Right = false;
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            Input += Vector2.right;
            direction.Up = false;
            direction.Down = false;
            direction.Left = false;
            direction.Right = true;
        }

        if (Keyboard.current.downArrowKey.isPressed)
        {
            direction.Up = false;
            direction.Down = true;
            direction.Left = false;
            direction.Right = false;
        }

        if (Keyboard.current.rightShiftKey.isPressed)
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


}


#region SPATULA:
private Vector2 Spatula()
{
    Vector2 SpatulaDirection = new Vector2();
    Vector2 SpatulaVelocity = new Vector2();
  //  float Fixed = 0.25f;


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

    SpatulaVelocity = SpatulaDirection * SpatulaSpeed ;

    return SpatulaVelocity;

}

private void SpatulaGenerator()
{
    SpatulaObject = Instantiate(spatula, rb1.position, Quaternion.identity);
    //SpatulaObject.AddComponent<Rigidbody2D>();
    Rigidbody2D rbS = SpatulaObject.GetComponent<Rigidbody2D>();
    Transform transformS = SpatulaObject.transform;
    transformS.position = Player1.transform.position;


    rbS.velocity = Spatula();
}
#endregion

#region COLLISION CONTROL:
private void OnCollisionEnter2D(Collision2D collision)
{

    OnGround = true;


}

private void OnCollisionExit2D(Collision2D collision)
{
    OnGround = false;
    if (collision.gameObject == gameObject.CompareTag("Enemy")) //Fixes a bug where player can no longer jump when enemy collides
    {
        OnGround = true;
    }
}
#endregion
private void OnEnable()
{
    playerOneMovement.Enable();
}

private void OnDisable()
{
    playerOneMovement.Disable();
}


}
#endregion */