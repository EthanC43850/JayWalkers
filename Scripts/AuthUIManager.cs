using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuthUIManager : MonoBehaviour
{
    public static AuthUIManager instance;

    //Screen object variables
    public GameObject loginUI;
    public GameObject registerUI;

    [Header("LeaderBoard References")]
    public Text usernameText;
    public Text highscoreText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    //Functions to change the login screen UI
    public void LoginScreen() //Back button
    {
        loginUI.SetActive(true);
        registerUI.SetActive(false);
    }
    public void RegisterScreen() // Regester button
    {
        loginUI.SetActive(false);
        registerUI.SetActive(true);
    }

    public void SetLeaderboardUltimateJaywalker(string _username, int _highScore)
    {


        usernameText.text = _username;
        highscoreText.text = _highScore.ToString();

    }
}
