using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score = 0;
    public int highScore = 0;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public bool isGameStarted = false;
    public Button startButton;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        startButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        isGameStarted = true;
        UpdateUI();
        startButton.gameObject.SetActive(false);
    }

    public void AddScore(int value)
    {
        score += value;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        highScoreText.text = "HighScore: " + highScore;
    }

    public void ResetGame()
    {
        score = 0;
        UpdateUI();
        // Réinitialiser le serpent et la nourriture
    }
}