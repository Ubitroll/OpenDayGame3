using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion_Detection : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Character" || collision.gameObject.tag == "Minion" || collision.gameObject.tag == "Turret")
        {
            if (collision.gameObject.GetComponent<Team_Assign>().Team != this.gameObject.GetComponentInParent<Team_Assign>().Team)
            {
                this.gameObject.GetComponentInParent<Minion>().DetectedEnemies.Add(collision.gameObject);             
            }
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Character" || collision.gameObject.tag == "Minion" || collision.gameObject.tag == "Turret")
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
