using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameObject hudPanel;
    private GameObject gameoverPanel;
    private Text newScoreText;
    private Text bestScoreText;

    private RoomManager roomManagaer;
    public static int enemiesDestroyed;

    private void Awake()
    {
        hudPanel = GameObject.Find("Hud");
        gameoverPanel = GameObject.Find("GameOver");
        newScoreText = GameObject.Find("Text New Score").GetComponent<Text>();
        bestScoreText = GameObject.Find("Text Best Score").GetComponent<Text>();
        roomManagaer = FindObjectOfType<RoomManager>();
    }

    private void Start()
    {
        gameoverPanel.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        if (enemiesDestroyed == 0) return;

        int newScore = CalculateHighScore();
        int bestScore = PlayerPrefs.GetInt("HighScore", 0);
        if (newScore > bestScore)
            PlayerPrefs.SetInt("HighScore", newScore);

        gameoverPanel.SetActive(true);
        newScoreText.text = newScore.ToString();
        bestScoreText.text = bestScore.ToString();

        enemiesDestroyed = 0;
    }

    private int CalculateHighScore()
    {
        int score = enemiesDestroyed * roomManagaer.roomNumber;

        return score;
    }
}
