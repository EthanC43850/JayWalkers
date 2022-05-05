using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardElement : MonoBehaviour
{

    public Text usernameText;
    public Text highscoreText;
    public void NewScoreElement(string _username, int _highScore)
    {


        usernameText.text = _username;
        highscoreText.text = _highScore.ToString();

    }
}
