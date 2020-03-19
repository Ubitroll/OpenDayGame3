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
    public float basicAttackCooldown;
    public float ability1Cooldown;
    public float ability2Cooldown;
    public float ability3Cooldown;
    private bool basicAttackReady = true;
    private bool ability1Ready = true;
    private bool ability2Ready = true;
    private bool ability3Ready = true;

    // Ui settings
    public Slider healthBar;
    public Slider resourceBar;
    public Text team1Score;
    public Text team2Score;
    public Image characterImage;
    public Image ability1Image;
    public Image ability2Image;
    public Image ability3Image;

    // Character bools
    private bool basicMelee = false;
    private bool basicRanged = false;
    private bool isSummoner = false;
    private bool isRange = false;
    private bool isMelee = false;
    private bool isMage = false;
    
    // Start is called before the first frame update
    void Start()
    {
        InitialiseUI();
        
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

        // Ability 1 is ready
        if(ability1Ready == true)
        {
            // If ability 1 used
            if(this.GetComponent<PlayerInput>().aPressed == true)
            {
                // Depending on character
                if (isSummoner == true)
                {
                    // Do summoner ability 1

                }

                if (isRange == true)
                {
                    // Do range ability 1

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
        else
        {
            // Set float timer1 to cool down timer
            float timer1 = ability1Cooldown;

            if (timer1 > 0)
            {
                timer1 -= Time.fixedDeltaTime;
            }
            else
            {
                ability1Ready = true;
            }
        }

        // Ability 2 is ready
        if (ability2Ready == true)
        {
            // If ability 2 used
            if (this.GetComponent<PlayerInput>().bPressed == true)
            {
                // Depending on character
                if (isSummoner == true)
                {
                    // Do summoner ability 2

                }

                if (isRange == true)
                {
                    // Do range ability 2

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
            // Set float timer1 to cool down timer
            float timer2 = ability2Cooldown;

            if (timer2 > 0)
            {
                timer2 -= Time.fixedDeltaTime;
            }
            else
            {
                ability2Ready = true;
            }
        }

        // Ability 3 is ready
        if (ability3Ready == true)
        {
            // If ability 3 used
            if (this.GetComponent<PlayerInput>().xPressed == true)
            {
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
            // Set float timer1 to cool down timer
            float timer3 = ability3Cooldown;

            if (timer3 > 0)
            {
                timer3 -= Time.fixedDeltaTime;
            }
            else
            {
                ability3Ready = true;
            }
        }
    }

    // UI method
    void InitialiseUI()
    {
        healthBar.maxValue = playerMaxHealth;
        resourceBar.maxValue = resourceMax;
    }

    // UI update method
    void UpdateUI()
    {

    }
}