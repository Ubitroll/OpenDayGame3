using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderboardHandler : MonoBehaviour
{
    public Text[] HighScoreSlots = new Text[6];

    public List<string> PlayerNames = new List<string>();
    public List<int> PlayerScores = new List<int>();

    public GameObject WonEffects, LostEffects, WinPanel, LostPanel, LeaderboardPanel;

    public Text Score, Condition;

    public bool HasPressed;

    public int CurrentPlayerScoreTemp;
    public string CurrentPlayerNameTemp;

    public int State; //0 = Victory/Lose Screen //1 = Leaderboard.

    // Start is called before the first frame update
    void Start()
    { 
        State = 0;
        LoadLeaderboardValues();
        LoadLeaderboard();

        if (DataTransferer.Instance.HasWon)
        {
            WonEffects.SetActive(true);
            WinPanel.SetActive(true);
        }
        else
        {
            LostEffects.SetActive(true);
            LostPanel.SetActive(true);
        }

        Score.text = "Score : " + DataTransferer.Instance.CurrentPlayerScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("joystick button 0") && HasPressed == false)
        {
            HasPressed = true;
            Continue();
        }
        if (Input.GetKeyUp("joystick button 0"))
        {
            HasPressed = false;
        }
    }

    void LoadLeaderboard()
    {
        for (int i = 0; i < PlayerNames.Count; i++)
        {
            HighScoreSlots[i].text = "Score : " + PlayerScores[i].ToString() + " " + PlayerNames[i];
        }
    }
    void SortHighscore()
    {
        int PlayerScore = DataTransferer.Instance.CurrentPlayerScore; 
        string PlayerName = DataTransferer.Instance.CurrentPlayerName;

        for (int i = 0; i < PlayerScores.Count; i++)
        {
            Debug.Log(i);
            if (PlayerScore > PlayerScores[i])
            {
                Debug.Log("New Highscore!");

                for(int x = 5; x > i ; x--)
                {
                    Debug.Log(x);
                    PlayerScores[x] = PlayerScores[x - 1];
                    PlayerNames[x] = PlayerNames[x - 1];
                }

                PlayerScores[i] = PlayerScore;
                PlayerNames[i] = PlayerName;

                break;
            }
        }

        for(int i = 0; i < PlayerNames.Count; i++)
        {
            HighScoreSlots[i].text = PlayerNames[i] + " : " + PlayerScores[i].ToString();
        }
    }
    void Continue()
    {
        if (!HasPressed)
        {
            HasPressed = true;

            if (State == 0)
            {
                State++;
            }
            if (State == 1)
            {
                LeaderboardPanel.SetActive(true);
                State++;
            }
            if (State == 2)
            {
                SaveLeaderboard();

                DataTransferer.Instance.CurrentPlayerScore = 0; //Resetting the Data that is being transferred. 
                DataTransferer.Instance.CurrentPlayerName = "";
                DataTransferer.Instance.PlayerChoice = null;
                DataTransferer.Instance.AI_1_Choice = null;

                SceneManager.LoadScene("MainMenu"); //Looping game by heading to main menu.
            }
        }   
    }
    void LoadLeaderboardValues()
    {
        for(int i = 0; i < PlayerScores.Count; i++)
        {
            PlayerPrefs.GetInt("PlayerScore" + i, PlayerScores[i]);
            PlayerPrefs.GetString("PlayerName" + i, PlayerNames[i]);
        }
    }
    void SaveLeaderboard()
    {
        for(int i = 0; i < PlayerScores.Count; i++)
        {
            PlayerPrefs.SetInt("PlayerScore" + i, PlayerScores[i]);
            PlayerPrefs.SetString("PlayerName" + i, PlayerNames[i]);
        }
    }
}
