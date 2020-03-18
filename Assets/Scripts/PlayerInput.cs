using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Variables for controlls
    // Set to automatically resort to controller 1
    
    // Booleans
    // Set to public incase there is an error and one is stuck on
    public bool leftStick = true;
    public bool rightStick = true;
    public bool dPad = true;
    public bool aPressed, bPressed, xPressed, yPressed = false;
    public bool dPadUp, dPadDown, dPadLeft, dPadRight = false;
    public bool selectPressed, pausePressed = false;
    
    // Integer
    // An Integer to hold the player number
    public int playerNumber = 1;
    
    // Floats
    // Used to change the speed of certain actions
    public float runSpeed = 0;
    public float sensitivityAiming = 0;
    
    // Gameobjects
    // 
    public GameObject worldDirection;
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

    // Start is called before the first frame update
    void Start()
    {
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

        // Set player transform
        playerTransform = transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        camera.transform.position = new Vector3(playerTransform.position.x, 12, playerTransform.position.z - 3);

        // If the left stick is set to respond
        if (leftStick)
        {
            // Takes in the input direction
            Vector3 moveDirection = Vector3.zero;
            moveDirection.x = Input.GetAxis(moveX);
            moveDirection.z = Input.GetAxis(moveY);

            playerTransform.position = playerTransform.position + (worldDirection.transform.forward * moveDirection.z + worldDirection.transform.right * moveDirection.x) * (Time.deltaTime * runSpeed);
        }

        // If the right stick is set to respond
        if (rightStick)
        {
            // Move the direction of attack towards the same direction the stick is facing
            

            var lookDirection = Mathf.Atan2(Input.GetAxis(horizontal), -Input.GetAxis(vertical)) * Mathf.Rad2Deg;
            
            playerTransform.rotation = Quaternion.Euler(0, lookDirection, 0);   
        }

        // If the Dpad is activated
        if (dPad)
        {
            // Take in the input direction
            Vector3 dPadDirection = Vector3.zero;
            dPadDirection.x = Input.GetAxis(dPadLeftRight);
            dPadDirection.z = Input.GetAxis(dPadUpDown);

            // If the right Dpad is pressed
            if (dPadDirection.x > 0)
            {
                dPadRight = true;
                dPadLeft = false;
            }

            // If the left Dpad is pressed
            if (dPadDirection.x < 0)
            {
                dPadLeft = true;
                dPadRight = false;
            }

            // If neither left or right Dpad is pressed
            if (dPadDirection.x == 0)
            {
                dPadLeft = false;
                dPadRight = false;
            }

            // If the up Dpad is pressed
            if (dPadDirection.z > 0)
            {
                dPadUp = true;
                dPadDown = false;
            }

            // If the down Dpad is pressed
            if (dPadDirection.z < 0)
            {
                dPadDown = true;
                dPadUp = false;
            }

            // If neither up or down Dpad is pressed
            if (dPadDirection.z == 0)
            {
                dPadUp = false;
                dPadDown = false;
            }
        }

        // If a button is pressed
        if (Input.GetButton(aButton))
        {
            aPressed = true;
        }
        else
        {
            aPressed = false;
        }

        // If b button is pressed
        if (Input.GetButton(bButton))
        {
            bPressed = true;
        }
        else
        {
            bPressed = false;
        }

        // If x button is pressed
        if (Input.GetButton(xButton))
        {
            xPressed = true;
        }
        else
        {
            xPressed = false;
        }

        // If y button is pressed
        if (Input.GetButton(yButton))
        {
            yPressed = true;
        }
        else
        {
            yPressed = false;
        }

        // If select button is pressed
        if (Input.GetButton(select))
        {
            selectPressed = true;
        }
        else
        {
            selectPressed = false;
        }
       
        // If pause button is pressed
        if (Input.GetButton(pause))
        {
            pausePressed = true;
        }
        else
        {
            pausePressed = false;
        }
    }
}