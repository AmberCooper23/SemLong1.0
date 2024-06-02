using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2Movement : MonoBehaviour //Updated to include projectiles
{
    private GameObject Player2;
    private Rigidbody2D rb2;

    public GameObject spatula;
    GameObject SpatulaObject;
    public float SpatulaSpeed = 50f;

    Keyboard key = Keyboard.current;

    public float JumpHeight = 2000f;
    public float MovementSpeed = 7f;
    //public float JumpTimer = 1f;

    private DirectionCheck direction;

    private bool OnGround;

    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        Player2 = this.gameObject;
        rb2 = GetComponent<Rigidbody2D>();


        OnGround = true;

        direction = new DirectionCheck(false, false, false, false);
    }
    private void Movement()
    {
        Vector2 Input = new Vector2();

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
        rb2.velocity = Input * MovementSpeed ;


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
}