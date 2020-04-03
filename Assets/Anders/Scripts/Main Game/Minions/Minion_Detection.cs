using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion_Detection : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Character" || collision.gameObject.tag == "Minion" || collision.gameObject.tag == "Turret" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "PowerSource")
        {
            if(collision.tag != "PowerSource")
            {
                if (collision.gameObject.GetComponent<Team_Assign>().Team != this.gameObject.GetComponentInParent<Team_Assign>().Team)
                {
                    this.gameObject.GetComponentInParent<Minion>().DetectedEnemies.Add(collision.gameObject);
                }
            }
            if(collision.tag == "PowerSource")
            {
                if(this.gameObject.GetComponentInParent<Team_Assign>().Team == true)
                {
                    if (collision.gameObject.GetComponent<Enemy_Crystal_Singleton>() == true)
                    {
                        this.gameObject.GetComponentInParent<Minion>().DetectedEnemies.Add(collision.gameObject);
                    }
                }
                else
                {
                    if (collision.gameObject.GetComponent<Good_Crystal_Singleton>() == true)
                    {
                        this.gameObject.GetComponentInParent<Minion>().DetectedEnemies.Add(collision.gameObject);
                    }
                }
            }
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Character" || collision.gameObject.tag == "Minion" || collision.gameObject.tag == "Turret" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "PowerSource")
        {
            if (collision.gameObject.GetComponent<Team_Assign>().Team != this.gameObject.GetComponentInParent<Team_Assign>().Team)
            {
                for(int i = 0; i < this.gameObject.GetComponentInParent<Minion>().DetectedEnemies.Count;i++)
                {
                    if(this.gameObject.GetComponentInParent<Minion>().DetectedEnemies[i] == collision.gameObject)
                    {
                        this.gameObject.GetComponentInParent<Minion>().DetectedEnemies.RemoveAt(i);
                        return;
                    }
                }
            }
        }
    }
}
