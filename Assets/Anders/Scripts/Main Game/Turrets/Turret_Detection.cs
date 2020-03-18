using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Detection : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Character" || collision.gameObject.tag == "Minion")
        {
            if (collision.gameObject.GetComponent<Team_Assign>().Team != this.gameObject.GetComponentInParent<Team_Assign>().Team)
            {
                this.gameObject.GetComponentInParent<Turret_Base>().DetectedEnemies.Add(collision.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Character" || collision.gameObject.tag == "Minion")
        {
            if (collision.gameObject.GetComponent<Team_Assign>().Team != this.gameObject.GetComponentInParent<Team_Assign>().Team)
            {
                this.gameObject.GetComponentInParent<Turret_Base>().DetectedEnemies.Remove(collision.gameObject);
                this.gameObject.GetComponentInParent<Turret_Base>().ResetTurret();
            }
        }        
    }
}
