using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    // Variables
    public int playerHealth;
    public int playerMaxHealth;
    public int playerArmour;
    public int attackRange;
    public int attackDamage;
    public float attackSpeed;
    public int resourceCurrent;
    public int resourceMax;
    private string enemyTag;

    // Cooldown timers
    private int channelTime;
    public int basicAttackChannelTime;
    public int ability1ChannelTime;
    public int ability2ChannelTime;
    public int ability3ChannelTime;
    private float timer1;
    private float timer2;
    private float timer3;
    private float timerBasic;
    public float basicAttackCooldown;
    public float ability1Cooldown;
    public float ability2Cooldown;
    public float ability3Cooldown;
    private bool basicAttackReady = true;
    public bool ability1Ready = true;
    public bool ability2Ready = true;
    public bool ability3Ready = true;

    // Ui settings
    public Slider healthBar;
    public Slider resourceBar;
    public Text team1Score;
    public Text team2Score;
    public Image characterImage;
    public Text ability1Timer;
    public Text ability2Timer;
    public Text ability3Timer;

    // Character bools
    private bool basicMelee = false;
    private bool basicRanged = false;
    public bool isSummoner = false;
    public bool isRange = false;
    public bool isMelee = false;
    public bool isMage = false;

    // Explosive Variables
    public GameObject bomb;
    bool bombSpawned = true;

    // Start is called before the first frame update
    void Start()
    {
        InitialiseUI();

        InitialiseTimers();
        
        // If melee or summoner set basic attack to melee
        if (isSummoner == true || isMelee == true)
        {
            basicMelee = true;
        }

        // If range or mage set basic attack to ranged
        if(isRange == true || isMage == true)
        {
            basicRanged = true;
        }

        // If in team one enemy is team2
        if(this.transform.tag == "Team1")
        {
            enemyTag = "Team2";
        }
        else if (this.transform.tag == "Team2")
        {
            enemyTag = "Team1";
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Basic Attack ready
        if(basicAttackReady == true)
        {
            // If basic attack triggered
            if(this.GetComponent<PlayerInput>().rightTriggerPressed == true)
            {
                // Set attack to not ready
                basicAttackReady = false;
                
                // If melee or range
                if (basicMelee == true)
                {
                    // makes 1 raycast forward
                    Ray ray = new Ray(transform.position, Vector3.forward);
                    RaycastHit hit;

                    // If raycast hits something
                    if (Physics.Raycast(ray, out hit, attackRange))
                    {
                        //If the raycast hits an enemy
                        if (hit.transform.tag == enemyTag)
                        {
                            // Reduce their health by an amount dictated by the players attack damage - the enemies armour. 
                            hit.transform.GetComponent<PlayerScript>().playerHealth = hit.transform.GetComponent<PlayerScript>().playerHealth - (attackDamage - hit.transform.GetComponent<PlayerScript>().playerArmour);
                        }
                    }
                }
                else if (basicRanged == true)
                {

                }
            }
        }
        // Else activate cool down timer
        else
        {
            
            if (timerBasic > 0)
            {
                timerBasic -= Time.fixedDeltaTime;
            }
            else
            {
                basicAttackReady = true;

                timerBasic = basicAttackCooldown;
            }
        }

        // Ability 1 is ready
        if (ability1Ready == true)
        {
            // If ability 1 used
            if(this.GetComponent<PlayerInput>().aPressed == true)
            {
                // Use ability
                ability1Ready = false;

                // Depending on character
                if (isSummoner == true)
                {
                    // Do summoner ability 1

                }

                if (isRange == true)
                {
                    // Set channel time
                    channelTime = ability1ChannelTime;

                    // Set to not do knockback
                    bomb.GetComponent<BombScript>().doesKnockack = false;

                    bombSpawned = true;

                    // Do range ability 1
                    StartCoroutine(RangeAbilityOne());
                }

                if (isMelee == true)
                {
                    // Do melee ability 1

                }

                if (isMage == true)
                {
                    // Do mage ability 1

                }
            }
        }
        // Else activate cool down timer
        else if (ability1Ready == false)
        {
            if (timer1 > 0)
            {
                timer1 -= Time.fixedDeltaTime;
            }
            else
            {
                ability1Ready = true;

                timer1 = ability1Cooldown;
            }
        }

        // Ability 2 is ready
        if (ability2Ready == true)
        {
            // If ability 2 used
            if (this.GetComponent<PlayerInput>().bPressed == true)
            {
                // Use ability
                ability2Ready = false;

                // Depending on character
                if (isSummoner == true)
                {
                    // Do summoner ability 2

                }

                if (isRange == true)
                {
                    // Set channel time
                    channelTime = ability2ChannelTime;

                    // Set to not do knockback
                    bomb.GetComponent<BombScript>().doesKnockack = true;

                    // Do range ability 1
                    StartCoroutine(RangeAbilityTwo());
                }

                if (isMelee == true)
                {
                    // Do melee ability 2

                }

                if (isMage == true)
                {
                    // Do mage ability 2

                }
            }
        }
        // Else activate cool down timer
        else
        {
            if (timer2 > 0)
            {
                timer2 -= Time.fixedDeltaTime;
            }
            else
            {
                ability2Ready = true;

                timer2 = ability2Cooldown;
            }
        }

        // Ability 3 is ready
        if (ability3Ready == true)
        {
            // If ability 3 used
            if (this.GetComponent<PlayerInput>().xPressed == true)
            {
                // Use ability
                ability3Ready = false;
                
                // Depending on character
                if (isSummoner == true)
                {
                    // Do summoner ability 3

                }

                if (isRange == true)
                {
                    // Do range ability 3

                }

                if (isMelee == true)
                {
                    // Do melee ability 3

                }

                if (isMage == true)
                {
                    // Do mage ability 3

                }
            }
        }
        // Else activate cool down timer
        else
        {
            if (timer3 > 0)
            {
                timer3 -= Time.fixedDeltaTime;
            }
            else
            {
                ability3Ready = true;

                timer3 = ability3Cooldown;
            }
        }

        // Update UI
        UpdateUI();
    }

    // UI initialise method
    void InitialiseUI()
    {
        healthBar.maxValue = playerMaxHealth;
        resourceBar.maxValue = resourceMax;
        ability1Timer.text = ability1Cooldown.ToString();
        ability2Timer.text = ability2Cooldown.ToString();
        ability3Timer.text = ability3Cooldown.ToString();
    }

    // UI update method
    void UpdateUI()
    {
        ability1Timer.text = timer1.ToString();
    }

    // Method to set up timers
    void InitialiseTimers()
    {
        // Set float timer1 to cool down timer
        timerBasic = basicAttackCooldown;

        // Set float timer1 to cool down timer
        timer1 = ability1Cooldown;

        // Set float timer1 to cool down timer
        timer2 = ability2Cooldown;

        // Set float timer1 to cool down timer
        timer3 = ability3Cooldown;
    }

    // Method for bomb detonation
    

    IEnumerator RangeAbilityOne()
    {
        

        if (bombSpawned == true)
        {
            // Wait till channeled then spawn bomb
            yield return new WaitForSeconds(channelTime);
            Instantiate(bomb, this.transform.position, Quaternion.identity);
            bombSpawned = false;
        }
        
    }

    IEnumerator RangeAbilityTwo()
    {
        // Wait till channeled then spawn bomb
        yield return new WaitForSeconds(channelTime);
        Instantiate(bomb, this.transform.position, Quaternion.identity);
    }

    IEnumerator RangeAbilityThree()
    {
        // Wait till channeled then spawn bomb
        yield return new WaitForSeconds(channelTime);
        Instantiate(bomb, this.transform.position, Quaternion.identity);
    }
}