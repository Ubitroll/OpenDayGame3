using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Minion : MonoBehaviour
{
    public int Health;
    public int Damage;

    public int Action_State;

    public float Speed;
    private float Action = 0;
    private float Death_Timer;
    [SerializeField]
    private float Distance;
    public GameObject Target;


    public bool Team;

    private Animator Controller;

    private NavMeshAgent Agent;

    public List<GameObject> DetectedEnemies = new List<GameObject>();


    private void Start()
    {
        Death_Timer = 2.3f;
        Controller = this.gameObject.GetComponent<Animator>();

        Agent = this.gameObject.GetComponent<NavMeshAgent>();
        Health = 200;
        Damage = 15;
    }

    private void Update()
    {        
        for(int i = 0; i < DetectedEnemies.Count; i++)
        {
            if(DetectedEnemies[i] == false)
            {
                DetectedEnemies.RemoveAt(i);
            }
        }

        foreach (GameObject Targets in DetectedEnemies)
        {
            if (Target == null)
            {
                Target = Targets;
            }
            else
            {
                if (Vector3.Distance(this.gameObject.transform.position, Targets.transform.position) < Vector3.Distance(this.gameObject.transform.position, Target.transform.position))
                {
                    Target = Targets;
                }
            }
        }

        if (Target == false && DetectedEnemies.Count != 0)
        {
            Target = DetectedEnemies[0];
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

        if(Health <= 0)
        {
            Action_State = 3;
        }

        switch (Action_State)
        {
            case 0:

                Controller.SetFloat("Action", 0);

                if(DetectedEnemies.Count == 0)
                {
                    if(this.gameObject.GetComponent<Team_Assign>().Team == true)
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
                    Agent.SetDestination(Target.transform.position);

                    Distance = Vector3.Distance(this.gameObject.transform.position, Target.transform.position);

                    if(Distance <= 1.2f)
                    { 
                        Action_State = 1;
                    }
                }

                break;

            case 1:

                Controller.SetFloat("Action", 0.33f);
                
                transform.LookAt(Target.transform);

                break;

            case 3:

                Controller.SetFloat("Action", 1f);

                break;
        }
    }

    public void Death()
    {
        Destroy(this.gameObject);
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
        }
    }
}
