
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;

    public static ScoreManager instance; // Singleton instance

    public int player1Score { get; private set; } = 0;
    public int player2Score { get; private set; } = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // So the score doesn't reset 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreText();
    }

    // Method to handle score change for collisions:
    public void HandleCollision(string playerTag, int scoreChange)
    {
        if (playerTag == "Player1")
        {
            player1Score += scoreChange;
            if (player1Score < 0) player1Score = 0; // So that P1 score doesn't return a negative number
        }
        else if (playerTag == "Player2")
        {
            player2Score += scoreChange;
            if (player2Score < 0) player2Score = 0; // So that P2 score doesn't return a negative number
        }
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        player1ScoreText.text = "Player 1 Score: " + player1Score.ToString();
        player2ScoreText.text = "Player 2 Score: " + player2Score.ToString();
    }
}


// Method to update the score UI (UI manager needs to handle the display):
/*private void UpdateScoreUI()
{
    //  there needs to be methods in UI Manager to update score display for this work so imma leave it as a comment for now:
    UIManager.instance.UpdatePlayer1Score(player1Score);
    UIManager.instance.UpdatePlayer2Score(player2Score);
}
}*/

