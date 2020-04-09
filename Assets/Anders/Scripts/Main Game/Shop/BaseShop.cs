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
    public GameObject CurrentIndicator;
    public bool HasHover;

    #region Side Panel Vars

    public GameObject SidePanel;
    public Text ItemName, ItemDescription, ItemCost;
    public Image ItemIcon;

    #endregion

    #region Inventory in Shop Vars

    public int CurrentItemSlot;
    public List<Image> Inventory = new List<Image>();
    public List<Image> BackgroundSlot = new List<Image>();

    #endregion

    #region Singleton

    private static BaseShop _instance;

    public static BaseShop Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        OnTier = 1;
        SelectionState = -1;
        HasHover = false;
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


            Destroy(GameObject.FindGameObjectWithTag("HoverIndicator"));
            HasHover = false;

            SidePanel.SetActive(false);
            ResetItemPanel();

            RecommendShop.SetActive(false);
            WeaponShop.SetActive(false);
            CrystalShop.SetActive(false);
            DefenceShop.SetActive(false);
            MiscShop.SetActive(false);
        }

        if (SelectionState > -1)
        {
            if(CurrentClassIndex != 0)
            {
                if (Input.GetKeyDown("joystick button 0"))
                {
                    foreach (GameObject Item in ShopItems)
                    {
                        if (Item.GetComponent<ButtonData>().ThisItem.ID == IDBuilder)
                        {
                            Item.GetComponent<ButtonData>().Selected();
                        }
                    }
                }

                LRDpad();
                UDDpad();
            }
            else
            {
                if (Input.GetKeyDown("joystick button 0"))
                {
                    Player.GetComponent<Player>().Gold += PlayerInventory.Instance.Inventory[CurrentItemSlot].Cost / 2;
                    PlayerInventory.Instance.Inventory.RemoveAt(CurrentItemSlot);
                    LoadInventory();
                }

                Inventory_LRDpad();
            }
        }

        if (SelectionState == -1)
        {
            GameObject[] Find = GameObject.FindGameObjectsWithTag("HoverIndicator");
            foreach (GameObject Object in Find)
            {
                Destroy(Object);
            }

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
                SelectionState = 1;
                CurrentItemIndex = 0;
                ClassSelected(CurrentClassIndex);
            }

            UpdateShopClassUI();
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

                RecommendShop.SetActive(true);
                SidePanel.SetActive(true);
                LoadInventory();
                HoverOverItem();

                break;

            case 1:

                WeaponShop.SetActive(true);
                SidePanel.SetActive(true);

                break;

            case 2:

                CrystalShop.SetActive(true);
                SidePanel.SetActive(true);

                break;

            case 3:



                break;

            case 4:



                break;
        }

        LoadValues();
    }

    void ExitShop()
    {
        RecommendShop.SetActive(false);
        WeaponShop.SetActive(false);
        CrystalShop.SetActive(false);
        DefenceShop.SetActive(false);
        MiscShop.SetActive(false);

        this.gameObject.SetActive(false);

        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<PlayerInput>().enabled = true;
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
        Debug.Log("Firing");
        IDBuilder = null;

        IDBuilder = OnTier.ToString() + CurrentItemIndex.ToString() + CurrentClassIndex.ToString();

        foreach(GameObject Item in ShopItems)
        {
            if (Item.GetComponent<ButtonData>().ThisItem.ID == IDBuilder)
            {
                if(HasHover == false)
                {
                    HasHover = true;
                    GameObject Instance = Instantiate(HoverIndicator, Item.transform) as GameObject;
                    CurrentIndicator = Instance;
                    SetItemPanel(Item.GetComponent<ButtonData>().ThisItem);
                }
                else
                {
                    CurrentIndicator.transform.position = Item.transform.position;
                    SetItemPanel(Item.GetComponent<ButtonData>().ThisItem);
                }
            }
        }
    }

    void LoadValues()
    {
        Debug.Log("Loading Values");

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

        UpdateShopItemUI();
    }

    #region Normal D-pad Input Voids
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

        if (PressedUD == true)
        {
            LoadValues();
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

        if(PressedLR == true)
        {
            LoadValues();
        }

        if (Input.GetAxis("C1DpadLR") == 0)
        {
            PressedLR = false;
        }
    }

    #endregion

    #region Inventory Selling Methods

    void LoadInventory()
    {
        for(int i = 0; i < Inventory.Count; i++)
        {
            Inventory[i].sprite = null;
            Inventory[i].color = new Color(1, 1, 1, 0);
        }

        for (int i = 0; i < PlayerInventory.Instance.Inventory.Count; i++)
        {
            Inventory[i].sprite = PlayerInventory.Instance.Inventory[i].ItemIcon;
            Inventory[i].color = new Color(1, 1, 1, 1);
        }
        for (int i = Inventory.Count; i < Inventory.Count; i++)
        {
            Inventory[i].color = new Color(1, 1, 1, 0);
        }

        PlayerInventory.Instance.SortInventory();

        if (PlayerInventory.Instance.Inventory.Count != 0)
        {
            if (PlayerInventory.Instance.Inventory.Count >= CurrentItemSlot)
            {
                SidePanel.SetActive(true);
                SetItemPanel(PlayerInventory.Instance.Inventory[CurrentItemSlot]);
            }
            else
            {
                SidePanel.SetActive(false);
            }
        }
        else
        {
            SidePanel.SetActive(false);
        }
    }
    void Inventory_LRDpad()
    {
        if (Input.GetAxis("C1DpadLR") > 0 && !PressedLR)
        {
            PressedLR = true;
            CurrentItemSlot++;
            if(CurrentItemSlot > 5)
            {
                CurrentItemSlot = 0;
            }

        }
        if (Input.GetAxis("C1DpadLR") < 0 && !PressedLR)
        {
            PressedLR = true;
            CurrentItemSlot--;
            if (CurrentItemSlot < 0)
            {
                CurrentItemSlot = 5;
            }
        }

        if (PressedLR == true)
        {
            HoverOverItem();            
        }

        if(Input.GetAxis("C1DpadLR") == 0)
        {
            PressedLR = false;
        }
    }
    void HoverOverItem()
    {     
        for (int i = 0; i < BackgroundSlot.Count; i++)
        {
            BackgroundSlot[i].color = new Color(1, 1, 1, 0.3f);
        }

        BackgroundSlot[CurrentItemSlot].color = new Color(1, 1, 1, 1);

        if(PlayerInventory.Instance.Inventory.Count != 0)
        {
            if(PlayerInventory.Instance.Inventory.Count >= CurrentItemSlot)
            {
                SetItemPanel(PlayerInventory.Instance.Inventory[CurrentItemSlot]);
            }
        }
    }

    #endregion

    #region SidePanel
    void ResetItemPanel()
    {
        ItemName.text = "";
        ItemDescription.text = "";
        ItemCost.text = "";
        ItemIcon.sprite = null;
        ItemIcon.color = new Color(1, 1, 1, 0);
    }

    void SetItemPanel(BaseItem Item)
    {
        if(CurrentClassIndex != 0)
        {
            ItemName.text = Item.Name;
            ItemDescription.text = Item.Description;
            ItemCost.text = "Cost : " + Item.Cost;
            ItemIcon.sprite = Item.ItemIcon;
            ItemIcon.color = new Color(1, 1, 1, 1);
        }
        else
        {
            ItemName.text = Item.Name;
            ItemDescription.text = Item.Description;
            ItemCost.text = "Price : " + (Item.Cost / 2);
            ItemIcon.sprite = Item.ItemIcon;
            ItemIcon.color = new Color(1, 1, 1, 1);
        }
    }

    #endregion
}
