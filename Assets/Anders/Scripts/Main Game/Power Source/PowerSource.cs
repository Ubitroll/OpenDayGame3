using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSource : MonoBehaviour
{
    public int Health;

    public bool HasExploded;

    public GameObject Explosion;

    public float Timer;

    private void Start()
    {
        Health = 1000;

        Timer = 2;
    }

    private void Update()
    {
        if(Health <= 0)
        {
            if (!HasExploded)
            {
                HasExploded = true;

                Instantiate(Explosion, this.transform);
                GameObject Instance = Instantiate(Explosion, this.transform.position, Quaternion.identity) as GameObject;
            }
            else
            {
                Timer -= Time.fixedDeltaTime;

                if (Timer <= 0)
                {
                    if (this.gameObject.GetComponent<Team_Assign>().Team != true)
                    {
                        PointSystem.Instance.AllyPoints += 15;
                    }
                    else
                    {
                        PointSystem.Instance.EnemyPoints += 15;
                    }
                }
            }
        }
    }
}
