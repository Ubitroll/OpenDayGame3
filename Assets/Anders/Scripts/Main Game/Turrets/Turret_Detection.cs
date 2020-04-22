using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Detection : MonoBehaviour
{
    public GameObject ParentTurret;
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Detection");
        if (collision.gameObject.tag == "Character" || collision.gameObject.tag == "Minion" || collision.gameObject.tag == "Player")
        {
            Debug.Log("Detected : " + collision.gameObject.tag);
            if (collision.gameObject.GetComponent<Team_Assign>().Team != ParentTurret.GetComponent<Team_Assign>().Team)
            {
                Debug.Log("IsEnemy");
                ParentTurret.GetComponent<Turret_Base>().DetectedEnemies.Add(collision.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Character" || collision.gameObject.tag == "Minion" || collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Team_Assign>().Team != ParentTurret.GetComponent<Team_Assign>().Team)
            {
                ParentTurret.GetComponent<Turret_Base>().DetectedEnemies.Remove(collision.gameObject);
                ParentTurret.GetComponent<Turret_Base>().ResetTurret();
            }
        }        
    }
}
