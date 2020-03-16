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
            for (int i = 0; i < Player.GetComponent<PlayerInventory>().Inventory.Count; i++)
            {
                Debug.Log(i);
                if (Player.GetComponent<PlayerInventory>().Inventory[i] == null)
                {
                    Player.GetComponent<PlayerInventory>().Inventory[i] = ThisItem;
                    Player.GetComponent<Player>().Gold -= ThisItem.Cost;
                    Player.GetComponent<PlayerInventory>().SortInventory();
                    break;
                }
                if (i == Player.GetComponent<PlayerInventory>().Inventory.Count - 1)
                {
                    Debug.Log("Not enough room in the inventory");
                }
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
