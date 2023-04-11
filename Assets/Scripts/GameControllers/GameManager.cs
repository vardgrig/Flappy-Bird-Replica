using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int score;
    public static int highScore;
    public Text scoreText;
    public GameObject WhiteFlash;
    public GameObject Ground;

    public List<Sprite> bg;
    public GameObject backGround;

    public GameObject StartPanel;
    public GameObject Spawner;

    public GameObject GameoverPanel;
    public GameObject NewBest;
    public Text ScoreBoardText;
    public Text HighScoreText;
    private int drawScore;

    private bool isGameOver = false;

    public State state;
    public enum State
    {
        Start,
        Playing,
        Dead
    }
    private void Awake()
    {
        Application.targetFrameRate = 120;
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this);
    }
    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        backGround.GetComponent<SpriteRenderer>().sprite = bg[Random.Range(0, bg.Count)];
        state = State.Start;
    }
    void Update()
    {
        switch (state)
        {
            case State.Start:
                isGameOver = false;
                StartPanel.SetActive(true);
                scoreText.enabled = true;
                GameoverPanel.SetActive(false);
                Spawner.SetActive(false);
                NewBest.SetActive(false);
                WhiteFlash.SetActive(false);
                score = 0;
                drawScore = 0;
                scoreText.text = score.ToString();
                break;
            case State.Playing:
                isGameOver = false;
                if (StartPanel.activeInHierarchy)
                {
                    StartPanel.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Fade");
                    StartPanel.transform.GetChild(1).GetComponent<Animator>().SetTrigger("Fade");
                }
                scoreText.enabled = true;
                GameoverPanel.SetActive(false);
                Spawner.SetActive(true);
                NewBest.SetActive(false);
                WhiteFlash.SetActive(false);
                scoreText.text = score.ToString();
                break;
            case State.Dead:
                WhiteFlash.SetActive(true);
                StartPanel.SetActive(false);
                scoreText.enabled = false;
                GameoverPanel.SetActive(true);
                if (isGameOver == false)
                {
                    Invoke(nameof(ShowGameOvertext), 0.7f);
                    isGameOver = true;
                }
                Spawner.SetActive(true);
                if (score > highScore)
                {
                    PlayerPrefs.SetInt("HighScore", score);
                    HighScoreText.text = score.ToString();
                    NewBest.SetActive(true);
                }
                else
                {
                    HighScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
                    NewBest.SetActive(false);
                }
                break;
        }
    }
    public IEnumerator DrawScore()
    {
        float seconds;
        if (score < 10)
            seconds = 0.25f;
        else if (score < 20)
            seconds = 0.2f;
        else if (score < 30)
            seconds = 0.15f;
        else if (score < 40)
            seconds = 0.1f;
        else
            seconds = 0.05f;
        while (drawScore <= score)
        {
            ScoreBoardText.text = drawScore.ToString();
            ++drawScore;
            yield return new WaitForSeconds(seconds);
        }
    }

    private void ShowGameOvertext()
    {
        GameoverPanel.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void ShowScoreBoard()
    {
        GameoverPanel.transform.GetChild(1).gameObject.SetActive(true);
    }
    public void ShowButtons()
    {
        GameoverPanel.transform.GetChild(2).gameObject.SetActive(true);
    }
    public void DisableStartPage()
    {
        StartPanel.SetActive(false);
    }
}
