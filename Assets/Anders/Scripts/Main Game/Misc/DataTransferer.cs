using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTransferer : MonoBehaviour
{
    public GameObject PlayerChoice, AI_1_Choice, AI_2_Choice, AI_3_Choice;

    private static DataTransferer _instance;

    public static DataTransferer Instance { get { return _instance; } }

    private void Awake() // Singleton Pattern.
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

    void Start()
    {
        DontDestroyOnLoad(this.gameObject); 
    }

    void Update()
    {
        
    }
}
