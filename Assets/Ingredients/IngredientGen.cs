using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IngredientGen : MonoBehaviour
{
    public GameObject BaconPrefab;
    public GameObject OlivePrefab;
    public GameObject CheesePrefab;
    public GameObject PepperPrefab;
    public GameObject MushroomPrefab;
    public GameObject OnionPrefab;
    public GameObject PepperoniPrefab;
    public GameObject PineapplePrefab;
    public GameObject SausagePrefab;

    private GameObject Current_obj;

    // Arrays storing symbols (representing an ingredient) relative to their worth
    private char[] Value_1; //Pepperoni
    private char[] Value_2;      //Bacon and Sausage
    private char[] Value_3; //Mushroom Olive Pineapple Pepper
    private char[] Value_4; //Onion Cheese
    
    //Timers for respawning Items based on their worth (Timer1 is most valuable and Timer4 is LeastValue)
    private float Timer1 = 20f;
    private float Timer2 = 15f;
    private float Timer3 = 10f;
    private float Timer4 = 5f;

    //Spawn locations determined by empty game objects ontop of the scene
    public Transform SpawnParent_1;
    private Transform[] Spawn_Spot1;

    //Int value to track which Spawn spot is taken to avoid overlapping objects
   // int iTaken_Spot = -1;
    // Start is called before the first frame update
    void Start()
    {
        Value_1 = new char[1]; Value_1[0] = 'P'; //Array stores index for pepperoni
        Value_3 = new char[4]; Value_3[0] = 'M'; Value_3[1] = 'O'; Value_3[2] = 'P'; Value_3[3] = 'E'; //Array stores Mushroom(M), Olives(O), Pineapple(P), Pepper(E)
        Value_2 = new char[2];Value_2[0] = 'B'; Value_2[1] = 'S';//Bacon and Sausage
        Value_4 = new char[2];Value_4[0] = 'O'; Value_4[1] = 'C';//Onion and Cheese
        
        Spawn_Spot1 = new Transform[8];
        for (int i = 0; i < SpawnParent_1.childCount; i++)
        {
            Spawn_Spot1[i] = SpawnParent_1.GetChild(i);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        SpawnCountdown1();
        SpawnCountdown2();
        SpawnCountdown3();
        SpawnCountdown4();
    }


    void SpawnCountdown1() //Pepperoni only
    {
        int Index = 0;
        if (Timer1>=0)
        {
            Timer1-= Time.deltaTime;
        }
        else
        {
            Timer1 = 20f;
            IngDet1(Index);
        }
    }

    void SpawnCountdown2() //Bacon and Sausage
    {
        int Index = Random.Range(0,2);
        if (Timer2>=0)
        {
            Timer2-= Time.deltaTime;
        }
        else
        {
            Timer2 = Timer3+15f;
            IngDet2(Index);
        }
    }

    void SpawnCountdown3() //Mushrooms, Olives, Pineapple, Pepper
    {
        int Index = Random.Range(0, 4);
        if (Timer3>=0)
        {
            Timer3-= Time.deltaTime;
        }
        else
        {
            Timer3 = Timer4+10f;
            IngDet3(Index);
        }
    }

    void SpawnCountdown4() //Onion and cheese
    {
        int Index = Random.Range(0,2);
        if(Timer4>=0)
        {
            Timer4-= Time.deltaTime;
        }
        else
        {
            Timer4 = Timer1+5f; //Needs to respawn after final spawn
            IngDet4(Index);
        }
    }
    Transform SpawnLocation()
    {
        int Pos = Random.Range(0, 8);
        return Spawn_Spot1[Pos];
    }

   void GenerateBacon()
    {
        Destroy(Current_obj);
        Current_obj = Instantiate(BaconPrefab, SpawnLocation().position, Quaternion.identity);
        Debug.Log("Spawning Bacon");
    }

    void GenerateOlive()
    {
        Destroy(Current_obj);
        Current_obj = Instantiate(OlivePrefab, SpawnLocation().position, Quaternion.identity);
        Debug.Log("Spawning Olives");
    }

    void GenerateCheese()
    {
        Destroy(Current_obj);
        Current_obj = Instantiate(CheesePrefab, SpawnLocation().position, Quaternion.identity);
        Debug.Log("Spawning Cheese");
    }

    void GeneratePepper()
    {
        Destroy(Current_obj);
        Current_obj = Instantiate(PepperPrefab, SpawnLocation().position, Quaternion.identity);
        Debug.Log("Spawning Pepper");
    }

    void GenerateMushroom()
    {
        Destroy(Current_obj);
        Current_obj = Instantiate(MushroomPrefab, SpawnLocation().position, Quaternion.identity);
        Debug.Log("Spawning Mushroom");
    }

    void GenerateOnion()
    {
        Destroy(Current_obj);
        Current_obj = Instantiate(OnionPrefab, SpawnLocation().position, Quaternion.identity);
        Debug.Log("Spawning onion");
    }

    void GeneratePepperoni()
    {
        Destroy (Current_obj);
        Current_obj = Instantiate(PepperoniPrefab, SpawnLocation().position, Quaternion.identity);
        Debug.Log("Spawning Pepperoni");
    }

    void GeneratePineapple()
    {
        Destroy(Current_obj);
        Current_obj = Instantiate(PineapplePrefab, SpawnLocation().position, Quaternion.identity);
        Debug.Log("Spawning Pineapple");
    }

    void GenerateSausage()
    {
        Destroy(Current_obj);
        Current_obj = Instantiate(SausagePrefab, SpawnLocation().position, Quaternion.identity);
        Debug.Log("Spawning Sausage");
    }

    void IngDet1(int Index)
    {
        if (Index == 0)//Only pepperoni currently
        {
            GeneratePepperoni();
        }
    }

    void IngDet2(int Index) //Bacon sausage
    {
        if (Value_2[Index] == 'B')
        {
            GenerateBacon();
        }
        else if (Value_2[Index] =='S')
        {
            GenerateSausage();
        }
    }

    void IngDet3(int Index) //Mushrooms, Olives, Pineapple, Pepper
    {
        if (Value_3[Index] == 'M')
        {
            GenerateMushroom();
        }
        else if (Value_3[Index] =='O')
        {
            GenerateOlive();
        }
        else if (Value_3[Index] =='P')
        {
            GeneratePineapple();
        }
        else if (Value_3[Index] =='E')
        {
            GeneratePepper();
        }
    }

    void IngDet4(int Index) //Onion cheese
    {
        if (Value_4[Index] == 'O')
        {
            GenerateOnion();
        }
        else if (Value_4[Index] =='C')
        {
            GenerateCheese();
        }
    }

}
