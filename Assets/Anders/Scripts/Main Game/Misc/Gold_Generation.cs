using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold_Generation : MonoBehaviour
{
    public int Passive_Gold_Income;

    public float Timer;
    void Start()
    {
        Timer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        if(Timer <= 0)
        {
            this.gameObject.GetComponentInParent<PlayerInput>().Gold += Passive_Gold_Income;
            Timer = 1;
        }
    }
}
