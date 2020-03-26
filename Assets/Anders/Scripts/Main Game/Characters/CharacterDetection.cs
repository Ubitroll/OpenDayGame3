using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        switch (collision.tag)
        {
            case "Character":

                if (collision.gameObject.GetComponent<Team_Assign>().Team != this.gameObject.GetComponentInParent<Team_Assign>().Team)
                {
                    this.gameObject.GetComponentInParent<BaseCharacter>().DetectedEnemies.Add(collision.gameObject);
                }
                else
                {
                    this.gameObject.GetComponentInParent<BaseCharacter>().DetectedAllies.Add(collision.gameObject);
                }

                break;

            case "Minion":

                if (collision.gameObject.GetComponent<Team_Assign>().Team != this.gameObject.GetComponentInParent<Team_Assign>().Team)
                {
                    this.gameObject.GetComponentInParent<BaseCharacter>().DetectedEntities.Add(collision.gameObject);
                }
                else
                {
                    return;
                }

                break;

            case "Turret":

                if (collision.gameObject.GetComponent<Team_Assign>().Team != this.gameObject.GetComponentInParent<Team_Assign>().Team)
                {
                    this.gameObject.GetComponentInParent<BaseCharacter>().DetectedObjectives.Add(collision.gameObject);
                }
                else
                {
                    return;
                }

                break;

            case "PowerSource":

                if (collision.gameObject.GetComponent<Team_Assign>().Team != this.gameObject.GetComponentInParent<Team_Assign>().Team)
                {
                    this.gameObject.GetComponentInParent<BaseCharacter>().DetectedObjectives.Add(collision.gameObject);
                }
                else
                {
                    return;
                }

                break;
        }

        //|| collision.gameObject.tag == "Minion" || collision.gameObject.tag == "Turret"
    }
    private void OnTriggerExit(Collider collision)
    {
        switch (collision.tag)
        {
            case "Character":

                if (collision.gameObject.GetComponent<Team_Assign>().Team != this.gameObject.GetComponentInParent<Team_Assign>().Team)
                {
                    this.gameObject.GetComponentInParent<BaseCharacter>().DetectedEnemies.Remove(collision.gameObject);
                }
                else
                {
                    this.gameObject.GetComponentInParent<BaseCharacter>().DetectedAllies.Remove(collision.gameObject);
                }

                break;

            case "Minion":

                if (collision.gameObject.GetComponent<Team_Assign>().Team != this.gameObject.GetComponentInParent<Team_Assign>().Team)
                {
                    this.gameObject.GetComponentInParent<BaseCharacter>().DetectedEntities.Remove(collision.gameObject);
                }

                break;

            case "Turrets":

                if (collision.gameObject.GetComponent<Team_Assign>().Team != this.gameObject.GetComponentInParent<Team_Assign>().Team)
                {
                    this.gameObject.GetComponentInParent<BaseCharacter>().DetectedObjectives.Remove(collision.gameObject);
                }

                break;

            case "PowerSource":

                if (collision.gameObject.GetComponent<Team_Assign>().Team != this.gameObject.GetComponentInParent<Team_Assign>().Team)
                {
                    this.gameObject.GetComponentInParent<BaseCharacter>().DetectedObjectives.Remove(collision.gameObject);
                }

                break;       
        }
    }
}
