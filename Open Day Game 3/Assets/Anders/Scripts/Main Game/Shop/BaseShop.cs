using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class BaseShop : MonoBehaviour
{
    public BaseItem Item;

    public List<GameObject> ShopItems = new List<GameObject>();

    public GameObject Player;
    public GameObject RecommendShop, WeaponShop, CrystalShop, DefenceShop, MiscShop;
    public GameObject[] Buttons;

    public string IDBuilder;
    public string Show;

    public int OnTier, NumberOfItemsInTier;

    public int SelectionState, CurrentClassIndex, CurrentItemIndex;

    public bool PressedUD,PressedLR;

    public GameObject HoverIndicator;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        OnTier = 1;
        SelectionState = -1;
        Buttons[0].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
        Buttons[1].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
        Buttons[2].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
        Buttons[3].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
        Buttons[4].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("joystick button 1"))
        {
            CurrentItemIndex = 0;

            if (SelectionState == -1)
            {
                ExitShop();
            }

            SelectionState = -1;
            RecommendShop.SetActive(false);
            WeaponShop.SetActive(false);
            CrystalShop.SetActive(false);
            DefenceShop.SetActive(false);
            MiscShop.SetActive(false);
        }

        if(SelectionState == -1)
        {
            if (Input.GetAxis("C1DpadUD") > 0 && !PressedUD)
            {
                PressedUD = true;
                CurrentClassIndex--;
                if (CurrentClassIndex < 0)
                {
                    CurrentClassIndex = 4;
                }
            }
            if (Input.GetAxis("C1DpadUD") < 0 && !PressedUD)
            {
                PressedUD = true;
                CurrentClassIndex++;
                if (CurrentClassIndex > 4)
                {
                    CurrentClassIndex = 0;
                }
            }
            if (Input.GetAxis("C1DpadUD") == 0)
            {
                PressedUD = false;
            }
            if (Input.GetKeyDown("joystick button 0"))
            {
                SelectionState = CurrentClassIndex;
                CurrentItemIndex = 0;
                ClassSelected(SelectionState);
            }

            UpdateShopClassUI();
        }
        
        if(SelectionState > -1)
        {
            if(Input.GetKeyDown("joystick button 0"))
            {
                foreach(GameObject Item in ShopItems)
                {
                    if(Item.GetComponent<ButtonData>().ThisItem.ID == IDBuilder)
                    {
                        Item.GetComponent<ButtonData>().Selected();
                    }
                }
            }
            LRDpad();
            UDDpad();
            UpdateShopItemUI();
        }
    }

    public void ClassSelected(int Option)
    {
        RecommendShop.SetActive(false);
        WeaponShop.SetActive(false);
        CrystalShop.SetActive(false);
        DefenceShop.SetActive(false);
        MiscShop.SetActive(false);

        switch (Option)
        {
            case 0:



                break;

            case 1:

                WeaponShop.SetActive(true);

                break;

            case 2:



                break;

            case 3:



                break;

            case 4:



                break;
        }
    }

    void ExitShop()
    {
        RecommendShop.SetActive(false);
        WeaponShop.SetActive(false);
        CrystalShop.SetActive(false);
        DefenceShop.SetActive(false);
        MiscShop.SetActive(false);

        this.gameObject.SetActive(false);
    }

    void UpdateShopClassUI()
    {
        switch (CurrentClassIndex)
        {
            case 0:

                Buttons[0].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                Buttons[1].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                Buttons[4].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);

                break;

            case 1:

                Buttons[0].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                Buttons[1].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                Buttons[2].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);

                break;

            case 2:

                Buttons[1].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                Buttons[2].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                Buttons[3].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);

                break;

            case 3:

                Buttons[2].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                Buttons[3].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                Buttons[4].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);


                break;

            case 4:

                Buttons[3].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                Buttons[4].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                Buttons[0].GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);

                break;
        }
    }

    void UpdateShopItemUI()
    {
        IDBuilder = null;

        IDBuilder = OnTier.ToString() + CurrentItemIndex.ToString();

        foreach(GameObject Item in ShopItems)
        {
            Debug.Log(Item.GetComponent<ButtonData>().ThisItem.ID);
            if (Item.GetComponent<ButtonData>().ThisItem.ID == IDBuilder)
            {
                Debug.Log("Match");
                if (GameObject.FindGameObjectWithTag("HoverIndicator"))
                {
                    Destroy(GameObject.FindGameObjectWithTag("HoverIndicator").gameObject);
                }
                Instantiate(HoverIndicator, Item.transform);
            }
        }
    }

    void LoadValues()
    {
        switch (OnTier)
        {
            case 1:

                NumberOfItemsInTier = 4;

                if(CurrentItemIndex >= NumberOfItemsInTier)
                {
                    CurrentItemIndex = 3;
                }

                break;

            case 2:

                NumberOfItemsInTier = 6;

                if (CurrentItemIndex >= NumberOfItemsInTier)
                {
                    CurrentItemIndex = 5;
                }

                break;

            case 3:

                NumberOfItemsInTier = 7;

                break;
        }
    }

    void UDDpad()
    {
        if (Input.GetAxis("C1DpadUD") > 0 && !PressedUD)
        {
            PressedUD = true;
            CurrentItemIndex--;
            if (CurrentItemIndex < 0)
            {
                CurrentItemIndex = NumberOfItemsInTier - 1;
            }
        }
        if (Input.GetAxis("C1DpadUD") < 0 && !PressedUD)
        {
            PressedUD = true;
            CurrentItemIndex++;
            if (CurrentItemIndex > NumberOfItemsInTier - 1)
            {
                CurrentItemIndex = 0;
            }
        }
        if (Input.GetAxis("C1DpadUD") == 0)
        {
            PressedUD = false;
        }
    }

    void LRDpad()
    {
        if (Input.GetAxis("C1DpadLR") > 0 && !PressedLR)
        {
            PressedLR = true;
            OnTier++;
            if (OnTier > 3)
            {
                OnTier = 1;
            }
        }
        if (Input.GetAxis("C1DpadLR") < 0 && !PressedLR)
        {
            PressedLR = true;
            OnTier--;
            if (OnTier < 1)
            {
                OnTier = 3;
            }
        }
        if (Input.GetAxis("C1DpadLR") == 0)
        {
            LoadValues();
            PressedLR = false;
        }
    }
}
