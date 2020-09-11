using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int score;

    public TextMeshProUGUI Scoretext;

    public TextMeshProUGUI highscoreText;

    public GameObject player;

    float highScore;

    void Awake()
    {
        Scoretext = GetComponent<TextMeshProUGUI>();
        score = 0;

        highscoreText.text = "High Score : " + PlayerPrefs.GetFloat("Highscore").ToString();

        highScore = PlayerPrefs.GetFloat("Highscore");
    }

    // Update is called once per frame
    void Update()
    {
        Scoretext.text = "Kill Score: " + score;


        if (highScore < score)
            PlayerPrefs.SetFloat("Highscore", score);

        if (player.GetComponent<PlayerHealthManager>().currentHealth <= 0)
            PlayerPrefs.SetFloat("Score", score);

    }
}
