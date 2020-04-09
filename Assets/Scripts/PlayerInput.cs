using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float SpeedController;

    private Animator Animator;

    public string ActionValue;

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

    public int CurrentWeaponPower;
    public int BaseWeaponPower;

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

    #region NonEssential Vars
    // Variables for controlls
    // Set to automatically resort to controller 1

    // Booleans
    // Set to public incase there is an error and one is stuck on
    public bool leftStick = true;
    public bool rightStick = true;
    public bool dPad = true;
    public bool triggers = true;
    public bool leftTriggerPressed, rightTriggerPressed = false;
    public bool aPressed, bPressed, xPressed, yPressed = false;
    public bool dPadUp, dPadDown, dPadLeft, dPadRight = false;
    public bool selectPressed, pausePressed = false;
    
    // Integer
    // An Integer to hold the player number
    public int playerNumber = 1;
    public int cameraOffset;
    
    // Floats
    // Used to change the speed of certain actions
    public float runSpeed = 0;
    
    // Gameobjects 
    public GameObject camera;

    // Privates
    private Vector3 startPos;
    private Transform playerTransform;
    
    // Strings
    // Set to private so editor is less cluttered
    private string moveX;
    private string moveY;
    private string horizontal;
    private string vertical;
    private string aButton;
    private string bButton;
    private string xButton;
    private string yButton;
    private string dPadUpDown;
    private string dPadLeftRight;
    private string select;
    private string pause;
    private string leftTrigger;
    private string rightTrigger;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region Controller Input Var Setup

        // Set controller inputs
        moveX = "C" + playerNumber.ToString() + "moveX";
        moveY = "C" + playerNumber.ToString() + "moveY";
        horizontal = "C" + playerNumber.ToString() + "horizontal";
        vertical = "C" + playerNumber.ToString() + "vertical";
        aButton = "C" + playerNumber.ToString() + "A";
        bButton = "C" + playerNumber.ToString() + "B";
        xButton = "C" + playerNumber.ToString() + "X";
        yButton = "C" + playerNumber.ToString() + "Y";
        dPadUpDown = "C" + playerNumber.ToString() + "DpadUD";
        dPadLeftRight = "C" + playerNumber.ToString() + "DpadLR";
        select = "C" + playerNumber.ToString() + "Select";
        pause = "C" + playerNumber.ToString() + "Pause";
        leftTrigger = "C" + playerNumber.ToString() + "LT";
        rightTrigger = "C" + playerNumber.ToString() + "RT";

        #endregion

        #region Character Stat Set up

        CurrentWeaponPower = BaseWeaponPower;

        #endregion

        playerTransform = transform; // Set player transform

        Animator = this.gameObject.GetComponent<Animator>();

        camera = GameObject.FindGameObjectWithTag("MainCamera");

    }

    // Update is called once per frame
    void Update()
    {
        camera.transform.position = new Vector3(playerTransform.position.x + 4, playerTransform.position.y + 6, playerTransform.position.z); //Setting Camera Position

        if (Input.GetAxisRaw(rightTrigger) > 0) //Basic Attack Handler.
        { 
            rightTriggerPressed = true;
            ActionValue = "BasicAttack";
        }
        else
        {
            rightTriggerPressed = false;
            ActionValue = "Idle";
        }
        
        if(ActionValue != "BasicAttack")
        {
            if (leftStick)
            {
                if (Input.GetAxis(moveX) == 0 && Input.GetAxis(moveY) == 0)
                {
                    ActionValue = "Idle";
                }
                if (Input.GetAxis(moveX) != 0 || Input.GetAxis(moveY) != 0)
                {
                    ActionValue = "Jog";
                    if (Input.GetAxis(moveX) != 0 && Input.GetAxis(moveY) != 0) //Remove a small bug of moving faster at angles.
                    {
                        playerTransform.position += new Vector3(((-Input.GetAxis(moveY)) * (SpeedController / 1.5f)), 0, ((Input.GetAxis(moveX)) * (SpeedController / 1.5f)));
                    }
                    else
                    {
                        playerTransform.position += new Vector3(-Input.GetAxis(moveY) * SpeedController, 0, Input.GetAxis(moveX) * SpeedController);
                    }
                    playerTransform.eulerAngles = new Vector3(0, Mathf.Atan2(-Input.GetAxis(moveY), Input.GetAxis(moveX)) * 180 / Mathf.PI, 0); //Rotates the Character to the angle of the left stick if we stick with it... Get it? Stick with it... Ok bad joke....
                }
            }
        }

        #region Button Input
        ////if (rightStick)
        ////{
        ////    if (Input.GetAxis(horizontal) != 0 || Input.GetAxis(vertical) != 0)
        ////    {
        ////        playerTransform.eulerAngles = new Vector3(0, Mathf.Atan2(Input.GetAxis(vertical), Input.GetAxis(horizontal)) * 180 / Mathf.PI, 0);
        ////    }
        ////}

        //// If the Dpad is activated
        //if (dPad)
        //{
        //    // Take in the input direction
        //    Vector3 dPadDirection = Vector3.zero;
        //    dPadDirection.x = Input.GetAxis(dPadLeftRight);
        //    dPadDirection.z = Input.GetAxis(dPadUpDown);

        //    // If the right Dpad is pressed
        //    if (dPadDirection.x > 0)
        //    {
        //        dPadRight = true;
        //        dPadLeft = false;
        //    }

        //    // If the left Dpad is pressed
        //    if (dPadDirection.x < 0)
        //    {
        //        dPadLeft = true;
        //        dPadRight = false;
        //    }

        //    // If neither left or right Dpad is pressed
        //    if (dPadDirection.x == 0)
        //    {
        //        dPadLeft = false;
        //        dPadRight = false;
        //    }

        //    // If the up Dpad is pressed
        //    if (dPadDirection.z > 0)
        //    {
        //        dPadUp = true;
        //        dPadDown = false;
        //    }

        //    // If the down Dpad is pressed
        //    if (dPadDirection.z < 0)
        //    {
        //        dPadDown = true;
        //        dPadUp = false;
        //    }

        //    // If neither up or down Dpad is pressed
        //    if (dPadDirection.z == 0)
        //    {
        //        dPadUp = false;
        //        dPadDown = false;
        //    }
        //}

        //// If a button is pressed
        //if (Input.GetButton(aButton))
        //{
        //    aPressed = true;
        //}
        //else
        //{
        //    aPressed = false;
        //}

        //// If b button is pressed
        //if (Input.GetButton(bButton))
        //{
        //    bPressed = true;
        //}
        //else
        //{
        //    bPressed = false;
        //}

        //// If x button is pressed
        //if (Input.GetButton(xButton))
        //{
        //    xPressed = true;
        //}
        //else
        //{
        //    xPressed = false;
        //}

        //// If y button is pressed
        //if (Input.GetButton(yButton))
        //{
        //    yPressed = true;
        //}
        //else
        //{
        //    yPressed = false;
        //}

        //// If select button is pressed
        //if (Input.GetButton(select))
        //{
        //    selectPressed = true;
        //}
        //else
        //{
        //    selectPressed = false;
        //}
       
        //// If pause button is pressed
        //if (Input.GetButton(pause))
        //{
        //    pausePressed = true;
        //}
        //else
        //{
        //    pausePressed = false;
        //}

        #endregion

        ChangeAnimationState();
    }

    void ChangeAnimationState()
    {
        switch (ActionValue)
        {
            case "Idle":

                Animator.SetFloat("Action", 0);

                break;

            case "Jog":

                Animator.SetFloat("Action", 0.1f);

                break;

            case "Sprint":

                Animator.SetFloat("Action", 0.2f);

                break;

            case "BasicAttack":

                Animator.SetFloat("Action", 0.3f);

                break;

            case "Ability1":

                Animator.SetFloat("Action", 0.4f);

                break;

            case "Ability2":

                Animator.SetFloat("Action", 0.5f);

                break;

            case "Ability3":

                Animator.SetFloat("Action", 0.6f);

                break;

            case "Death":

                Animator.SetFloat("Action", 0.7f);

                break;

            case "Celebrate":

                Animator.SetFloat("Action", 0.8f);

                break;
        }
    }
}