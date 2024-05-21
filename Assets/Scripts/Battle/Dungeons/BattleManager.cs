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
    public List<GameObject> party_List = new List<GameObject>();
    public List<GameObject> deploy_Player_List = new List<GameObject>();
    public List<GameObject> deploy_Enemy_List = new List<GameObject>();
    public GameObject popup_Bg;
    public GameObject vic_Popup;
    public GameObject def_Popup;
    public GameObject deploy_area;
    public GameObject unit_deploy_area;
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

        ChangePhase(BattlePhase.Start); // 방 체크
        room = FindObjectOfType<RoomManager>();
    }

  


    private void Start()
    {
        foreach (Transform room_obj in room.rooms)
        {
            if (room.currentRoom.tag == "Battle")
            {
                ChangePhase(BattlePhase.Deploy);
            }
            else
            {
                ChangePhase(BattlePhase.Rest);
                Debug.Log("처음 던전에 들어오셨습니다.");
            }
        }

        if (_curphase == BattlePhase.Deploy)
        {
            GameObject[] enemy_Obj = GameObject.FindGameObjectsWithTag("Enemy");

            if (enemy_Obj != null)
            {
                foreach (GameObject obj in enemy_Obj)
                {
                    deploy_Enemy_List.Add(obj);
                }
            }
        }
        

    }

    public void BattleReady()
    {
        BaseEntity[] entity = FindObjectsOfType<BaseEntity>(); // 몬스터와 플레이어를 찾음
        battleEnded = false;

        deploy_area = GameObject.FindGameObjectWithTag("Deloy");
        unit_deploy_area = GameObject.FindGameObjectWithTag("Wait");
        deploy_area.SetActive(true);
        unit_deploy_area.SetActive(true);

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
        if (_curphase == BattlePhase.Deploy)
        {
            BattleReady();
        }

        if (_curphase == BattlePhase.Battle && (deploy_Player_List.Count == 0 || deploy_Enemy_List.Count == 0))
        {
            Debug.Log("다 죽음");
            ChangePhase(BattlePhase.End);
        }

        if (_curphase == BattlePhase.End)
        {
            EndBattle();
        }
    }


    public void ChangePhase(BattlePhase phase)
    {
        _curphase = phase;

        switch (phase)
        {
            case BattlePhase.Start:
                // 입장한 맵의 종류 체크 후 그 방에 해당하는 휴식방
                break;
            case BattlePhase.Deploy:

                break;
            case BattlePhase.Battle:

                break;
            case BattlePhase.End:
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

            deploy_area = GameObject.FindGameObjectWithTag("Deloy");
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
                }
            }
        }
    }

    private void EndBattle()
    {
        if (_curphase == BattlePhase.End && !battleEnded)
        {
            BaseEntity[] unit = FindObjectsOfType<BaseEntity>();
            
            foreach (BaseEntity obj in unit)
            {
                Destroy(obj.gameObject);
            }

            if (deploy_Player_List.Count == 0 && (room.rooms.Length - 1 == room.room_Count))
            {
                popup_Bg.SetActive(true);
                def_Popup.SetActive(true);
                vic_Popup.SetActive(false);
            }
            else if (deploy_Enemy_List.Count == 0 && (room.rooms.Length - 1 == room.room_Count))
            {
                popup_Bg.SetActive(true);
                def_Popup.SetActive(false);
                vic_Popup.SetActive(true);
            }

            deploy_Player_List.Clear();
            deploy_Enemy_List.Clear();
        }


        battleEnded = true;
    }


    public void ReturnToTown()
    {
        Debug.Log("마을로 이동");
        SceneManager.LoadScene("Scene1");
    }

}
