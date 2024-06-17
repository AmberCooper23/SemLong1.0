using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region INITIALISATION:
    public TextMeshProUGUI orderText;
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;
    public TextMeshProUGUI timerText;

    public ScoreManager scoreManager;

    private string[] pizzaToppings = { "Pepperoni", "Mushrooms", "Onions", "Sausage", "Bacon", "Extra Cheese", "Black Olives", "Green Peppers" };
    public int[] arrToppingsIndex = new int[8];
    private string currentOrder;
    private float timer = 30f;
    private bool bonusAchieved = false;
    public int numToppings;

    void Start()
    {
        GenerateOrder();
        UpdateOrderText();
        UpdateScoreText();
        UpdateTimerText();
    }
    #endregion

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimerText();

            if (!bonusAchieved && timer <= 0)
            {
                scoreManager.HandleCollision("Player1", 5); // Bonus for completing order within 30 seconds for Player1 (or you can decide based on logic)
                bonusAchieved = true;
                UpdateScoreText();
            }
        }
        else
        {
            // Time's up, generate a new order and reset the timer
            GenerateOrder();
            timer = 30f;
            bonusAchieved = false;
        }
    }

    #region ORDERS:
    void GenerateOrder()
    {
        currentOrder = "Order: ";
        numToppings = Random.Range(1, 4); // Random number of toppings between 1 and 3

        for (int i = 0; i < numToppings; i++)
        {
            int toppingIndex = Random.Range(0, pizzaToppings.Length);
            arrToppingsIndex[i] = toppingIndex;
            currentOrder += pizzaToppings[toppingIndex] + ", ";
        }

        currentOrder = currentOrder.TrimEnd(' ', ',');
        UpdateOrderText();
    }

    public void CheckOrder(string[] playerToppings, int arrayLength)
    {
        string[] orderToppings = currentOrder.Substring(7).Split(',');
        bool orderCompleted = true;

        if (orderToppings.Length != arrayLength)
            orderCompleted = false;
        else
        {
            foreach (string topping in orderToppings)
            {
                bool toppingFound = false;
                foreach (string playerTopping in playerToppings)
                {
                    if (topping.Trim() == playerTopping.Trim())
                    {
                        toppingFound = true;
                        break;
                    }
                }
                if (!toppingFound)
                {
                    orderCompleted = false;
                    break;
                }
            }
        }

        if (orderCompleted)
        {
            scoreManager.HandleCollision("Player1", 10); // Increase score for completing order for Player1 (or logic based on player who completed)
            GenerateOrder(); // Generate new order
            timer = 30f; // Reset timer
            bonusAchieved = false;
            orderCompleted = false; // Prevents score from being added without the order being complete
            UpdateScoreText(); // Update score text
        }
    }
    #endregion

    #region UPDATE TEXT:
    void UpdateOrderText()
    {
        orderText.text = currentOrder;
    }

    void UpdateScoreText()
    {
        player1ScoreText.text = "Player 1 Score: " + scoreManager.player1Score.ToString();
        player2ScoreText.text = "Player 2 Score: " + scoreManager.player2Score.ToString();
    }

    void UpdateTimerText()
    {
        timerText.text = "Time: " + Mathf.Round(timer).ToString();
    }
    #endregion
}
