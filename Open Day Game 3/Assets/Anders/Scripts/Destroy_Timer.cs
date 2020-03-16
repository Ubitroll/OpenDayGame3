using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Timer : MonoBehaviour
{
    public float Timer;

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.fixedDeltaTime;
        if(Timer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
