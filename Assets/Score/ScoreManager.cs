
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public int score { get; private set; }

    void Start()
    {
        score = 0;
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    public void SubtractScore(int points)
    {
        score -= points;
        if (score < 0)
            score = 0;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public static ScoreManager instance; // Singleton instance

    public int player1Score = 0;
    public int player2Score = 0;

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

    // Method to increase or decrease scores based on collisions:
    public void HandleCollision(string playerTag, int scoreChange)
    {
        if (playerTag == "Player1")
        {
            player1Score += scoreChange;
            if (player1Score < 0) player1Score = 0; // So that P1 score doesn't return a negative number
            UpdateScoreText();
        }
        else if (playerTag == "Player2")
        {
            player2Score += scoreChange;
            if (player2Score < 0) player2Score = 0; // So that P2 score doesnt return a negative number
            UpdateScoreText();
        }
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

