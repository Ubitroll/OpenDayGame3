using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public enum Heroes { Armadillo, Summoner, Mabbit};
    public Heroes SelectHero;

    public float AbilityACoolDown, AbilityBCoolDown, UltimateCoolDown;

    public float AbilityADMG, AbilityBDMG, UltimateDMG;

    public GameObject Ability1VFX, Ability2VFX, UltimateVFX;

    public void AbilityA()
    {
        if(AbilityACoolDown == 0)
        {
            switch (SelectHero)
            {
                case Heroes.Armadillo:

                    Debug.Log("Trigger Armadillo A");

                    break;

                case Heroes.Summoner:

                    Debug.Log("Trigger Summoner A");

                    GameObject Instance = Instantiate(Ability1VFX, this.transform.position + new Vector3( 0, 1.8f, 0.5f), this.transform.rotation) as GameObject;
                    GetComponent<PlayerInput>().ActionValue = "Ability1";


                    break;

                case Heroes.Mabbit:

                    Debug.Log("Trigger Mabbit A");


                    break;
            }
        }
        else
        {
            AbilityACoolDown -= Time.fixedDeltaTime;
            if(AbilityACoolDown <= 0)
            {
                AbilityACoolDown = 0;
            }
        }
    }

    //public IEnumerator AbilityATimer()
    //{
    //    yield return new WaitForSeconds(2);

    //}
}
