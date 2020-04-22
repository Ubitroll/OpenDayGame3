using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject InformationForPanelMeeting;

    public Animator[] MainMenuElements = new Animator[4];
    public Animator[] HeroSelectionElements = new Animator[3];

    public GameObject[] MainButtons = new GameObject[3];
    public GameObject HeroSelectionUI, MainMenuUI;

    public GameObject[] HeroesAsPlayer = new GameObject[3];
    public GameObject[] HeroesAsAI = new GameObject[3];

    public GameObject PlayerHeroIcon, EnemyHeroIcon;

    public GameObject[] HeroIcons = new GameObject[3];

    //public List<GameObject> CharactersNotSelected = new List<GameObject>(4);

    public float PickTimer;
    public bool CountToStart;
    public Text Timer;

    public int AISelection;

    public int CurrentButtonIndex;

    [SerializeField]
    private bool HasClickedMain, LoadHeroSelection, Pressed, PressA;

    private string moveX;
    private string moveY;
    private string horizontal;
    private string vertical;
    private string aButton;
    private string bButton;
    private string xButton;
    private string yButton;
    private string dPadUpDown;
    private string dPadLeftRight;
    private string select;
    private string pause;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;
    }
    // Start is called before the first frame update
    void Start()
    {
        PickTimer = 60;
        moveX = "C" + "moveX";
        moveY = "C" + "moveY";
        horizontal = "C" + "horizontal";
        vertical = "C" + "vertical";
        aButton = "C" + "A";
        bButton = "C" + "B";
        xButton = "C" + "X";
        yButton = "C" + "Y";
        dPadUpDown = "C" + "DpadUD";
        dPadLeftRight = "C" + "DpadLR";
        select = "C" + "Select";
        pause = "C" + "Pause";

        CurrentButtonIndex = 1;
    }

    // Update is called once per frame
    void Update()
    {

        #region SelectionTimer

        if (LoadHeroSelection)
        {
            if (CountToStart != true)
            {
                PickTimer -= Time.fixedDeltaTime;
                string minutes = Mathf.Floor(PickTimer / 60).ToString("00");
                string seconds = (PickTimer % 60).ToString("00");

                Timer.text = (string.Format("{0}:{1}", minutes, seconds));
                if (PickTimer <= 0)
                {
                    CountToStart = true;
                    RandomPick();
                    InitializeGame();
                    PickTimer = 3;
                }
            }
            else
            {
                PickTimer -= Time.fixedDeltaTime;
                Timer.text = ((int)PickTimer).ToString();
                if (PickTimer <= 0)
                {
                    SceneManager.LoadScene("Map Prototype v2");
                }
            }
        }

        #endregion

        #region MainOptionsHandler

        if (!HasClickedMain)
        {
            if (Input.GetAxis("C1DpadLR") > 0 && !Pressed)
            {
                Pressed = true;
                CurrentButtonIndex++;
                if (CurrentButtonIndex > 2)
                {
                    CurrentButtonIndex = 0;
                }
            }
            if (Input.GetAxis("C1DpadLR") < 0 && !Pressed)
            {
                Pressed = true;
                CurrentButtonIndex--;
                if (CurrentButtonIndex < 0)
                {
                    CurrentButtonIndex = 2;
                }
            }
            if (Input.GetAxis("C1DpadLR") == 0)
            {
                Pressed = false;
            }
            if (Input.GetKeyDown("joystick button 0") && !PressA)
            {
                PressA = true;

                HasClickedMain = true;

                for (int i = 0; i < 4; i++)
                {
                    MainMenuElements[i].SetBool("IsClicked", true);
                    Debug.Log(i);
                }

                switch (CurrentButtonIndex)
                {
                    case 0:

                        break;

                    case 1:

                        Debug.Log("Working");

                        LoadHeroSelection = true;

                        LoadHeroSelectionUI();

                        break;

                    case 2:

                        break;
                }
            }

            if (!Input.GetKeyDown("joystick button 0"))
            {
                PressA = false;
            }
        }

        #region ButtonHandler

        switch (CurrentButtonIndex)
        {
            case 0:

                MainButtons[0].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                MainButtons[1].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                MainButtons[2].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);

                break;

            case 1:

                MainButtons[0].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                MainButtons[1].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                MainButtons[2].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);

                break;

            case 2:

                MainButtons[0].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                MainButtons[1].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                MainButtons[2].GetComponent<Image>().color = new Color(1, 1, 1, 1);

                break;
        }

        #endregion

        #endregion

        #region HeroSelectionHandler

        if (LoadHeroSelection)
        {
            if (Input.GetAxis("C1DpadUD") > 0 && !Pressed)
            {
                Pressed = true;
                CurrentButtonIndex--;
                if (CurrentButtonIndex < 0)
                {
                    CurrentButtonIndex = 2;
                }
            }
            if (Input.GetAxis("C1DpadUD") < 0 && !Pressed)
            {
                Pressed = true;
                CurrentButtonIndex++;
                if (CurrentButtonIndex > 2)
                {
                    CurrentButtonIndex = 0;
                }
            }
            if (Input.GetAxis("C1DpadUD") == 0)
            {
                Pressed = false;
            }

            for (int i = 0; i < 3; i++)
            {
                if (CurrentButtonIndex == i)
                {
                    HeroIcons[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }
                else
                {
                    HeroIcons[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                }
            }

            if (Input.GetKeyDown("joystick button 0") && !PressA) //Adding the right gameobjects to the data transferer.
            {
                PressA = true;

                DataTransferer.Instance.PlayerChoice = HeroesAsPlayer[CurrentButtonIndex];
                AISelection = Random.Range(0, 3);
                DataTransferer.Instance.AI_1_Choice = HeroesAsAI[AISelection];

                InitializeGame();
            }

            if (!Input.GetKeyDown("joystick button 0"))
            {
                PressA = false;
            }

        }

            #endregion
       
        void LoadHeroSelectionUI()
        {
            HeroSelectionUI.SetActive(true);

            for (int i = 0; i < HeroSelectionElements.Length; i++)
            {
                HeroSelectionElements[i].SetBool("LoadHeroSelection", true);
            }
        }

        void InitializeGame()
        {
            CountToStart = true;
            PickTimer = 3;

            PlayerHeroIcon.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            PlayerHeroIcon.GetComponent<Image>().sprite = HeroIcons[CurrentButtonIndex].GetComponent<Image>().sprite;
            EnemyHeroIcon.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            EnemyHeroIcon.GetComponent<Image>().sprite = HeroIcons[AISelection].GetComponent<Image>().sprite;
        }

        void RandomPick() //Adding the right gameobjects to the data transferer.
        {
            DataTransferer.Instance.PlayerChoice = HeroesAsPlayer[Random.Range(0, 2)];
            AISelection = Random.Range(0, 3);
            DataTransferer.Instance.AI_1_Choice = HeroesAsAI[AISelection];
        }
    }
}
