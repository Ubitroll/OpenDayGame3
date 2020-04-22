using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion_Detection : MonoBehaviour
{
    public GameObject Minion;
    public GameObject MinionAnimator;
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Character" || collision.gameObject.tag == "Minion" || collision.gameObject.tag == "Turret" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "PowerSource")
        {
            if(collision.tag != "PowerSource")
            {
                if (collision.gameObject.GetComponent<Team_Assign>().Team != Minion.GetComponent<Team_Assign>().Team)
                {
                    Minion.GetComponent<Minion>().DetectedEnemies.Add(collision.gameObject);
                }
            }
            if(collision.tag == "PowerSource")
            {
                if(Minion.GetComponent<Team_Assign>().Team == true)
                {
                    if (collision.gameObject.GetComponent<Enemy_Crystal_Singleton>() == true)
                    {
                        Minion.GetComponent<Minion>().DetectedEnemies.Add(collision.gameObject);
                    }
                }
                else
                {
                    if (collision.gameObject.GetComponent<Good_Crystal_Singleton>() == true)
                    {
                        Minion.GetComponent<Minion>().DetectedEnemies.Add(collision.gameObject);
                    }
                }
            }
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Character" || collision.gameObject.tag == "Minion" || collision.gameObject.tag == "Turret" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "PowerSource")
        {
            if (collision.gameObject.GetComponent<Team_Assign>().Team != Minion.GetComponent<Team_Assign>().Team)
            {
                for(int i = 0; i < Minion.GetComponent<Minion>().DetectedEnemies.Count;i++)
                {
                    if(Minion.GetComponent<Minion>().DetectedEnemies[i] == collision.gameObject)
                    {
                        Minion.GetComponent<Minion>().DetectedEnemies.RemoveAt(i);
                        return;
                    }
                }
            }
        }
    }
}
