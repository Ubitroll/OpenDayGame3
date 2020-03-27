using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BaseCharacter : MonoBehaviour
{
    #region CharacterStats

    public string Name;

    private int Level;

    [SerializeField]
    public float CurrentHealth;
    public float CurrentMaxHealth;
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

    public List<GameObject> DetectedObjectives = new List<GameObject>();
    public List<GameObject> DetectedEntities = new List<GameObject>();
    public List<GameObject> DetectedEnemies = new List<GameObject>();
    public List<GameObject> DetectedAllies = new List<GameObject>();

    public GameObject Target;

    private int WhichList; //0 = Objectives : 1 = Entities : 2 = EnemyCharacters. Used to different the action when attacking due to targetting.

    public GameObject EnemyCrystal;

    public GameObject HomeBase;

    #region State Vars
    [SerializeField]
    private int AIStateValue;
    public enum AIState {Idle, Search, Chase, Farm, ObjectPush, ReturnToBase, Attack, Celebrate, Retreat};

    public float StoppingDistance;

    #endregion

    #endregion

    #region Misc Vars

    public float DeathTimer, TPTimer;

    public bool IsDead;

    public GameObject HomePowerSource;

    #endregion

    #region Animation Vars

    public int ActionValue;
    public enum AIAnimation {Idle, Jog, Sprint, BasicAttack, Ability1, Ability2, Ability3, Death, Celebration};
    public AIAnimation CurrentAnimationState;

    private Animator Controller;

    #endregion

    //debug vars
    public float show;

    public bool IsInvisible;
    void Start()
    {
        TPTimer = 3;
        CurrentHealth = BaseMaxHealth;
        CurrentMaxHealth = BaseMaxHealth;
        CurrentWeaponPower = BaseWeaponPower;
        Controller = this.gameObject.GetComponent<Animator>();
        Agent = this.gameObject.GetComponent<NavMeshAgent>();

        if (this.gameObject.GetComponent<Team_Assign>().Team == true)
        {
            EnemyCrystal = Enemy_Crystal_Singleton.Instance.gameObject;
            HomePowerSource = Good_Crystal_Singleton.Instance.gameObject;
        }
        else
        {
            EnemyCrystal = Good_Crystal_Singleton.Instance.gameObject;
            HomePowerSource = Enemy_Crystal_Singleton.Instance.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        AIConditionChecks();

        if (CurrentHealth < CurrentMaxHealth / 2)
        {
            AIStateValue = 2;
            return;
        }
        #region Checking Missing List Objects

        for (int i = 0; i < DetectedEnemies.Count; i++)
        {
            if (DetectedEnemies[i] == false)
            {
                DetectedEnemies.RemoveAt(i);
            }
        }

        for (int i = 0; i < DetectedEntities.Count; i++)
        {
            if (DetectedEntities[i] == false)
            {
                DetectedEntities.RemoveAt(i);
            }
        }

        #endregion

        if(Target == null)
        {
            AIStateValue = 3;
        }

        if (Target != null)
        {
            if (Vector3.Distance(this.gameObject.transform.position, Target.transform.position) > 10)
            {
                if (Target.tag != "PowerSource")
                {
                    Target = null;
                    Agent.isStopped = true;
                }
            }
            CheckTargetHealth();
            show = Vector3.Distance(this.gameObject.transform.position, Target.transform.position);
        }
        
        if(CurrentHealth <= 0)
        {
            if (!IsDead)
            {
                CharacterDies();
                DetectedEnemies.Clear();
                DetectedAllies.Clear();
                DetectedEntities.Clear();
                Target = null;
                AIStateValue = 7;
            }
            else
            {
                return;
            }
        }

        if(DeathTimer > 0)
        {
            DeathTimer -= Time.fixedDeltaTime;

            AIStateValue = 7;

            if(DeathTimer <= 0)
            {
                IsDead = false;
                AIStateValue = 3;
                Agent.isStopped = false;
                this.gameObject.GetComponent<CapsuleCollider>().enabled = true;
            }
        }


        if(EnemyCrystal == null)
        {
            if(this.gameObject.GetComponent<Team_Assign>().Team == true)
            {
                EnemyCrystal = Enemy_Crystal_Singleton.Instance.gameObject;
            }
            else
            {
                EnemyCrystal = Good_Crystal_Singleton.Instance.gameObject;
            }
        }

        if (IsInvisible)
        {
        }
        else
        {
        }
    }
    void AIConditionChecks()
    {
        switch (AIStateValue)
        {
            case 0: //Waiting / Regening

             #region 0

                if (DetectedEnemies.Count == 0 && DetectedEntities.Count == 0)
                {
                    if(CurrentHealth >= CurrentMaxHealth / 3)
                    { //Add Start game bool here + timer to have count down!!!
                        AIStateValue = 3; //Default state should be push objective.
                        return;
                    }
                    else
                    {
                        AIStateValue = 2; //Retreating
                    }
                }


                break;

            #endregion

            case 1: //Chase Target

             #region 1

                if (DetectedEnemies.Count == 0)
                {
                    Target = DetectedEntities[0];
                    Controller.SetFloat("Action", 0.1f);
                    Agent.SetDestination(Target.transform.position);
                }
                else
                {
                   Target = DetectedEnemies[0];
                   Controller.SetFloat("Action", 0.1f);
                   Agent.SetDestination(Target.transform.position);
                }

                if(DetectedObjectives.Count == 0)
                {
                    return;
                }
                else
                {
                    
                }

                break;

            #endregion

            case 2: //Retreat / TP Home

             #region 2

                if (DetectedEnemies.Count == 0 && DetectedEntities.Count == 0)
                {
                    Controller.SetFloat("Action", 0);
                    if(TPTimer > 0)
                    {
                        TPTimer -= Time.fixedDeltaTime;
                        if(TPTimer <= 0)
                        {
                            this.gameObject.transform.position = HomeBase.transform.position;
                        }
                    }
                }

                if(DetectedEnemies.Count != 0)
                {
                    Debug.Log("Setting Target To base");
                    Target = HomePowerSource;
                    Agent.SetDestination(Target.transform.position);
                    Controller.SetFloat("Action", 0.1f);
                    return;
                }

                if (Vector3.Distance(this.gameObject.transform.position, Target.transform.position) <= StoppingDistance)
                {
                    CurrentHealth += (int)(CurrentMaxHealth * 0.2); //Regenning when at base
                    if(CurrentHealth > CurrentMaxHealth)
                    {
                        CurrentHealth = CurrentMaxHealth;
                    }

                    if (CurrentHealth == CurrentMaxHealth)
                    {
                        AIStateValue = 4;
                        TPTimer = 3;
                        return;
                    }
                }

                break;

            #endregion

            case 3: //ObjectivePush

             #region 3

                if (DetectedEnemies.Count == 0 && DetectedEntities.Count == 0 && DetectedObjectives.Count == 0)
                {
                    Target = EnemyCrystal;
                    Controller.SetFloat("Action", 0.1f);
                    Agent.SetDestination(Target.transform.position);
                }

                if (DetectedEntities.Count != 0)
                {
                    Target = DetectedEntities[0];
                    Controller.SetFloat("Action", 0.1f);
                    Agent.SetDestination(Target.transform.position);
                    if (Vector3.Distance(this.gameObject.transform.position, Target.transform.position) <= StoppingDistance)
                    {
                        AIStateValue = 4;
                    }
                }

                if (DetectedObjectives.Count != 0)
                {
                    Target = DetectedObjectives[0];
                    Controller.SetFloat("Action", 0.1f);
                    Agent.SetDestination(Target.transform.position);
                    if(Vector3.Distance(this.gameObject.transform.position, Target.transform.position) <= StoppingDistance)
                    {
                        AIStateValue = 4;
                    }
                }

                if (DetectedEnemies.Count != 0)
                {
                    if (CurrentHealth >= CurrentMaxHealth / 2)
                    {
                        Target = DetectedEnemies[0];
                        Controller.SetFloat("Action", 0.1f);
                        Agent.SetDestination(Target.transform.position);
                        if (Vector3.Distance(this.gameObject.transform.position, Target.transform.position) <= StoppingDistance)
                        {
                            AIStateValue = 4;
                        }
                    }
                    else
                    {
                        AIStateValue = 2;
                        return;
                    }
                }

                break;

            #endregion

            case 4: //Fighting

             #region 4

                Controller.SetFloat("Action", 0.3f);

                break;

            #endregion

            case 5:

                break;

            case 6:

                break;

            case 7:

                Controller.SetFloat("Action", 0.7f);

                break;
        }

        if (CurrentHealth <= 0 && DeathTimer <= 0)
        {
            Debug.Log("Firing");
            AIStateValue = 7;
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
            CurrentMaxHealth = BaseMaxHealth + Item.Health;
            CurrentHealthRegen = BaseHealthRegen + Item.HealthRegen;

            CurrentManaRegen = BaseManaRegen + Item.ManaRegen;
       }
    }

    public void AbilityAttack()
    {

    }
    public void BasicAttack()
    {
        if(Target != null)
        {
            switch (Target.tag)
            {
                case "Character":
                    Controller.SetFloat("Action", 0.3f);
                    Target.GetComponent<BaseCharacter>().CurrentHealth -= CurrentWeaponPower;

                    break;

                case "Minion":

                    Controller.SetFloat("Action", 0.3f);
                    Target.GetComponent<Minion>().Health -= (int)CurrentWeaponPower;

                    break;

                case "Turret":

                    Controller.SetFloat("Action", 0.3f);
                    Target.GetComponent<Turret_Base>().Health -= (int)CurrentWeaponPower;

                    break;
            }
        }   
    }

    public void CharacterDies()
    {
        Controller.SetFloat("Action", 0.7f);
        Debug.Log("Dead" + this.gameObject.name);
        this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        DeathTimer = 5;
        IsDead = true;       
        this.gameObject.transform.position = HomeBase.transform.position;
        Agent.isStopped = true;
        CurrentHealth = CurrentMaxHealth;
    }

    void CheckTargetHealth()
    {
        switch (Target.tag)
        {
            case "Character":

                if(Target.GetComponent<BaseCharacter>().CurrentHealth <= 0)
                {
                    DetectedEnemies.Remove(Target);
                    Target = null;
                }

                break;

            case "Minion":

                if (Target.GetComponent<Minion>().Health <= 0)
                {
                    DetectedEntities.Remove(Target);
                    Target = null;
                }

                break;
        }
    }
}
