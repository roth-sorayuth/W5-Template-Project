using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // 0 = Rock, 1 = Paper, 2 = Scissors
    public enum Choice { Rock = 0, Paper = 1, Scissors = 2 }

    [Header("Images (UI Panel)")]
    public Image imgYou;            // ImgYou
    public Image imgCam;            // ImgCam

    [Header("Score Texts")]
    public TMP_Text txtYouScore;    // TxtYou
    public TMP_Text txtBotScore;    // Cam (bot score text)

    [Header("Result Text")]
    public TMP_Text txtResult;      // TxtResult

    [Header("Sprites (order: Rock, Paper, Scissors)")]
    public Sprite[] choiceSprites = new Sprite[3];
    public Sprite questionSprite;   // the "?" image shown at start

    [Header("Game Settings")]
    public int winningScore = 3;    // first to reach this wins
    public float restartDelay = 2f; // seconds before the game restarts

    private int playerScore = 0;
    private int botScore = 0;
    private bool gameOver = false;

    void Start()
    {
        ResetGame();
    }

    // ---------- Button hooks (set in each Button's OnClick) ----------
    public void OnRockClick()     { Play(Choice.Rock); }
    public void OnPaperClick()    { Play(Choice.Paper); }
    public void OnScissorClick()  { Play(Choice.Scissors); }

    // ---------- Game Logic ----------
    void Play(Choice playerChoice)
    {
        // Ignore clicks while waiting to restart
        if (gameOver) return;

        // 1) Computer random pick
        Choice botChoice = (Choice)Random.Range(0, 3);

        // 2) Update images
        imgYou.sprite = choiceSprites[(int)playerChoice];
        imgCam.sprite = choiceSprites[(int)botChoice];

        // 3) Decide winner
        int result = GetResult(playerChoice, botChoice); // 1 = win, 0 = draw, -1 = lose

        // 4) Update score + result text
        if (result == 1)
        {
            playerScore++;
            txtResult.text = "You Win!";
        }
        else if (result == -1)
        {
            botScore++;
            txtResult.text = "You Lose!";
        }
        else
        {
            txtResult.text = "Draw!";
        }

        UpdateScoreUI();

        // 5) Check if someone reached the winning score
        CheckGameOver();
    }

    void CheckGameOver()
    {
        if (playerScore >= winningScore)
        {
            txtResult.text = "YOU WIN THE GAME!";
            EndGame();
        }
        else if (botScore >= winningScore)
        {
            txtResult.text = "BOT WINS THE GAME!";
            EndGame();
        }
    }

    void EndGame()
    {
        gameOver = true;
        Invoke(nameof(ResetGame), restartDelay);
    }

    // Returns 1 if player wins, -1 if bot wins, 0 if draw
    int GetResult(Choice player, Choice bot)
    {
        if (player == bot) return 0;

        // Rock beats Scissors, Paper beats Rock, Scissors beats Paper
        bool playerWins =
            (player == Choice.Rock     && bot == Choice.Scissors) ||
            (player == Choice.Paper    && bot == Choice.Rock)     ||
            (player == Choice.Scissors && bot == Choice.Paper);

        return playerWins ? 1 : -1;
    }

    void UpdateScoreUI()
    {
        txtYouScore.text = playerScore.ToString();
        txtBotScore.text = botScore.ToString();
    }

    public void ResetGame()
    {
        CancelInvoke();
        gameOver = false;
        playerScore = 0;
        botScore = 0;
        imgYou.sprite = questionSprite;
        imgCam.sprite = questionSprite;
        txtResult.text = "Select Button Below";
        UpdateScoreUI();
    }
}