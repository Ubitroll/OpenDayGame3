using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Base : MonoBehaviour
{
    #region Beam Vars

    public List<GameObject> Beam_Database = new List<GameObject>();

    private GameObject beamLineRendererPrefab;
    private GameObject beamStartPrefab;
    private GameObject beamEndPrefab;

    private int currentBeam = 0;

    private GameObject beamStart;
    private GameObject beamEnd;
    private GameObject beam;

    private LineRenderer line;

    public float beamEndOffset = 1f; //How far from the raycast hit point the end effect is positioned
    public float textureScrollSpeed = 8f; //How fast the texture scrolls along the beam
    public float textureLengthScale = 3; //Length of the beam texture

    #endregion

    #region Turret Vars

    [SerializeField]
    private float Fire_Timer = 6;
    [SerializeField]
    private float Explosion_Timer = 1f;

    public bool Team;

    private bool HasFired;
    private bool HasExploded;
    

    public List<GameObject> DetectedEnemies = new List<GameObject>();

    public GameObject TurretFirePoint;

    public List<GameObject> ParticleEffectsDatabase = new List<GameObject>();

    public List<GameObject> Explosions = new List<GameObject>();

    [SerializeField]
    private List<GameObject> UsingParticles = new List<GameObject>();

    public int Health;

    #endregion

    #region Turret Part References

    public MeshRenderer TurretBody;
    public MeshRenderer TurretTop;
    public GameObject Owl;


    #endregion

    void Start()
    {
        Health = 1000;

        UsingParticles = ParticleEffectsDatabase;

        //if(OnBlueTeam)
        //{
            beamStartPrefab = Beam_Database[1];
            beamEndPrefab = Beam_Database[2];
            beamLineRendererPrefab = Beam_Database[0];
        //}
        //else
        //{
            //beamStart = Beam_Database[3];
            //beamEnd = Beam_Database[4];
            //beam = Beam_Database[5];
        //}
    }

    void Update()
    {       
        if(DetectedEnemies.Count != 0)
        {
            if (DetectedEnemies[0] == null)
            {
                DetectedEnemies.RemoveAt(0);
                ResetTurret();
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Health = 0;
            GameObject Instance = Instantiate(Explosions[0], new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity) as GameObject;
        }

        if (HasFired)
        {
            ShootBeamInDir(TurretFirePoint.transform.position, DetectedEnemies[0].transform.position);

            if(DetectedEnemies[0].gameObject.tag == "Minion")
            {
                DetectedEnemies[0].GetComponent<Minion>().Health -= 1;
                if(DetectedEnemies[0].GetComponent<Minion>().Health <= 0)
                {
                    DetectedEnemies.RemoveAt(0);
                    ResetTurret();
                }
            }
        }

        if (DetectedEnemies.Count != 0)
        {
            if (!HasExploded)
            {
                if (Fire_Timer == 6f)
                {
                    Instantiate(UsingParticles[0], TurretFirePoint.transform);
                    Fire_Timer -= Time.fixedDeltaTime;
                }
                else
                {
                    Fire_Timer -= Time.fixedDeltaTime;

                    if (Fire_Timer <= 1)
                    {
                        if (!HasFired)
                        {
                            Destroy(TurretFirePoint.transform.GetChild(0).gameObject);
                            HasFired = true;
                            Instantiate(UsingParticles[1], TurretFirePoint.transform);

                            beamStart = Instantiate(beamStartPrefab, TurretFirePoint.transform);
                            beamEnd = Instantiate(beamEndPrefab, DetectedEnemies[0].transform);
                            beam = Instantiate(beamLineRendererPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                            line = beam.GetComponent<LineRenderer>();
                        }
                    }

                    if (Fire_Timer <= 0f)
                    {
                        HasFired = false;
                        ResetTurret();
                    }

                }
            }
        }

        if(Health <= 0)
        {
            if (!HasExploded)
            {
                if(this.gameObject.GetComponent<Team_Assign>().Team == true)
                {
                    PointSystem.Instance.GetComponent<PointSystem>().EnemyPoints += 3;
                }
                else
                {
                    PointSystem.Instance.GetComponent<PointSystem>().AllyPoints += 3; ;
                }

                HasExploded = true;

                ResetTurret();

                GameObject Instance = Instantiate(Explosions[0], new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity) as GameObject;

                for(int i = 0; i < DetectedEnemies.Count; i++)
                {
                    switch (DetectedEnemies[i].tag)
                    {
                        case "Minion":

                            DetectedEnemies[i].GetComponent<Minion>().Health -= 100 / 4; //This 25 damage can be modified when balancing.

                            break;

                        case "Character":

                            DetectedEnemies[i].GetComponent<BaseCharacter>().CurrentHealth -= 100 / 4;

                            break;
                    }
                }
            }
        }

        if (HasExploded)
        {
            Explosion_Timer -= Time.fixedDeltaTime;
            if(Explosion_Timer <= 0)
            {
                Destroy(TurretBody);
                Destroy(TurretTop);
                Destroy(Owl);               
                this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
                Destroy(this.gameObject);
            }
        }
    }

    void ShootBeamInDir(Vector3 start, Vector3 dir)
    {
        line.positionCount = 2;
        line.SetPosition(0, start);
        beamStart.transform.position = start;

        Vector3 end = Vector3.zero;

        end = DetectedEnemies[0].transform.position;

        beamEnd.transform.position = end;
        line.SetPosition(1, end);

        beamStart.transform.LookAt(beamEnd.transform.position);
        beamEnd.transform.LookAt(beamStart.transform.position);

        float distance = Vector3.Distance(start, end);
        line.sharedMaterial.mainTextureScale = new Vector2(distance / textureLengthScale, 1);
        line.sharedMaterial.mainTextureOffset -= new Vector2(Time.deltaTime * textureScrollSpeed, 0);
    }

    public void ResetTurret()
    {
        HasFired = false;
        Destroy(beamStart);
        Destroy(beamEnd);
        Destroy(beam);
        Destroy(TurretFirePoint.transform.GetChild(0).gameObject);
        Fire_Timer = 6f;
    }
}
