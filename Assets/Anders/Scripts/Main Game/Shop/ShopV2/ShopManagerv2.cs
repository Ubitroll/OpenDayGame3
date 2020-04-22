using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManagerv2 : MonoBehaviour
{
    public GameObject ShopDetection;
    public GameObject Player;

    public Image[] MagicDmgItems, MagicDefenceItem, PhysicalDmgItems, PhysicalDefenceItems, InventoryIcons;
    public GameObject[] CategoryCovers;
    public BaseItem[] ItemsInStore;

    public int CurrentItemIndex, MaxItemIndex; //Maxindex is used to ensure when we are going through items that we loop correctly as some sections have more than others.
    public int CurrentCategory; //1 = PhysicalDmg, 2 = MagicDmg, 3 = PhysicalDef and 4 = MagicDmg.
    public int playerNumber = 1;

    public string CurrentItemID;

    public bool TriggerPressed, PressedLR;
    public bool PressedLT, PressedRT;

    public int SellBackCost;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        playerNumber = 1;

        foreach (Image icon in PhysicalDmgItems)
        {
            icon.color = new Color(.5f, .5f, .5f, 1);
        }
        foreach (Image icon in MagicDmgItems)
        {
            icon.color = new Color(.5f, .5f, .5f, 1);
        }
        foreach (Image icon in PhysicalDefenceItems)
        {
            icon.color = new Color(.5f, .5f, .5f, 1);
        }
        foreach (Image icon in MagicDefenceItem)
        {
            icon.color = new Color(.5f, .5f, .5f, 1);
        }

        SetSection();
        SetIcons();
    }
    private void Update()
    {
        #region Inputs

        if (Input.GetKeyDown("joystick button 3"))
        {
            ExitShop();
        }

        if(Input.GetKeyDown("joystick button 0"))
        {
            if(CurrentCategory != 4)
            {
                CurrentItemID = CurrentCategory.ToString() + CurrentItemIndex.ToString();

                for (int i = 0; i < ItemsInStore.Length; i++)
                {
                    Debug.Log("Searching"+i);
                    if (ItemsInStore[i].ID == CurrentItemID)
                    {
                        Debug.Log("Found at : " + i);
                        BuyItem(ItemsInStore[i]);
                        for(int x = 0; x < GetComponent<PlayerInventory>().Inventory.Count; x++)
                        {
                            InventoryIcons[x].color = new Color(1, 1, 1, 1);
                        }
                    }
                }
            }
            if(CurrentCategory == 4) //Sell Function is this.
            {
                if(GetComponent<PlayerInventory>().Inventory.Count != 0)
                {
                    Player.GetComponent<PlayerInput>().Gold += GetComponent<PlayerInventory>().Inventory[CurrentItemIndex].Cost / SellBackCost;
                    GetComponent<PlayerInventory>().Inventory.RemoveAt(CurrentItemIndex);
                    GetComponent<PlayerInventory>().InventoryIcon.RemoveAt(CurrentItemIndex);
                    if (CurrentItemIndex > 0)
                    {
                        CurrentItemIndex--;
                    }
                    GetComponent<PlayerInventory>().SortInventory();
                    for (int x = 0; x < GetComponent<PlayerInventory>().Inventory.Count; x++)
                    {
                        InventoryIcons[x].color = new Color(.5f, .5f, .5f, 1);
                        SetIcons();
                    }
                }
            }
        }

        #endregion

        #region SwitchItem

        if (Input.GetAxis("C1DpadLR") > 0 && !PressedLR)
        {
            PressedLR = true;
            CurrentItemIndex++;
            if (CurrentItemIndex > MaxItemIndex)
            {
                CurrentItemIndex = 0;
            }

            SetIcons();
        }
        if (Input.GetAxis("C1DpadLR") < 0 && !PressedLR)
        {
            PressedLR = true;
            CurrentItemIndex--;
            if (CurrentItemIndex < 0)
            {
                CurrentItemIndex = MaxItemIndex;
            }

            SetIcons();
        }
        if (Input.GetAxis("C1DpadLR") == 0)
        {
            PressedLR = false;
        }
        if (Input.GetKeyDown("joystick button 0"))
        {
        }

        #endregion

        #region SwitchCatergory

        if (Input.GetAxisRaw("C1LT") > 0 && !TriggerPressed) //Basic Attack Handler.
        {
            TriggerPressed = true;

            CurrentCategory--;
            if(CurrentCategory < 0)
            {
                CurrentCategory = 4;
            }
            SetSection();
        }
        if(Input.GetAxisRaw("C1RT") > 0 && !TriggerPressed)
        {
            TriggerPressed = true;

            CurrentCategory++;
            if (CurrentCategory > 4)
            {
                CurrentCategory = 0;
            }
            SetSection();
        }
        if(Input.GetAxisRaw("C1RT") == 0 && Input.GetAxisRaw("C1LT") == 0)
        {
            TriggerPressed = false;
        }

        #endregion

        #region Misc

        if(CurrentCategory == 4)
        {
            if(GetComponent<PlayerInventory>().Inventory.Count == 0)
            {
                MaxItemIndex = GetComponent<PlayerInventory>().Inventory.Count;
            }
            else
            {
                MaxItemIndex = GetComponent<PlayerInventory>().Inventory.Count - 1;
            }
        }

        #endregion
    }

    private void SetSection()
    {
        for(int i = 0; i < CategoryCovers.Length; i++)
        {
            CategoryCovers[i].SetActive(true); //Covers up all categories.
        }

        CurrentItemIndex = 0;

        switch (CurrentCategory)
        {
            case 0:

                MaxItemIndex = 4;
                CategoryCovers[0].SetActive(false);


                break;

            case 1:

                MaxItemIndex = 4;
                CategoryCovers[1].SetActive(false);


                break;

            case 2:

                MaxItemIndex = 4;
                CategoryCovers[2].SetActive(false);

                break;

            case 3:

                MaxItemIndex = 2;
                CategoryCovers[3].SetActive(false);

                break;

            case 4:

                MaxItemIndex = 3;
                CategoryCovers[4].SetActive(false);

                break;
        }

        SetIcons();
    }

    private void SetIcons()
    {
        switch (CurrentCategory)
        {
            case 0:

                foreach (Image icon in PhysicalDmgItems)
                {
                    icon.color = new Color(.5f, .5f, .5f, 1); //Darkens item icons.
                }

                PhysicalDmgItems[CurrentItemIndex].color = new Color(1, 1, 1, 1); //Lights up item icons if we are hovering over it.

                break;

            case 1:

                foreach (Image icon in MagicDmgItems)
                {
                    icon.color = new Color(.5f, .5f, .5f, 1); //Darkens item icons.
                }

                MagicDmgItems[CurrentItemIndex].color = new Color(1, 1, 1, 1); //Lights up item icons if we are hovering over it.

                break;

            case 2:

                foreach (Image icon in PhysicalDefenceItems)
                {
                    icon.color = new Color(.5f, .5f, .5f, 1); //Darkens item icons.
                }

                PhysicalDefenceItems[CurrentItemIndex].color = new Color(1, 1, 1, 1); //Lights up item icons if we are hovering over it.

                break;

            case 3:

                foreach (Image icon in MagicDefenceItem)
                {
                    icon.color = new Color(.5f, .5f, .5f, 1); //Darkens item icons.
                }

                MagicDefenceItem[CurrentItemIndex].color = new Color(1, 1, 1, 1); //Lights up item icons if we are hovering over it.

                break;

            case 4:

                for (int x = 0; x < GetComponent<PlayerInventory>().Inventory.Count; x++)
                {
                    InventoryIcons[x].color = new Color(.5f, .5f, .5f, 1);
                }
                if(InventoryIcons[CurrentItemIndex].sprite != null)
                {
                    InventoryIcons[CurrentItemIndex].color = new Color(1, 1, 1, 1); //Lights up item icons if we are hovering over it.
                }

                break;
        }
    }

    public void EnterShop()
    {
        ShopDetection.GetComponent<Stalls>().OpenShopDisplay.SetActive(false);
        ShopDetection.GetComponent<Stalls>().enabled = false;

        foreach (Image icon in PhysicalDmgItems)
        {
            icon.color = new Color(.5f, .5f, .5f, 1);
        }
        foreach (Image icon in MagicDmgItems)
        {
            icon.color = new Color(.5f, .5f, .5f, 1);
        }
        foreach (Image icon in PhysicalDefenceItems)
        {
            icon.color = new Color(.5f, .5f, .5f, 1);
        }
        foreach (Image icon in MagicDefenceItem)
        {
            icon.color = new Color(.5f, .5f, .5f, 1);
        }

        SetSection();
        SetIcons();

        this.gameObject.SetActive(true);
    }

    private void ExitShop()
    {
        this.gameObject.SetActive(false);
        ShopDetection.GetComponent<Stalls>().enabled = true;
        ShopDetection.GetComponent<Stalls>().OpenShopDisplay.SetActive(true);

        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<PlayerInput>().enabled = true;
    }

    public void BuyItem(BaseItem item)
    {
        if(GetComponent<PlayerInventory>().Inventory.Count < 4)
        {
            if(Player.GetComponent<PlayerInput>().Gold >= item.Cost)
            {
                GetComponent<PlayerInventory>().Inventory.Add(item);
                GetComponent<PlayerInventory>().InventoryIcon.Add(item.ItemIcon);
                GetComponent<PlayerInventory>().SortInventory();
            }
            else
            {
                Debug.Log("Not enough gold! You need : " + (item.Cost - Player.GetComponent<PlayerInput>().Gold)); //To send to animated text when implemented.
            }
        }
        else
        {
            Debug.Log("Not enough room in inventory!");
        }
    }
}
