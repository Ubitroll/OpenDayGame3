using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject InformationForPanelMeeting;

    public GameObject[] MainButtons = new GameObject[3];
    public GameObject[] HeroSelectionUI = new GameObject[3];
    public GameObject[] SelectedHeroUI = new GameObject[3];
    public GameObject[] HeroButtons = new GameObject[4];

    public List<GameObject> CharactersNotSelected = new List<GameObject>(4);

    public float PickTimer;
    public bool CountToStart;
    public Text Timer;

    public int CurrentButtonIndex;

    [SerializeField]
    private bool HasClickedMain, LoadHeroSelection, Pressed;

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
        yButton = "C" +  "Y";
        dPadUpDown = "C" + "DpadUD";
        dPadLeftRight = "C" + "DpadLR";
        select = "C" + "Select";
        pause = "C" + "Pause";
    }

    // Update is called once per frame
    void Update()
    {
        if (LoadHeroSelection)
        {
            if(CountToStart != true)
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
                if(PickTimer <= 0)
                {
                    SceneManager.LoadScene("MainGame");
                }
            }
        }        

        #region UI Handlers

        if (HasClickedMain)
        {
            if(MainButtons[0].transform.localPosition.x < -165)
            {
                MainButtons[0].transform.Translate(20, 0, 0);
                if(MainButtons[0].transform.localPosition.x > -165)
                {
                    MainButtons[0].transform.localPosition = new Vector3(-165, 1200, 0);
                }
            }
            if (MainButtons[1].transform.localPosition.x > -780)
            {
                MainButtons[1].transform.Translate(-20, 0, 0);
                if (MainButtons[1].transform.localPosition.x < -780)
                {
                    MainButtons[1].transform.localPosition = new Vector3(-780, -1380, 0);
                }
            }
            if (MainButtons[2].transform.localPosition.x < 1565)
            {
                MainButtons[2].transform.Translate(20, 0, 0);
                if (MainButtons[2].transform.localPosition.x > 1565)
                {
                    MainButtons[2].transform.localPosition = new Vector3(1565, 1100, 0);
                }
            }
        }

        if (LoadHeroSelection)
        {

            InformationForPanelMeeting.SetActive(true);

            #region UI Handler
            if (HeroSelectionUI[0].transform.localPosition.x < -720)
            {
                HeroSelectionUI[0].transform.Translate(20, 0, 0);
                if(HeroSelectionUI[0].transform.localPosition.x > -720)
                {
                    HeroSelectionUI[0].transform.localPosition = new Vector3(-720, 0, 0);
                }
            }

            if (HeroSelectionUI[1].transform.localPosition.y > 340)
            {
                HeroSelectionUI[1].transform.Translate(0, -20, 0);
                if (HeroSelectionUI[1].transform.localPosition.y < 340)
                {
                    HeroSelectionUI[1].transform.localPosition = new Vector3(70, 340, 0);
                }
            }

            if (HeroSelectionUI[2].transform.localPosition.x > 895)
            {
                HeroSelectionUI[2].transform.Translate(20, 0, 0);
                if (HeroSelectionUI[2].transform.localPosition.x < 895)
                {
                    HeroSelectionUI[2].transform.localPosition = new Vector3(895, 0, 0);
                }
            }

            #endregion

            if (Input.GetAxis("C1DpadUD") > 0 && !Pressed)
            {
                Pressed = true;
                CurrentButtonIndex--;
                if (CurrentButtonIndex < 0)
                {
                    CurrentButtonIndex = 3;
                }
            }
            if (Input.GetAxis("C1DpadUD") < 0 && !Pressed)
            {
                Pressed = true;
                CurrentButtonIndex++;
                if (CurrentButtonIndex > 3)
                {
                    CurrentButtonIndex = 0;
                }
            }
            if (Input.GetAxis("C1DpadUD") == 0)
            {
                Pressed = false;
            }
            if (Input.GetKeyDown("joystick button 0"))
            {
                DataTransferer.Instance.PlayerChoice = CharactersNotSelected[CurrentButtonIndex];

                CharactersNotSelected.RemoveAt(CurrentButtonIndex);

                for (int i = 0; i < CharactersNotSelected.Count; i++)
                {
                    Debug.Log(i);
                    if (DataTransferer.Instance.AI_1_Choice == null)
                    {
                        int RandomChoice = Random.Range(0, CharactersNotSelected.Count);
                        DataTransferer.Instance.AI_1_Choice = CharactersNotSelected[RandomChoice];
                        CharactersNotSelected.RemoveAt(RandomChoice);
                        i = 0;
                    }
                    //if (DataTransferer.Instance.AI_2_Choice == null)
                    //{
                    //    int RandomChoice = Random.Range(0, CharactersNotSelected.Count);
                    //    DataTransferer.Instance.AI_2_Choice = CharactersNotSelected[RandomChoice];
                    //    CharactersNotSelected.RemoveAt(RandomChoice);
                    //    i = 0;
                    //}
                    //if (DataTransferer.Instance.AI_3_Choice == null)
                    //{
                    //    int RandomChoice = Random.Range(0, CharactersNotSelected.Count);
                    //    DataTransferer.Instance.AI_3_Choice = CharactersNotSelected[RandomChoice];
                    //    CharactersNotSelected.RemoveAt(RandomChoice);
                    //    i = 0;
                    //}
                }

                InitializeGame();    
            }

            #region ButtonHandler

            switch (CurrentButtonIndex)
            {
                case 0:

                    HeroButtons[0].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    HeroButtons[1].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                    HeroButtons[2].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                    HeroButtons[3].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);

                    break;

                case 1:

                    HeroButtons[0].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                    HeroButtons[1].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    HeroButtons[2].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                    HeroButtons[3].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);

                    break;

                case 2:

                    HeroButtons[0].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                    HeroButtons[1].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                    HeroButtons[2].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    HeroButtons[3].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);

                    break;

                case 3:

                    HeroButtons[0].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                    HeroButtons[1].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                    HeroButtons[2].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                    HeroButtons[3].GetComponent<Image>().color = new Color(1, 1, 1, 1);

                    break;
            }

            #endregion
        }

        if (!HasClickedMain)
        {
            if (Input.GetAxis("C1DpadLR") > 0 && !Pressed)
            {
                Pressed = true;
                CurrentButtonIndex++;
                if (CurrentButtonIndex > 1)
                {
                    CurrentButtonIndex = -1;
                }
            }
            if (Input.GetAxis("C1DpadLR") < 0 && !Pressed)
            {
                Pressed = true;
                CurrentButtonIndex--;
                if (CurrentButtonIndex < -1)
                {
                    CurrentButtonIndex = 1;
                }
            }
            if (Input.GetAxis("C1DpadLR") == 0)
            {
                Pressed = false;
            }
            if (Input.GetKeyDown("joystick button 0"))
            {
                HasClickedMain = true;

                switch (CurrentButtonIndex)
                {
                    case -1:

                        break;

                    case 0:

                        LoadHeroSelection = true;
                        CurrentButtonIndex = 0;

                        break;

                    case 1:

                        break;
                }
            }
        }

        #region ButtonHandler

        switch (CurrentButtonIndex)
        {
            case -1:

                MainButtons[0].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                MainButtons[1].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                MainButtons[2].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);

                break;

            case 0:

                MainButtons[0].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                MainButtons[1].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                MainButtons[2].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);

                break;

            case 1:

                MainButtons[0].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                MainButtons[1].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                MainButtons[2].GetComponent<Image>().color = new Color(1, 1, 1, 1);

                break;
        }

        #endregion

        #endregion
    }

    void InitializeGame()
    {
        CountToStart = true;
        PickTimer = 3;

        SelectedHeroUI[0].GetComponent<Image>().color = new Color(1, 1, 1, 1);
        SelectedHeroUI[0].GetComponent<Image>().sprite = DataTransferer.Instance.PlayerChoice.GetComponent<BaseCharacter>().CharacterIcon;
        //SelectedHeroUI[1].GetComponent<Image>().color = new Color(1, 1, 1, 1);
        //SelectedHeroUI[1].GetComponent<Image>().sprite = DataTransferer.Instance.AI_2_Choice.GetComponent<BaseCharacter>().CharacterIcon;
        SelectedHeroUI[2].GetComponent<Image>().color = new Color(1, 1, 1, 1);
        SelectedHeroUI[2].GetComponent<Image>().sprite = DataTransferer.Instance.AI_1_Choice.GetComponent<BaseCharacter>().CharacterIcon;
        //SelectedHeroUI[3].GetComponent<Image>().color = new Color(1, 1, 1, 1);
        //SelectedHeroUI[3].GetComponent<Image>().sprite = DataTransferer.Instance.AI_3_Choice.GetComponent<BaseCharacter>().CharacterIcon;

        SetPlayer();
    }

    void SetPlayer()
    {
        DataTransferer.Instance.PlayerChoice.GetComponent<BaseCharacter>().enabled = false;
        DataTransferer.Instance.PlayerChoice.GetComponent<PlayerInput>().enabled = true;
        DataTransferer.Instance.PlayerChoice.tag = "Player";


        DataTransferer.Instance.AI_1_Choice.GetComponent<BaseCharacter>().enabled = true; //Ensures if anything is saved when player uses it then it will revert back to default.
        DataTransferer.Instance.AI_1_Choice.GetComponent<PlayerInput>().enabled = false;
        DataTransferer.Instance.AI_1_Choice.tag = "Character";
    }

    void RandomPick()
    {
        for (int i = 0; i < CharactersNotSelected.Count; i++)
        {
            if (DataTransferer.Instance.PlayerChoice == null)
            {
                int RandomChoice = Random.Range(0, CharactersNotSelected.Count);
                DataTransferer.Instance.PlayerChoice = CharactersNotSelected[RandomChoice];
                CharactersNotSelected.RemoveAt(RandomChoice);
                i = 0;
            }
            if (DataTransferer.Instance.AI_1_Choice == null)
            {
                int RandomChoice = Random.Range(0, CharactersNotSelected.Count);
                DataTransferer.Instance.AI_1_Choice = CharactersNotSelected[RandomChoice];
                CharactersNotSelected.RemoveAt(RandomChoice);
                i = 0;
            }
            //if (DataTransferer.Instance.AI_2_Choice == null) //Scalability here!
            //{
            //    int RandomChoice = Random.Range(0, CharactersNotSelected.Count);
            //    DataTransferer.Instance.AI_2_Choice = CharactersNotSelected[RandomChoice];
            //    CharactersNotSelected.RemoveAt(RandomChoice);
            //    i = 0;
            //}
            //if (DataTransferer.Instance.AI_3_Choice == null)
            //{
            //    int RandomChoice = Random.Range(0, CharactersNotSelected.Count);
            //    DataTransferer.Instance.AI_3_Choice = CharactersNotSelected[RandomChoice];
            //    CharactersNotSelected.RemoveAt(RandomChoice);
            //    i = 0;
            //}
        }
    }
    public void Clicked(string Option)
    {
        HasClickedMain = true;

        switch (Option)
        {
            case "Play":

                LoadHeroSelection = true;

                break;
        }
    }
}
