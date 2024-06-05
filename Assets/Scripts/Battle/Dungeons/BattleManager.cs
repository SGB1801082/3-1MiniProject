using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    private static BattleManager instance = null;
    public ObjectManager pool;
    public RoomManager room;
    public UIManager ui;
    public List<GameObject> party_List = new List<GameObject>();
    public List<GameObject> deploy_Player_List = new List<GameObject>();
    public List<GameObject> deploy_Enemy_List = new List<GameObject>();
    public GameObject deploy_area;
    public GameObject unit_deploy_area;
    public bool isFirstEnter;
    private bool battleEnded = false;

    public static BattleManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }

    }


    public enum BattlePhase
    {
        Start,
        Deploy,
        Rest,
        Battle,
        End
    }


    public BattlePhase _curphase;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        room = FindObjectOfType<RoomManager>();
        isFirstEnter = true;

        for (int i = 0; i < GameUiMgr.single.lastDeparture.Count; i++)
        {
            party_List.Add(GameUiMgr.single.lastDeparture[i].partyData.obj_Data);
            Debug.Log(i + "PartyData.JobType: " + GameUiMgr.single.lastDeparture[i].partyData.jobType.ToString());
            Debug.Log(i + "PartyData.JobType: " + GameUiMgr.single.lastDeparture[i].partyData.type);
            Debug.Log(i + "PartyData.JobIndex: " + GameUiMgr.single.lastDeparture[i].partyData.partyJobIndex);
        }
    }

  


    private void Start()
    {
        ChangePhase(BattlePhase.Start); // 방 체크
    }

    public void BattleReady()
    {
        BaseEntity[] entity = FindObjectsOfType<BaseEntity>(); // 몬스터와 플레이어를 찾음
        battleEnded = false;

        deploy_area.SetActive(true);
        unit_deploy_area.SetActive(true);

        GameObject[] enemy_Obj = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemy_Obj != null)
        {
            foreach (GameObject obj in enemy_Obj)
            {
                deploy_Enemy_List.Add(obj);
            }
        }

        foreach (BaseEntity obj in entity)
        {
            NavMeshAgent nav = obj.GetComponent<NavMeshAgent>();
            EntityDrag drag = obj.GetComponent<EntityDrag>();

            if (nav != null)
            {
                nav.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
            }
            if (drag != null)
            {
                drag.enabled = true;
            }
        }
    }

    private void Update()
    {
        if (_curphase == BattlePhase.Battle && (deploy_Player_List.Count == 0 || deploy_Enemy_List.Count == 0))
        {
            Debug.Log("다 죽음");
            ChangePhase(BattlePhase.End);
        }
    }


    public void ChangePhase(BattlePhase phase)
    {
        _curphase = phase;

        switch (phase)
        {
            case BattlePhase.Start:
                if (room.isMoveDone || isFirstEnter)
                {
                    CheckRoom();
                    isFirstEnter = false;
                }
                break;
            case BattlePhase.Deploy:
                BattleReady();
                break;
            case BattlePhase.Battle:
                break;
            case BattlePhase.End:
                StartCoroutine(EndBattle());
                break;
        }
    }


    public void BattleStart()
    {
        if (deploy_Player_List.Count == 0)
        {
            Debug.Log("하나 이상의 플레이어를 배치하세요");
            return;
            
        }
        else
        {
            Debug.Log("배틀 시작");
            ChangePhase(BattlePhase.Battle);

            deploy_area = GameObject.FindGameObjectWithTag("Deploy");
            unit_deploy_area = GameObject.FindGameObjectWithTag("Wait");
            deploy_area.SetActive(false);
            unit_deploy_area.SetActive(false);

            if (_curphase == BattlePhase.Battle)
            {
                BaseEntity[] entity = FindObjectsOfType<BaseEntity>(); // 활성화 된 플레이어나 몬스터를 찾아서 리스트에 넣음

                foreach (BaseEntity obj in entity)
                {
                    NavMeshAgent nav = obj.GetComponent<NavMeshAgent>();
                    EntityDrag drag = obj.GetComponent<EntityDrag>();

                    if (nav != null)
                    {
                        nav.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
                    }
                    if (drag != null)
                    {
                        drag.enabled = false;
                    }

                    /*Ally ally = obj as Ally;
                    ally.RefreshCurrentHP();*/
                }
            }
        }
    }

    private IEnumerator EndBattle()
    {
        yield return new WaitForSeconds(2f);

        if (_curphase == BattlePhase.End && !battleEnded)
        {
            BaseEntity[] unit = FindObjectsOfType<BaseEntity>();
            
            foreach (BaseEntity obj in unit)
            {
                Ally ally = obj as Ally;
                if (ally != null)
                    ally.UpdateCurrentHPToSingle();
                Destroy(obj.gameObject);
            }

            if (deploy_Player_List.Count == 0 && (room.rooms.Length - 1 == room.room_Count))
            {
            }
            else if (deploy_Enemy_List.Count == 0 && (room.rooms.Length - 1 == room.room_Count))
            {
            }

            deploy_Player_List.Clear();
            deploy_Enemy_List.Clear();
        }


        battleEnded = true;
    }


    public void CheckRoom()
    {
        if (room.currentRoom.tag == "Battle")
        {
            Debug.Log("전투 방입니다.");
            ChangePhase(BattlePhase.Deploy);
        }
        else
        {
            ChangePhase(BattlePhase.Rest);
            Debug.Log("휴식");
        }
    }

    public void ReturnToTown()
    {
        Debug.Log("마을로 이동");
        SceneManager.LoadScene("Scene1");
    }

}
