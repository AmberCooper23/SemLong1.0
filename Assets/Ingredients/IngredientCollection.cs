using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class IngredientCollection : MonoBehaviour
{
    public GameManager gameManager; //References GameManager script
    private bool[] bOrder = new bool[8]; //Correlates to the pizzaOrder array in ScoreManager
    private string[] playerOrder = new string[1000]; //Tracks collected ingredients relevant to the order so multiple pickups of the same ingredient does not complete the order
    private int playerOrderIndex = 0; //Index used to determine the length of the array

    bool bCollectedPepperoni = false; //Variables checking whether an ingredient relevant to the order has already been picked up
    bool bCollectedMushrooms = false;
    bool bCollectedOnions = false;
    bool bCollectedSausage = false;
    bool bCollectedBacon = false;
    bool bCollectedCheese = false;
    bool bCollectedOlives = false;
    bool bCollectedPeppers = false;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < bOrder.Length; i++) //Initializes the bOrder array
        {
            bOrder[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < gameManager.numToppings; i++)
        {
            bOrder[gameManager.arrToppingsIndex[i]] = true; //Sets specific points in the array to true to help determine which ingredients need to be picked up for the order
        }

        gameManager.CheckOrder(playerOrder, playerOrderIndex); //Checks whether the order is complete from the GameManager script

        if (playerOrderIndex == gameManager.numToppings - 1) //If all the relevant ingredients are collected the array resets for the next order
        {
            ResetOrder(bOrder, playerOrder);

        }
    }

        void collectPepperoni() //These functions help the process of collecting an ingredient
        {


            for (int i = 0; i < playerOrder.Length; i++)
            {
                if (playerOrder[i] == "Pepperoni") //If the ingredient already exists in the array (picked up) the ingredient is ignored
                {
                    bCollectedPepperoni = true;
                }
            }
            if (bOrder[0] == true && bCollectedPepperoni == false) //The Index 0 corresponds to Pepperoni (follows order of ingredients from the order array in GameManager)
            {
                playerOrder[playerOrderIndex] = "Pepperoni";
                playerOrderIndex++;
            }
        }

        void collectMushrooms()
        {
            for (int i = 0; i < playerOrder.Length; i++)
            {
                if (playerOrder[i] == "Mushrooms")
                {
                    bCollectedMushrooms = true;
                }
            }
            if (bOrder[1] == true && bCollectedMushrooms == false)
            {
                playerOrder[playerOrderIndex] = "Mushrooms";
                playerOrderIndex++;
            }
        }

        void collectOnions()
        {
            for (int i = 0; i < playerOrder.Length; i++)
            {
                if (playerOrder[i] == "Onions")
                {
                    bCollectedOnions = true;
                }
            }
            if (bOrder[2] == true && bCollectedOnions == false)
            {
                playerOrder[playerOrderIndex] = "Onions";
                playerOrderIndex++;
            }
        }

        void collectSausage()
        {
            for (int i = 0; i < playerOrder.Length; i++)
            {
                if (playerOrder[i] == "Sausage")
                {
                    bCollectedSausage = true;
                }
            }
            if (bOrder[3] == true && bCollectedSausage == false)
            {
                playerOrder[playerOrderIndex] = "Sausage";
                playerOrderIndex++;
            }
        }

        void collectBacon()
        {
            for (int i = 0; i < playerOrder.Length; i++)
            {
                if (playerOrder[i] == "Bacon")
                {
                    bCollectedBacon = true;
                }
            }
            if (bOrder[4] == true && bCollectedBacon == false)
            {
                playerOrder[playerOrderIndex] = "Bacon";
                playerOrderIndex++;
            }
        }
        void collectCheese()
        {
            for (int i = 0; i < playerOrder.Length; i++)
            {
                if (playerOrder[i] == "Cheese")
                {
                    bCollectedCheese = true;
                }
            }
            if (bOrder[5] == true && bCollectedCheese == false)
            {
                playerOrder[playerOrderIndex] = "Cheese";
                playerOrderIndex++;
            }
        }
        void collectOlives()
        {
            for (int i = 0; i < playerOrder.Length; i++)
            {
                if (playerOrder[i] == "Olives")
                {
                    bCollectedOlives = true;
                }
            }
            if (bOrder[6] == true && bCollectedOlives == false)
            {
                playerOrder[playerOrderIndex] = "Olives";
                playerOrderIndex++;
            }
        }
        void collectPeppers()
        {
            for (int i = 0; i < playerOrder.Length; i++)
            {
                if (playerOrder[i] == "Peppers")
                {
                    bCollectedPeppers = true;
                }
            }
            if (bOrder[7] == true && bCollectedPeppers == false)
            {
                playerOrder[playerOrderIndex] = "Peppers";
                playerOrderIndex++;
            }
        }

        void ResetOrder(bool[] bArr, string[] sArr) //Resets arrays upon a completed order
        {
            for (int i = 0; i < gameManager.numToppings; i++)
            {
                bArr[gameManager.arrToppingsIndex[i]] = false;
                sArr[i] = " ";
            }
            playerOrderIndex = 0;
        }

    private void OnCollisionEnter2D(Collision2D collision) //Checks name of object collided with and performs a collect...() function depending on the name
    {
        GameObject CollidedObject = collision.gameObject;

        if (CollidedObject.name == "Bacon(Clone)")
        {
            collectBacon();
            Destroy(CollidedObject);
            Debug.Log("Collecting Bacon");
        }

        if (CollidedObject.name == "BlackOlives(Clone)")
        {
            collectOlives();
            Destroy(CollidedObject);
            Debug.Log("Collecting Olives");
        }

        if (CollidedObject.name == "Cheese(Clone)")
        {
            collectCheese();
            Destroy(CollidedObject);
            Debug.Log("Collecting Cheese");
        }

        if (CollidedObject.name == "GreenPepper(Clone)")
        {
            collectPeppers();
            Destroy(CollidedObject);
            Debug.Log("Collecting sausage");
        }

        if (CollidedObject.name == "Mushroom(Clone)")
        {
            collectMushrooms();
            Destroy(CollidedObject);
            Debug.Log("Collecting Mushroom");
        }

        if (CollidedObject.name == "Onion(Clone)")
        {
            collectOnions();
            Destroy(CollidedObject);
            Debug.Log("Collecting Onion");
        }

        if (CollidedObject.name == "Peperoni(Clone)")
        {
            collectPepperoni();
            Destroy(CollidedObject);
            Debug.Log("Collecting Pepperoni");
        }

        if (CollidedObject.name == "Sausage(Clone)")
        {
            collectSausage();
            Destroy(CollidedObject);
            Debug.Log("Collecting Sausage");
        }
    }








}
