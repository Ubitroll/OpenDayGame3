using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    public List<GameObject> Characters = new List<GameObject>();

    public GameObject MinionWaveGood, MinionWaveBad;
    public GameObject GoodCrystal, BadCrystal;
    public GameObject RightTurrets, LeftTurrets;

    public GameObject RightMinionSpawn, LeftMinionSpawn;

    public GameObject RightBase, LeftBase;

    public GameObject RightCrystalSpawn, LeftCrystalSpawn;

    private void Awake()
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

            RightTurrets.transform.GetComponent<Team_Assign>().Team = true;
            //RightTurrets.transform.GetChild(1).GetComponent<Team_Assign>().Team = true;
            //RightTurrets.transform.GetChild(2).GetComponent<Team_Assign>().Team = true;

            LeftTurrets.transform.GetComponent<Team_Assign>().Team = false;
            //LeftTurrets.transform.GetChild(1).GetComponent<Team_Assign>().Team = false;
            //LeftTurrets.transform.GetChild(2).GetComponent<Team_Assign>().Team = false;

            RightMinionSpawn.GetComponent<Team_Assign>().Team = true; //Only need to declare right spawner for the minions to work it out.

            RightBase.GetComponent<Team_Assign>().Team = true;

            DataTransferer.Instance.PlayerChoice.GetComponent<BaseCharacter>().HomeBase = RightBase;
            DataTransferer.Instance.AI_1_Choice.GetComponent<BaseCharacter>().HomeBase = RightBase;
            DataTransferer.Instance.AI_2_Choice.GetComponent<BaseCharacter>().HomeBase = LeftBase;
            DataTransferer.Instance.AI_3_Choice.GetComponent<BaseCharacter>().HomeBase = LeftBase;
        }
        if(AssignCrystalSpawn == 0)
        {
            GameObject Good_Crystal = Instantiate(GoodCrystal, new Vector3(LeftCrystalSpawn.transform.position.x, LeftCrystalSpawn.transform.position.y, LeftCrystalSpawn.transform.position.z), Quaternion.identity) as GameObject;
            GameObject Bad_Crystal = Instantiate(BadCrystal, new Vector3(RightCrystalSpawn.transform.position.x, RightCrystalSpawn.transform.position.y, RightCrystalSpawn.transform.position.z), Quaternion.identity) as GameObject;

            RightTurrets.transform.GetComponent<Team_Assign>().Team = false;
            //RightTurrets.transform.GetChild(1).GetComponent<Team_Assign>().Team = false;
            //RightTurrets.transform.GetChild(2).GetComponent<Team_Assign>().Team = false;

            LeftTurrets.transform.GetComponent<Team_Assign>().Team = true;
            //LeftTurrets.transform.GetChild(1).GetComponent<Team_Assign>().Team = true;
            //LeftTurrets.transform.GetChild(2).GetComponent<Team_Assign>().Team = true;

            RightMinionSpawn.GetComponent<Team_Assign>().Team = false; //Only need to declare right spawner for the minions to work it out.

            RightBase.GetComponent<Team_Assign>().Team = false;

            DataTransferer.Instance.PlayerChoice.GetComponent<BaseCharacter>().HomeBase = LeftBase;
            DataTransferer.Instance.AI_1_Choice.GetComponent<BaseCharacter>().HomeBase = LeftBase;
            DataTransferer.Instance.AI_2_Choice.GetComponent<BaseCharacter>().HomeBase = RightBase;
            DataTransferer.Instance.AI_3_Choice.GetComponent<BaseCharacter>().HomeBase = RightBase;
        }

        DataTransferer.Instance.PlayerChoice.GetComponent<Team_Assign>().Team = true;
        DataTransferer.Instance.AI_1_Choice.GetComponent<Team_Assign>().Team = true;
        DataTransferer.Instance.AI_2_Choice.GetComponent<Team_Assign>().Team = false;
        DataTransferer.Instance.AI_3_Choice.GetComponent<Team_Assign>().Team = false;
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
    void SpawnAllCharacters()
    {
        if(RightBase.GetComponent<Team_Assign>().Team == true)
        {
            GameObject Instance = Instantiate(DataTransferer.Instance.PlayerChoice, RightBase.transform.GetChild(0).transform.position, Quaternion.identity) as GameObject;
            GameObject Instance1 = Instantiate(DataTransferer.Instance.AI_1_Choice, RightBase.transform.GetChild(1).transform.position, Quaternion.identity) as GameObject;
            GameObject Instance2 = Instantiate(DataTransferer.Instance.AI_2_Choice, LeftBase.transform.GetChild(0).transform.position, Quaternion.identity) as GameObject;
            GameObject Instance3 = Instantiate(DataTransferer.Instance.AI_3_Choice, LeftBase.transform.GetChild(1).transform.position, Quaternion.identity) as GameObject;
        }
        else
        {
            GameObject Instance = Instantiate(DataTransferer.Instance.PlayerChoice, LeftBase.transform.GetChild(0).transform.position, Quaternion.identity) as GameObject;
            GameObject Instance1 = Instantiate(DataTransferer.Instance.AI_1_Choice, LeftBase.transform.GetChild(1).transform.position, Quaternion.identity) as GameObject;
            GameObject Instance2 = Instantiate(DataTransferer.Instance.AI_2_Choice, RightBase.transform.GetChild(0).transform.position, Quaternion.identity) as GameObject;
            GameObject Instance3 = Instantiate(DataTransferer.Instance.AI_3_Choice, RightBase.transform.GetChild(1).transform.position, Quaternion.identity) as GameObject;
        }
    }

    void StartGame()
    {
        SpawnMinionWave();
        SpawnAllCharacters();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartGame();
        }
    }
}
