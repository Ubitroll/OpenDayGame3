using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Minion : MonoBehaviour
{
    public int Health;
    public int Damage;

    public int Action_State;

    private float Action = 0;
    [SerializeField]
    private float Distance;
    public GameObject Target;

    [SerializeField]
    private Animator Controller;

    [SerializeField]
    private NavMeshAgent Agent;

    public List<GameObject> DetectedEnemies = new List<GameObject>();

    public GameObject EnemyCrystal;

    public GameObject ThisMinion;


    private void Start()
    {
        Health = 200;
        Damage = 15;

        if (this.gameObject.GetComponent<Team_Assign>().Team == true)
        {
            EnemyCrystal = Enemy_Crystal_Singleton.Instance.gameObject;
        }
        else
        {
            EnemyCrystal = Good_Crystal_Singleton.Instance.gameObject;
        }
    }

    private void Update()
    {

        Conditions();

        switch (Action_State)
        {
            case 0:

                Controller.SetBool("IsAttacking", false);

                Agent.isStopped = false;

                if (DetectedEnemies.Count == 0)
                {
                    Target = EnemyCrystal;

                    if (this.gameObject.GetComponent<Team_Assign>().Team == true)
                    {
                        Agent.SetDestination(Enemy_Crystal_Singleton.Instance.gameObject.transform.position);
                    }
                    if(this.gameObject.GetComponent<Team_Assign>().Team == false)
                    {
                        Agent.SetDestination(Good_Crystal_Singleton.Instance.gameObject.transform.position);
                    }
                }
                else
                {
                    Target = DetectedEnemies[0];

                    Agent.SetDestination(Target.transform.position);

                    Distance = Vector3.Distance(this.gameObject.transform.position, Target.transform.position);

                    if(Distance <= 4.2)
                    { 
                        Action_State = 1;
                    }
                }

                break;

            case 1:
               
                Agent.isStopped = true;

                Controller.SetBool("IsAttacking", true);
                
                transform.LookAt(Target.transform);

                break;

            case 3:

                Agent.isStopped = true;
                Controller.SetBool("IsDead", true);

                break;
        }

        CheckTargetHealth();
    }

    void Conditions()
    {
        for (int i = 0; i < DetectedEnemies.Count; i++)
        {
            if (DetectedEnemies[i] == null)
            {
                DetectedEnemies.RemoveAt(i);
            }
        }
        if(Target == null && DetectedEnemies.Count != 0)
        {
            Target = DetectedEnemies[0];
        }

        if (DetectedEnemies.Count != 0)
        {
            for (int i = 0; i < DetectedEnemies.Count; i++)
            {
                if (Vector3.Distance(this.gameObject.transform.position, DetectedEnemies[i].transform.position) < Vector3.Distance(this.gameObject.transform.position, Target.transform.position))
                {
                    Target = DetectedEnemies[i];
                }
            }
        }

        if (DetectedEnemies.Count != 0)
        {
            if (DetectedEnemies[0] == null)
            {
                DetectedEnemies.RemoveAt(0);
            }
        }       

        if (DetectedEnemies.Count == 0 || DetectedEnemies == null)
        {
            Action_State = 0;
        }

        if (Health <= 0)
        {
            Action_State = 3;
        }
    }

    public void Death()
    {
        Destroy(ThisMinion);
    }

    void CheckTargetHealth()
    {
        if(Target != null)
        {
            switch (Target.tag)
            {
                case "Minion":

                    if (Target.GetComponent<Minion>().Health <= 0)
                    {
                        Target = null;
                        Conditions();
                    }

                    break;

                case "Turret":

                    if (Target.GetComponent<Turret_Base>().Health <= 0)
                    {
                        Target = null;
                        Conditions();
                    }


                    break;

                case "Character":


                    if (Target.GetComponent<BaseCharacter>().CurrentHealth <= 0)
                    {
                        Target = null;
                        Conditions();
                    }

                    break;

                case "Player":

                    if (Target.GetComponent<PlayerInput>().CurrentHealth <= 0)
                    {
                        Target = null;
                        Conditions();
                    }

                    break;

                case "PowerSource":

                    if (Target.GetComponent<PowerSource>().Health <= 0)
                    {
                        Target = null;
                        Conditions();
                    }

                    break;
            }
        }     
    }
    public void Melee_Hit()
    {

        switch (Target.tag)
        {
            case "Minion":

                Target.gameObject.GetComponent<Minion>().Health -= Damage;

                if (Target.gameObject.GetComponent<Minion>().Health <= 0)
                {
                    DetectedEnemies.Remove(Target);
                }

                break;

            case "Character":

                Target.gameObject.GetComponent<BaseCharacter>().CurrentHealth -= Damage;

                if (Target.gameObject.GetComponent<BaseCharacter>().CurrentHealth <= 0)
                {
                    DetectedEnemies.Remove(Target);
                }

                break;

            case "Turret":

                DetectedEnemies[0].gameObject.GetComponent<Turret_Base>().Health -= Damage;

                break;

            case "PowerSource":

                DetectedEnemies[0].gameObject.GetComponent<PowerSource>().Health -= Damage;

                break;
        }
    }
}
