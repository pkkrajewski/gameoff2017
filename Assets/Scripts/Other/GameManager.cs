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
        ResetVariables();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        int newScore = CalculateHighScore();
        int bestScore = PlayerPrefs.GetInt("HighScore", 0);
        if (newScore > bestScore)
            PlayerPrefs.SetInt("HighScore", newScore);

        gameoverPanel.SetActive(true);
        newScoreText.text = newScore.ToString();
        bestScoreText.text = bestScore.ToString();

        enemiesDestroyed = 0;
        ResetVariables();
    }

    private void ResetVariables()
    {
        EnemyFollowPlayer.MoveSpeed = 1.0f;
        EnemyShootPlayer.MaxInterval = 1.5f;
        EnemyShootPlayer.BulletSpeed = 4.0f;
        Room.startingAmountOfEnemies = 1;
    }

    private int CalculateHighScore()
    {
        int score = enemiesDestroyed * roomManagaer.roomNumber;

        return score;
    }
}
