using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public Image HeroIcon;
    public Image HeroAbilityIcons;

    public Image[] AbilityCover;

    public Text Ability_1_Timer, Ability_2_Timer , Ability_3_Timer;

    public GameObject Player;
    void Start()
    {
    }

    private void Update()
    {
        if(Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }


        if(Player.GetComponent<Abilities>().AbilityACoolDown > 0)
        {
            AbilityCover[0].color = new Color(1, 1, 1, 0.8f);
        }
        else
        {
            AbilityCover[0].color = new Color(1, 1, 1, 0);
        }

        if (Player.GetComponent<Abilities>().AbilityBCoolDown > 0)
        {
            AbilityCover[1].color = new Color(1, 1, 1, 0.8f);
        }
        else
        {
            AbilityCover[1].color = new Color(1, 1, 1, 0);
        }

        if (Player.GetComponent<Abilities>().UltimateCoolDown > 0)
        {
            AbilityCover[2].color = new Color(1, 1, 1, 0.8f);
        }
        else
        {
            AbilityCover[2].color = new Color(1, 1, 1, 0);
        }

        Ability_1_Timer.text = Player.GetComponent<Abilities>().AbilityACoolDown.ToString();
        Ability_2_Timer.text = Player.GetComponent<Abilities>().AbilityBCoolDown.ToString();
        Ability_3_Timer.text = Player.GetComponent<Abilities>().UltimateCoolDown.ToString();
    }
}
