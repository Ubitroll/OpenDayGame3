using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    public int AllyPoints, EnemyPoints;

    public int MaxPoints;

    public GameObject GUIManager;

    private static PointSystem _instance;

    public static PointSystem Instance { get { return _instance; } }

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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AllyPoints += 1;
        }

        GUIManager.GetComponent<GUIManager>().AllyPoints.text = AllyPoints.ToString();
        GUIManager.GetComponent<GUIManager>().EnemyPoints.text = EnemyPoints.ToString();

        if (AllyPoints >= MaxPoints)
        {
            Debug.Log("Ally Wins");
        }
        if(EnemyPoints >= MaxPoints)
        {
            Debug.Log("Enemy Wins");
        }
    }
}
