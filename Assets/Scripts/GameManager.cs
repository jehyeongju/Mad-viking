using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Text textScore = null;
    [SerializeField]
    private Text textLife = null;
    [SerializeField]
    private Text textHighScore = null;
    [SerializeField]
    private GameObject enemyCrow = null;
    [SerializeField]
    private GameObject enemySnake = null;
    
    public Vector2 MinPosition { get; private set; }
    public Vector2 MaxPosition { get; private set; }
    public PoolManager poolManager { get; private set; }

    private int score = 0;
    private int life = 3;
    private int highScore = 0;

    void Awake()
    {
        poolManager = FindObjectOfType<PoolManager>();
        highScore = PlayerPrefs.GetInt("HIGHSCORE", 500);
        MinPosition = new Vector2(-3.5f, -1.8f);
        MaxPosition = new Vector2(3.5f, 1.8f);
        StartCoroutine(SpawnCrow());
        StartCoroutine(SpawnSnake());
        UpdateUI();
    }
    public void UpdateUI()
    {
        textScore.text = string.Format("SCORE\n{0}", score);
        textLife.text = string.Format("LIFE\n{0}", life);
        textHighScore.text = string.Format("HIGHSCORE\n{0}", highScore);
    }
    public void AddScore(int addScore)
    {
        score += addScore;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HIGHSCORE", highScore);
        }
        UpdateUI();
    }
    public int GetLife()
    {
        return life;
    }

    public void Dead()
    {
        life--;
        if (life <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        UpdateUI();
    }

    private IEnumerator SpawnCrow()
    {
        float randomY = 0f;
        float randomDelay = 0f;

        while (true)
        {
            randomY = Random.Range(-1.5f, 1.2f);
            randomDelay = Random.Range(1f, 5f);
            for (int i = 0; i < 2; i++)
            {
                Instantiate(enemyCrow, new Vector2(4.4f, randomY), Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(randomDelay);
        }
    }
    private IEnumerator SpawnSnake()
    {
        float randomY = 0f;
        float randomDelay = 0f;

        yield return new WaitForSeconds(5f);

        while (true)
        {
            randomY = Random.Range(-1.5f, 1.2f);
            randomDelay = Random.Range(1f, 5f);
            for (int i = 0; i < 1; i++)
            {
                Instantiate(enemySnake, new Vector2(4.4f, randomY), Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(randomDelay);
        }
    }
}