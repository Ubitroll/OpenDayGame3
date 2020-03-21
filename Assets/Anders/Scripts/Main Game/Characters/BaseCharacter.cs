using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BaseCharacter : MonoBehaviour
{
    #region CharacterStats

    public string Name;

    private int Level;

    private float CurrentHealth;
    private float CurrentMaxHealth;
    public float BaseMaxHealth;

    private float CurrentHealthRegen;
    public float BaseHealthRegen;

    public float BaseMana;
    private float CurrentMana;

    private float CurrentManaRegen;
    public float BaseManaRegen;

    private float CurrentSpeed;
    public float BaseSpeed;

    private float CurrentWeaponPower;
    public float BaseWeaponPower;

    private float CurrentAttackSpeed;
    public float BaseAttackSpeed;

    private float CurrentCrystalPower;
    public float BaseCrystalPower;

    private float CurrentCriticalChance;
    public float BaseCriticalChance;

    private float CurrentCriticalDamage;
    public float BaseCriticalDamage;

    //Non passive vars so do not need a base value as it is default of 0.
    public float LifeSteal;
    public float ArmorPierce;
    public float ShieldPierce;

    //TP Vars
    public float TPReturnTimer;


    #endregion

    #region AI Vars

    private NavMeshAgent Agent;

    [SerializeField]
    private List<GameObject> DetectedObjectives = new List<GameObject>();
    [SerializeField]
    private List<GameObject> DetectedEntities = new List<GameObject>();
    [SerializeField]
    private List<GameObject> DetectedEnemies = new List<GameObject>();
    [SerializeField]
    private List<GameObject> DetectedAllies = new List<GameObject>();

    private int WhichList; //0 = Objectives : 1 = Entities : 2 = EnemyCharacters. Used to different the action when attacking due to targetting.

    private GameObject EnemyCrystal;

    private GameObject HomeBase;

    #region State Vars
    [SerializeField]
    private int AIStateValue;
    public enum AIState {Idle, Search, Chase, Farm, ObjectPush, ReturnToBase, Attack, Celebrate, Retreat};

    #endregion

    #endregion

    #region Animation Vars

    public int ActionValue;
    public enum AIAnimation {Idle, Jog, Sprint, BasicAttack, Ability1, Ability2, Ability3, Death, Celebration};
    public AIAnimation CurrentAnimationState;

    private Animator Controller;

    #endregion

    public bool IsInvisible;
    void Start()
    {
        CurrentHealth = 800;
        Controller = this.gameObject.GetComponent<Animator>();
        Agent = this.gameObject.GetComponent<NavMeshAgent>();

        //EnemyCrystal = Enemy_Crystal_Singleton.Instance.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemyCrystal == null)
        {
            EnemyCrystal = Enemy_Crystal_Singleton.Instance.gameObject;
        }

        AIConditionChecks();

        switch (AIStateValue)
        {
            case 0:

                break;

            case 1:

                break;

            case 2:

                if (DetectedEnemies[0] == null)
                {
                    Debug.Log("No enemies detected ERROR");
                    AIStateValue = 4;
                    ActionValue = 1;
                    return;
                }
                else
                {
                    ActionValue = 1;
                    Agent.SetDestination(DetectedEnemies[0].transform.position);
                    return;
                }

                break;

            case 3:
                if(CurrentHealth >= CurrentMaxHealth / 3)
                {
                    Agent.SetDestination(DetectedEntities[0].transform.position);
                    ActionValue = 1;

                    if (Vector3.Distance(this.gameObject.transform.position, DetectedEntities[0].transform.position) <= 0.5f)
                    {
                        WhichList = 1;
                        AIStateValue = 6;
                        ActionValue = 3;
                        return;
                    }
                }
                else
                {
                    if(DetectedEnemies.Count == 0)
                    {
                        Agent.SetDestination(DetectedEntities[0].transform.position);
                        ActionValue = 1;

                        if (Vector3.Distance(this.gameObject.transform.position, DetectedEntities[0].transform.position) <= 0.5f)
                        {
                            WhichList = 1;
                            ActionValue = 3;
                            AIStateValue = 6;
                            return;
                        }
                    }
                    else
                    {
                        AIStateValue = 8;
                        return;
                    }
                }

                break;

            case 4:

                if(DetectedObjectives.Count == 0)
                {
                    Agent.SetDestination(EnemyCrystal.transform.position); //Objective Push
                    ActionValue = 1;
                }
                else
                {
                    Agent.SetDestination(DetectedObjectives[0].transform.position);

                    if (Vector3.Distance(this.gameObject.transform.position, DetectedObjectives[0].transform.position) <= 0.5f)
                    {
                        WhichList = 0;
                        ActionValue = 3;
                        AIStateValue = 6;
                    }
                }
                
                break;

            case 5:

                break;

            case 6:

                break;

            case 7:

                break;

            case 8:

                if(DetectedEnemies.Count == 0)
                {
                    AIStateValue = 5;
                    return;
                }
                else
                {
                    Agent.SetDestination(HomeBase.transform.position);
                    ActionValue = 1;
                    if (DetectedEnemies.Count == 0)
                    {
                        AIStateValue = 5;
                        ActionValue = 0;
                        TPReturnTimer -= Time.fixedDeltaTime;
                        if(TPReturnTimer <= 0)
                        {
                            this.gameObject.transform.position = HomeBase.transform.position;
                        }
                        return;
                    }
                }

                break;
        }

        ChangeAnimationState();

        if (IsInvisible)
        {
        }
        else
        {
        }
    }
    void AIConditionChecks()
    {
        if(CurrentHealth <= CurrentMaxHealth / 3)
        {
            AIStateValue = 5; //Return to base.
            return;
        }

        if (DetectedEntities.Count == 0 && DetectedEnemies.Count == 0 && DetectedObjectives.Count == 0)
        {
            if(CurrentHealth >= CurrentMaxHealth / 3)
            {
                AIStateValue = 4; //Push Objectives
                return;
            }
            else
            {
                //Add in retreat if needed to get to save distance to TP.
                AIStateValue = 5; //ReturnToBase.
                return;
            }
        }
    }
    void ChangeAnimationState()
    {
        switch (ActionValue)
        {
            case 0:

                Controller.SetFloat("Action", 0);
                CurrentAnimationState = AIAnimation.Idle;

                break;

            case 1:

                Controller.SetFloat("Action", 0.1f);
                CurrentAnimationState = AIAnimation.Jog;

                break;

            case 2:

                Controller.SetFloat("Action", 0.2f);
                CurrentAnimationState = AIAnimation.Sprint;

                break;

            case 3:

                Controller.SetFloat("Action", 0.3f);
                CurrentAnimationState = AIAnimation.BasicAttack;

                break;

            case 4:

                Controller.SetFloat("Action", 0.4f);
                CurrentAnimationState = AIAnimation.Ability1;

                break;

            case 5:

                Controller.SetFloat("Action", 0.5f);
                CurrentAnimationState = AIAnimation.Ability2;

                break;

            case 6:

                Controller.SetFloat("Action", 0.6f);
                CurrentAnimationState = AIAnimation.Ability3;

                break;

            case 7:

                Controller.SetFloat("Action", 0.7f);
                CurrentAnimationState = AIAnimation.Death;

                break;

            case 8:

                Controller.SetFloat("Action", 0.8f);
                CurrentAnimationState = AIAnimation.Celebration;

                break;
        }
    }

    void ApplyNewItems()
    {
       foreach(BaseItem Item in PlayerInventory.Instance.GetComponent<PlayerInventory>().Inventory)
       {
            
       }
    }



    public void BasicAttack()
    {
        switch (WhichList)
        {
            case 0:

                DetectedObjectives[0].GetComponent<Turret_Base>().Health -= (int)CurrentWeaponPower;

                break;

            case 1:

                DetectedEntities[0].GetComponent<Turret_Base>().Health -= (int)CurrentWeaponPower;

                break;

            case 2:

                DetectedEnemies[0].GetComponent<Turret_Base>().Health -= (int)CurrentWeaponPower;

                break;
        }
    }

    public void AbilityAttack()
    {

    }
}
