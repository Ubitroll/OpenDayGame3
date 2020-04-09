using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject Player;

    public enum DamageType {Weapon, Crystal};

    public DamageType Type;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if(Player == null)
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var Object = other.gameObject;

        switch (Object.tag)
        {
            case "Minion":

                if (Player.GetComponent<Team_Assign>().Team != Object.GetComponent<Team_Assign>().Team)
                {
                    Debug.Log("Hit Minion");

                    if (Type == DamageType.Weapon)
                    {
                        other.gameObject.GetComponent<Minion>().Health -= Player.GetComponent<PlayerInput>().CurrentWeaponPower;
                    }
                    else //Has to equal Crystal since only 2 states.
                    {
                        //other.gameObject.GetComponent<Minion>().Health -= Player.GetComponent<PlayerInput>();
                    }
                }

                break;

            case "Character":

                if (other.GetComponent<Team_Assign>().Team != Player.GetComponent<Team_Assign>().Team)
                {
                    Debug.Log("Hit Character");

                    if (Type == DamageType.Weapon)
                    {
                        other.gameObject.GetComponent<BaseCharacter>().CurrentHealth -= Player.GetComponent<PlayerInput>().CurrentWeaponPower;
                    }
                    else //Has to equal Crystal since only 2 states.
                    {
                        //other.gameObject.GetComponent<Minion>().Health -= Player.GetComponent<PlayerInput>();
                    }
                }

                break;

            case "Turret":

                if (other.GetComponent<Team_Assign>().Team != Player.GetComponent<Team_Assign>().Team)
                {
                    Debug.Log("Hit Turret");

                    if (Type == DamageType.Weapon)
                    {
                        other.gameObject.GetComponent<Turret_Base>().Health -= Player.GetComponent<PlayerInput>().CurrentWeaponPower;
                    }
                    else //Has to equal Crystal since only 2 states.
                    {
                        //other.gameObject.GetComponent<Minion>().Health -= Player.GetComponent<PlayerInput>();
                    }
                }

                break;

            case "PowerSource":

                if(other.GetComponent<Enemy_Crystal_Singleton>() == true)
                {
                    if (Type == DamageType.Weapon)
                    {
                        other.gameObject.GetComponent<PowerSource>().Health -= Player.GetComponent<PlayerInput>().CurrentWeaponPower;
                    }
                    else //Has to equal Crystal since only 2 states.
                    {
                        //other.gameObject.GetComponent<Minion>().Health -= Player.GetComponent<PlayerInput>();
                    }
                }

                break;
        }

    }
}

