using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameOverScoreLoad : MonoBehaviour
{

    public TextMeshProUGUI Scoretext;
    public TextMeshProUGUI highscoreText;

    private void Awake()
    {
        highscoreText.text = "High score : " + PlayerPrefs.GetFloat("Highscore").ToString();
        Scoretext.text = "Score : " + PlayerPrefs.GetFloat("Score").ToString();
    }

}
