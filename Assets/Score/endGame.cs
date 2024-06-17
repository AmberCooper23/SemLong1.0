using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endGame: MonoBehaviour
{

    public TextMeshProUGUI player1ScoreTotal;
    public TextMeshProUGUI player2ScoreTotal;
    public TextMeshProUGUI player1Tips;
    public TextMeshProUGUI player2Tips;
    public TextMeshProUGUI player1WinnerText;
    public TextMeshProUGUI player2WinnerText;

    public GameObject orderText;
    public GameObject scoreText;
    public GameObject timerText;

    public GameObject endScreen;

    public ScoreManager scoreManager;
    public GameManager gameManager;

    private void Start()
    {
        endScreen.SetActive(false);

        player1ScoreTotal.gameObject.SetActive(false);

        player2ScoreTotal.gameObject.SetActive(false);

        player1Tips.gameObject.SetActive(false);

        player2Tips.gameObject.SetActive(false);

        player1WinnerText.gameObject.SetActive(false);

        player2WinnerText.gameObject.SetActive(false);

        TotalTips();
        TotalScore();
        Winner();
    }

    public void EndGameCondition()
    {
        endScreen.SetActive(true);

        player1ScoreTotal.gameObject.SetActive(true);

        player2ScoreTotal.gameObject.SetActive(true);

        player1Tips.gameObject.SetActive(true);

        player2Tips.gameObject.SetActive(true);

        player1WinnerText.gameObject.SetActive(true);

        player2WinnerText.gameObject.SetActive(true);

        scoreText.SetActive(false);
        timerText.SetActive(false);
        orderText.SetActive(false);
    }

    void TotalScore()
    {
        player1ScoreTotal.text = "Player 1 Total Score; $" + scoreManager.player1Score.ToString();
        player2ScoreTotal.text = "Player 2 Total Score; $" + scoreManager.player2Score.ToString();
    }

    void TotalTips()
    {
        int tips1 = 3 * scoreManager.player1Score;
        player1Tips.text = "Total tips made:" + tips1.ToString();

        int tips2 = 3 * scoreManager.player2Score;
        player2Tips.text = "Total tips made:" + tips2.ToString();
    }

    void Winner()
    {
        if (scoreManager.player1Score > scoreManager.player2Score) 
        { 
            player1WinnerText.text = "Good job Player 1! You got the promotion"; 
            player2WinnerText.text = "Sorry buddy, you didn't quite get there"; 
        } 
        else if 
        (scoreManager.player2Score > scoreManager.player1Score) 
        { 
            player1WinnerText.text = "Sorry buddy, you didn't quite get there"; 
            player2WinnerText.text = "Good job Player 2! You got the promotion"; 
        } 
        else if 
        (scoreManager.player1Score == scoreManager.player2Score) 
        { 
            player1WinnerText.text = "A tie?? Guess you'll have to try again!"; 
            player2WinnerText.text = "A tie?? Guess you'll have to try again!"; 
        }
    }

}