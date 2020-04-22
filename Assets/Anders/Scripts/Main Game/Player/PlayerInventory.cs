using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public List<BaseItem> Inventory = new List<BaseItem>();

    public List<Sprite> InventoryIcon = new List<Sprite>();

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
       for(int i = 0; i < Inventory.Count; i++)
       {
            InventoryIcon[i] = Inventory[i].ItemIcon;
            GetComponent<ShopManagerv2>().InventoryIcons[i].sprite = InventoryIcon[i];
       }

       for(int i = Inventory.Count; i < GetComponent<ShopManagerv2>().InventoryIcons.Length; i++)
       {
            GetComponent<ShopManagerv2>().InventoryIcons[i].sprite = null;
            GetComponent<ShopManagerv2>().InventoryIcons[i].color = new Color(1,1,1,0);
       }

        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<PlayerInput>().ApplyItems();
    }
}
