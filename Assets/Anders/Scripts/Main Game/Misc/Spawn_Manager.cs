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

    public float MinionSpawnTimer;

    private void Awake()
    {
        AssigningTeam();
        MinionSpawnTimer = 20;
    }

    private void Start()
    {
        StartGame();
    }

    void AssigningTeam() //(True/1) = Right (False/0) = Left.
    {
        int AssignCrystalSpawn = Random.Range(0,2);
        Debug.Log(AssignCrystalSpawn);

        if (AssignCrystalSpawn == 1)
        {
            GameObject Good_Crystal = Instantiate(GoodCrystal, new Vector3(RightCrystalSpawn.transform.position.x, RightCrystalSpawn.transform.position.y, RightCrystalSpawn.transform.position.z), Quaternion.Euler(0, 90, 0)) as GameObject;
            GameObject Bad_Crystal = Instantiate(BadCrystal, new Vector3(LeftCrystalSpawn.transform.position.x, LeftCrystalSpawn.transform.position.y, LeftCrystalSpawn.transform.position.z), Quaternion.Euler(0, -90, 0)) as GameObject;

            RightTurrets.transform.GetComponent<Team_Assign>().Team = true;

            LeftTurrets.transform.GetComponent<Team_Assign>().Team = false;


            RightMinionSpawn.GetComponent<Team_Assign>().Team = true; //Only need to declare right spawner for the minions to work it out.

            RightBase.GetComponent<Team_Assign>().Team = true;

            DataTransferer.Instance.PlayerChoice.GetComponent<PlayerInput>().HomeBase = RightBase;
            // DataTransferer.Instance.AI_1_Choice.GetComponent<BaseCharacter>().HomeBase = LeftBase;
        }
        if (AssignCrystalSpawn == 0)
        {
            GameObject Good_Crystal = Instantiate(GoodCrystal, new Vector3(LeftCrystalSpawn.transform.position.x, LeftCrystalSpawn.transform.position.y, LeftCrystalSpawn.transform.position.z), Quaternion.Euler(0,-90,0)) as GameObject;
            GameObject Bad_Crystal = Instantiate(BadCrystal, new Vector3(RightCrystalSpawn.transform.position.x, RightCrystalSpawn.transform.position.y, RightCrystalSpawn.transform.position.z), Quaternion.Euler(0, 90, 0)) as GameObject;

            RightTurrets.transform.GetComponent<Team_Assign>().Team = false;

            LeftTurrets.transform.GetComponent<Team_Assign>().Team = true;

            RightMinionSpawn.GetComponent<Team_Assign>().Team = false; //Only need to declare right spawner for the minions to work it out.

            RightBase.GetComponent<Team_Assign>().Team = false;

            DataTransferer.Instance.PlayerChoice.GetComponent<PlayerInput>().HomeBase = LeftBase;
            //DataTransferer.Instance.AI_1_Choice.GetComponent<BaseCharacter>().HomeBase = RightBase;
        }

        DataTransferer.Instance.PlayerChoice.GetComponent<Team_Assign>().Team = true;
        //DataTransferer.Instance.AI_1_Choice.GetComponent<Team_Assign>().Team = false;
    }


    void SpawnMinionWave()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        for(int i = 0; i < 3; i++) //Spawns upto 3 minions this can increase or decrease.
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

            yield return new WaitForSeconds(1); //Waits a second before spawning a new minion.
        }
    }
    void SpawnAllCharacters()
    {
        Debug.Log("Spawning Characters");

        if(RightBase.GetComponent<Team_Assign>().Team == true)
        {
            Debug.Log("Spawning Characters Right");
            GameObject Instance = Instantiate(DataTransferer.Instance.PlayerChoice, RightBase.transform.position, Quaternion.identity) as GameObject;
            //GameObject Instance1 = Instantiate(DataTransferer.Instance.AI_1_Choice, LeftBase.transform.GetChild(0).transform.position, Quaternion.identity) as GameObject;
        }
        else
        {
            Debug.Log("Spawning Characters left");
            GameObject Instance = Instantiate(DataTransferer.Instance.PlayerChoice, LeftBase.transform.position, Quaternion.identity) as GameObject;
            //GameObject Instance1 = Instantiate(DataTransferer.Instance.AI_1_Choice, RightBase.transform.GetChild(0).transform.position, Quaternion.identity) as GameObject;
        }
    }

    void StartGame()
    {
        SpawnMinionWave();
        SpawnAllCharacters();
    }


    private void Update()
    {        
        MinionSpawnTimer -= Time.fixedDeltaTime;
        if(MinionSpawnTimer <= 0)
        {
            SpawnMinionWave();
            MinionSpawnTimer = 20;
        }
    }
}
