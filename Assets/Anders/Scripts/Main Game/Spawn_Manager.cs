using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    public GameObject MinionWaveGood, MinionWaveBad;
    public GameObject GoodCrystal, BadCrystal;
    public GameObject RightTurrets, LeftTurrets;

    public GameObject RightMinionSpawn, LeftMinionSpawn;

    public GameObject RightCrystalSpawn, LeftCrystalSpawn;

    private void Start()
    {
        AssigningTeam();
    }

    void AssigningTeam() //(True/1) = Right (False/0) = Left.
    {
        int AssignCrystalSpawn = Random.Range(0,2);
        Debug.Log(AssignCrystalSpawn);

        if (AssignCrystalSpawn == 1)
        {
            GameObject Good_Crystal = Instantiate(GoodCrystal, new Vector3(RightCrystalSpawn.transform.position.x, RightCrystalSpawn.transform.position.y, RightCrystalSpawn.transform.position.z), Quaternion.identity) as GameObject;
            GameObject Bad_Crystal = Instantiate(BadCrystal, new Vector3(LeftCrystalSpawn.transform.position.x, LeftCrystalSpawn.transform.position.y, LeftCrystalSpawn.transform.position.z), Quaternion.identity) as GameObject;

            RightTurrets.transform.GetChild(0).GetComponent<Team_Assign>().Team = true;
            //RightTurrets.transform.GetChild(1).GetComponent<Team_Assign>().Team = true;
            //RightTurrets.transform.GetChild(2).GetComponent<Team_Assign>().Team = true;

            LeftTurrets.transform.GetChild(0).GetComponent<Team_Assign>().Team = false;
            //LeftTurrets.transform.GetChild(1).GetComponent<Team_Assign>().Team = false;
            //LeftTurrets.transform.GetChild(2).GetComponent<Team_Assign>().Team = false;

            RightMinionSpawn.GetComponent<Team_Assign>().Team = true; //Only need to declare right spawner for the minions to work it out.
        }
        if(AssignCrystalSpawn == 0)
        {
            GameObject Good_Crystal = Instantiate(GoodCrystal, new Vector3(LeftCrystalSpawn.transform.position.x, LeftCrystalSpawn.transform.position.y, LeftCrystalSpawn.transform.position.z), Quaternion.identity) as GameObject;
            GameObject Bad_Crystal = Instantiate(BadCrystal, new Vector3(RightCrystalSpawn.transform.position.x, RightCrystalSpawn.transform.position.y, RightCrystalSpawn.transform.position.z), Quaternion.identity) as GameObject;

            RightTurrets.transform.GetChild(0).GetComponent<Team_Assign>().Team = false;
            //RightTurrets.transform.GetChild(1).GetComponent<Team_Assign>().Team = false;
            //RightTurrets.transform.GetChild(2).GetComponent<Team_Assign>().Team = false;

            LeftTurrets.transform.GetChild(0).GetComponent<Team_Assign>().Team = true;
            //LeftTurrets.transform.GetChild(1).GetComponent<Team_Assign>().Team = true;
            //LeftTurrets.transform.GetChild(2).GetComponent<Team_Assign>().Team = true;

            RightMinionSpawn.GetComponent<Team_Assign>().Team = false; //Only need to declare right spawner for the minions to work it out.
        }
    }


    void SpawnMinionWave()
    {
        if (RightMinionSpawn.GetComponent<Team_Assign>().Team == true) //Figures out which side is good which is bad!
        {
            GameObject Minion_Wave_Good = Instantiate(MinionWaveGood, new Vector3(RightMinionSpawn.transform.position.x, RightMinionSpawn.transform.position.y, RightMinionSpawn.transform.position.z), Quaternion.identity) as GameObject;
            GameObject Minion_Wave_Bad = Instantiate(MinionWaveBad, new Vector3(LeftMinionSpawn.transform.position.x, LeftMinionSpawn.transform.position.y, LeftMinionSpawn.transform.position.z), Quaternion.identity) as GameObject;
        }
        else
        {
            GameObject Minion_Wave_Good = Instantiate(MinionWaveGood, new Vector3(LeftMinionSpawn.transform.position.x, LeftMinionSpawn.transform.position.y, LeftMinionSpawn.transform.position.z), Quaternion.identity) as GameObject;
            GameObject Minion_Wave_Bad = Instantiate(MinionWaveBad, new Vector3(RightMinionSpawn.transform.position.x, RightMinionSpawn.transform.position.y, RightMinionSpawn.transform.position.z), Quaternion.identity) as GameObject;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnMinionWave();
        }
    }
}
