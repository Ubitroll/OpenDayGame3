using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public GameObject Player;

    public Text Gold, Timer, EnemyPoints, AllyPoints;

    public float GameTimer;

    public bool IsPlaying;

    // Start is called before the first frame update
    void Start()
    {
        GameTimer = 300;
        IsPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlaying)
        {
            GameTimer -= Time.fixedDeltaTime;
        }

        Gold.text = Player.GetComponent<Player>().Gold.ToString();

        string minutes = Mathf.Floor(GameTimer / 60).ToString("00");
        string seconds = (GameTimer % 60).ToString("00");

        Timer.text = (string.Format("{0}:{1}", minutes, seconds));
    }
}
