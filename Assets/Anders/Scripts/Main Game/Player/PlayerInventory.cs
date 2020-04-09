using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public List<BaseItem> Inventory = new List<BaseItem>();

    public List<Image> InventoryIcon = new List<Image>();

    public int MaxInventoryCap;

    #region Singleton

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

        MaxInventoryCap = 6;
    }

    #endregion
    public void SortInventory()
    {
        for (int i = 0; i < Inventory.Count; i++)
        {
            InventoryIcon[i].sprite = Inventory[i].ItemIcon;
            InventoryIcon[i].color = new Color(1, 1, 1, 1);
        }
        for (int i = Inventory.Count; i < InventoryIcon.Count; i++)
        {
            InventoryIcon[i].color = new Color(1, 1, 1, 0);
        }
    }
}
