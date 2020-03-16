using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    // Variables
    public int playerHealth;
    public int playerArmour;
    public int attackDamage;
    public int resourceCurrent;
    public int resourceMax;

    // Cooldown timers
    public float ability1Cooldown;
    public float ability2Cooldown;
    public float ability3Cooldown;

    // Ui settings
    public Slider healthBar;
    public Slider resourceSlider;
    public Text team1Score;
    public Text team2Score;
    public Image characterImage;
    public Image ability1Image;
    public Image ability2Image;
    public Image ability3Image;

    // Start is called before the first frame update
    void Start()
    {
        InitialiseUI();
    }

    // Update is called once per frame
    void Update()
    {
        // Ability One used


    }

    // UI method
    void InitialiseUI()
    {

    }

    // UI update method
    void UpdateUI()
    {

    }
}