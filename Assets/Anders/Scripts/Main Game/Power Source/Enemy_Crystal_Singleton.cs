using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Crystal_Singleton : MonoBehaviour
{
    private static Enemy_Crystal_Singleton _instance;

    public static Enemy_Crystal_Singleton Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
}
