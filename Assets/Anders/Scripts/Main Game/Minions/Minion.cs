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
                    Agent.SetDestination(DetectedEnemies[0].transform.position);

                    Distance = Vector3.Distance(this.gameObject.transform.position, DetectedEnemies[0].transform.position);

                    if(Distance <= 1.2f)
                    { 
                        Action_State = 1;
                    }
                }

                break;

            case 1:

                Controller.SetFloat("Action", 0.33f);
                
                transform.LookAt(DetectedEnemies[0].transform);

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
        string Target = DetectedEnemies[0].gameObject.tag;

        Debug.Log("Hitting" + Target);

        switch (Target)
        {
            case "Minion":

                DetectedEnemies[0].gameObject.GetComponent<Minion>().Health -= Damage;

                if (DetectedEnemies[0].gameObject.GetComponent<Minion>().Health <= 0)
                {
                    DetectedEnemies[0].gameObject.GetComponent<Minion>().Action_State = 0;
                    DetectedEnemies.RemoveAt(0);
                }

                break;

            case "Turret":

                DetectedEnemies[0].gameObject.GetComponent<Turret_Base>().Health -= Damage;

                break;
        }
    }
}
