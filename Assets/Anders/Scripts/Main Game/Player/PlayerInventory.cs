using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public List<BaseItem> Inventory = new List<BaseItem>();

    public List<Image> InventoryIcon = new List<Image>();

    private static PlayerInventory _instance;

    public static PlayerInventory Instance { get { return _instance; } }

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
    public void SortInventory()
    {
        //if(Inventory[0] == null)
        //{
        //    Inventory[0] = Inventory[1];
        //    Inventory[1] = Inventory[2];
        //    Inventory[2] = Inventory[3];
        //    Inventory[3] = Inventory[4];
        //    Inventory[4] = Inventory[5];
        //    Inventory[5] = null;

        //    InventoryIcon[0] = Inventory[1].ItemIcon;
        //    InventoryIcon[1] = Inventory[2].ItemIcon;
        //    InventoryIcon[2] = Inventory[3].ItemIcon;
        //    InventoryIcon[3] = Inventory[4].ItemIcon;
        //    InventoryIcon[4] = Inventory[5].ItemIcon;
        //    InventoryIcon[5] = null;


        //    for (int i = 0; i < Inventory.Count; i++)
        //    { 
        //        if(Inventory[i] != null)
        //        {
        //            InventoryIcon[i] = Inventory[i].ItemIcon;
        //            Instantiate(Inventory[i].ItemIcon, InventoryIcon[i].transform);
        //        }
        //    }
        //}

        for (int i = 0; i < Inventory.Count; i++)
        {
            if (Inventory[i] == null)
            {
                Inventory[i] = Inventory[i + 1];

                Instantiate(Inventory[i + 1].ItemIcon, InventoryIcon[i].transform);

                if (i + 1 > Inventory.Count)
                {
                    Inventory[i] = null;
                }
                else
                {
                    Inventory[i + 1] = null;
                }

            }
            else
            {
                Instantiate(Inventory[i].ItemIcon, InventoryIcon[i].transform);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SortInventory();
        }   
    }
}
