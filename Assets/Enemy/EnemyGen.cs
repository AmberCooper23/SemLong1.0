using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyGen : MonoBehaviour
{ //The comments next to the public variable is what must be placed in these variables
    public Transform trans; //Empty Parent Object (has 4 empty children but only add the parent) - explained in PositionVar struct
    PositionVar position;
    private Transform CurrentPos;

    public GameObject FlyPrefab; // the prefab with the Enemy character (I included the prefab file I used called Fly.prefab) 
    public GameObject Player1; //The player1 character
    private GameObject Fly;

    private GameObject[] EnemyObjects;
    private int EnemyNumber;
    private Rigidbody2D rb;

   // private Vector2 WalkVelocity = new Vector2(); //Not used and I was too lazy to delete

    public float timer = 0f; //Leave this its used to instantiate new enemies 
    public float WalkTimer = 6f; //Not used

    private int TakenPos = 0;

    // Start is called before the first frame update

    private struct PositionVar
    {
        public Transform Pos1, Pos2, Pos3, Pos4;


        public PositionVar(Transform pos1, Transform pos2, Transform pos3, Transform pos4) //NB create an empty parent object with 4 empty child objects.
        {                                                                                  // Place the 4 children where you want the enemies to spawn.
            Pos1 = pos1;                                                                   // Drag the parent object into the public GameObject variable: trans.
            Pos2 = pos2;
            Pos3 = pos3;
            Pos4 = pos4;


        }

    }

    void Start()
    {
        trans = GetComponent<Transform>();
        position = new PositionVar(trans.GetChild(0), trans.GetChild(1), trans.GetChild(2), trans.GetChild(3));

        EnemyNumber = 0; //Used for array index
        EnemyObjects = new GameObject[1000]; //Large number to store many enemies (i.e max would be 1000 enemies)
    }

    private void PosGen(int i) //Function which helps instantiate the Fly enemies
    {
        if (i == 0)
        {
            CurrentPos = position.Pos1;
        }

        if (i == 1)
        {
            CurrentPos = position.Pos2;
        }

        if (i == 2)
        {
            CurrentPos = position.Pos3;
        }

        if (i == 3)
        {
            CurrentPos = position.Pos4;
        }
    }

    private void EnemyGenerator() //Generates Flies (currently only 4 every 20 seconds) 
    {



        PosGen(TakenPos); //Gets a position from PosGen() function


        Fly = Instantiate(FlyPrefab, CurrentPos.position, Quaternion.identity);

        Fly.transform.tag = "Enemy"; //N.B use this tag to reference the enemy objects in the game !!!!

        Fly.AddComponent<Rigidbody2D>();
        Fly.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        Fly.AddComponent<BoxCollider2D>();

        EnemyObjects[EnemyNumber] = Fly; //Adds object to array
        EnemyNumber++;

        TakenPos++;


    }

    private void EnemyMovement() //Uses the array to allow all instantiated objects to move towards player1
    {
        Rigidbody2D rbPlayer1 = Player1.GetComponent<Rigidbody2D>();

        EnemyArrayReset();

        for (int i = 0; i < EnemyNumber; i++)
        {
            EnemyObjects[i].transform.position = Vector2.MoveTowards(EnemyObjects[i].transform.position, Player1.transform.position, 2f * Time.deltaTime);
        }




    }

    private void EnemyArrayReset() //Resets array if an enemy is killed so there are no null values in the array which can cause errors
    {
        GameObject Swap;

        for (int k = 0; k < EnemyNumber; k++)
        {
            if (EnemyObjects[k] == null)
            {
                Swap = EnemyObjects[EnemyNumber - 1];
                EnemyObjects[k] = Swap;
                EnemyNumber -= 2;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer == 0f)
        {
            for (int k = 0; k <= 3; k++)
            {
                EnemyGenerator(); //4 Enemies are generated every 20 seconds

            }

            timer = 20f;
            TakenPos = 0;
        }

        if (timer != 0f)
        {
            timer -= Time.deltaTime;
        }


        // EnemyArrayReset(); Script used in EnemyGen();
       
    }
    private void FixedUpdate()
    {
        EnemyMovement();

    }

    

    private void OnCollisionEnter2D(Collision2D collision) //Not currently used
    {

    }
}