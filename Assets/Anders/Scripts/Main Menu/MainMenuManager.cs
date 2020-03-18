using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject[] MainButtons = new GameObject[3];
    public GameObject[] HeroSelectionUI = new GameObject[3];
    public GameObject[] HeroButtons = new GameObject[4];

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
                switch (CurrentButtonIndex)
                {
                    case 0:
                        Debug.Log("Hero" + CurrentButtonIndex);

                        break;

                    case 1:
                        Debug.Log("Hero" + CurrentButtonIndex);

                        break;

                    case 2:
                        Debug.Log("Hero" + CurrentButtonIndex);

                        break;

                    case 3:
                        Debug.Log("Hero" + CurrentButtonIndex);

                        break;
                }
                SceneManager.LoadScene("MainGame");    
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
