using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    [SerializeField] public GameManager gm;
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public TextMeshProUGUI scoreTextEnd;
    [SerializeField] public TextMeshProUGUI highScoreText;

    [SerializeField] private float scoreVal;
    private float speed;
    private int scoreAsInt;
    private float highScore;

    private void Start()
    {
        scoreVal = 0;
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "High Score: " + highScore;
    }

    void FixedUpdate()
    {
        //get speed value from gm
        speed = gm.getCurrSpeed();

        //calculate score increase and cast to int
        scoreVal += speed;
        scoreAsInt = (int)scoreVal;

        //display scoreAsInt in UI
        scoreText.text = scoreAsInt.ToString();
        scoreTextEnd.text = "Score: " + scoreAsInt.ToString();
        
        if (scoreVal > highScore)
        {
            PlayerPrefs.SetInt("HighScore", scoreAsInt);
            highScoreText.text = "High Score: " + scoreAsInt;
        }
    }
}
