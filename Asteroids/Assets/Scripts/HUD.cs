using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    Text ScoreText;
    [SerializeField]
    Text TimerText;
    [SerializeField]
    Text HighScoreText;

    bool running = true;

    int score;
    int highScore;
    float elapsedTime;
    const string ScorePrefix = "Score : ";
    const string TimerPrefix = "Time : ";
    const string HighScorePrefix = "High Score: ";

    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = ScorePrefix + score.ToString();

        string previousHigh = PlayerPrefs.GetString("HighScore");

        if (string.IsNullOrEmpty(previousHigh))
        {
            previousHigh = "0";
        }

        HighScoreText.text = HighScorePrefix + previousHigh.ToString();

        highScore = int.Parse(previousHigh);
    }

    public void AddPoints(int points)
    {
        score += points;
        ScoreText.text = ScorePrefix + score.ToString();
    }

    public void StopGameTimer()
    {
        running = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            highScore = 0;
            PlayerPrefs.SetString("HighScore", highScore.ToString());
            PlayerPrefs.Save();

            string previousHigh = PlayerPrefs.GetString("HighScore");

            if (string.IsNullOrEmpty(previousHigh))
            {
                previousHigh = "0";
            }

            HighScoreText.text = HighScorePrefix + previousHigh.ToString();
        }


        if (running == true)
        {
            elapsedTime += Time.deltaTime;
            int display = (int)elapsedTime;
            TimerText.text = TimerPrefix + display.ToString();

            if (highScore < score)
            {
                highScore = score;
                PlayerPrefs.SetString("HighScore", highScore.ToString());
                PlayerPrefs.Save();

                string previousHigh = PlayerPrefs.GetString("HighScore");

                if (string.IsNullOrEmpty(previousHigh))
                {
                    previousHigh = "0";
                }

                HighScoreText.text = HighScorePrefix + previousHigh.ToString();
            }
        }

        if (running == false)
        {
            if (highScore < score)
            {
                highScore = score;
                PlayerPrefs.SetString("HighScore", highScore.ToString());
                PlayerPrefs.Save();

                string previousHigh = PlayerPrefs.GetString("HighScore");

                if (string.IsNullOrEmpty(previousHigh))
                {
                    previousHigh = "0";
                }

                HighScoreText.text = HighScorePrefix + previousHigh.ToString();
            }
        }

    }
}

