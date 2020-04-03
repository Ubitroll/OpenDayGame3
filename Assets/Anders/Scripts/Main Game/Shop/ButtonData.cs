using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonData : MonoBehaviour
{
    public BaseItem ThisItem;

    public GameObject Player;

    [SerializeField]
    private int Clicked;

    public void Selected()
    {
        if (Player.GetComponent<Player>().Gold >= ThisItem.Cost)
        {
            if(PlayerInventory.Instance.Inventory.Count < PlayerInventory.Instance.MaxInventoryCap)
            {
                Player.GetComponent<Player>().Gold -= ThisItem.Cost;
                PlayerInventory.Instance.Inventory.Add(ThisItem);
                PlayerInventory.Instance.SortInventory();
            }
            else
            {
                Debug.Log("Not enough room in inventory");
            }
        }
        else
        {
            Debug.Log("Not enough gold");
        }
    }

    private void Update()
    {
       if(Player.GetComponent<Player>().Gold >= ThisItem.Cost)
       {
            this.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
       }
       if (Player.GetComponent<Player>().Gold < ThisItem.Cost)
       {
            this.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
       }
    }
}
