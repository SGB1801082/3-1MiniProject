using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    private static BattleManager instance = null;
    public ObjectManager pool;
    public RoomManager room;
    public UIManager ui;
    public Dialogue dialogue;
    public TutorialManager tutorial;
    public List<GameObject> party_List = new List<GameObject>();
    public List<GameObject> deploy_Player_List = new List<GameObject>();
    public List<GameObject> deploy_Enemy_List = new List<GameObject>();
    public GameObject deploy_area;
    public GameObject unit_deploy_area;
    public bool isFirstEnter;
    private bool battleEnded = false;
    public float exp_Cnt;
    public int total_Gold;
    public float total_Exp;

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
        Start, // 방 진입 상태 (방의 종류 체크)
        Deploy, // 전투방 입장 후 배치 단계 상태
        Rest, // 전투방이 아닐 때 
        Battle, // 배치 단계에서 배틀 시작 버튼을 눌러 배틀이 시작된 상태
        End // 적이 다 죽었거나, 아군이 다 사망했을 경우 <- 현재는 배치한 파티원이 다 죽으면 끝나는 방식이라 만약 배치를 하지 않은 파티원이 있으면 재시작? 혹은 아예 다 배치되도록 해야할 듯
    }


    public BattlePhase _curphase;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        room = FindObjectOfType<RoomManager>();
        isFirstEnter = true;

        for (int i = 0; i < GameUiMgr.single.lastDeparture.Count; i++)
        {
            party_List.Add(GameUiMgr.single.lastDeparture[i].partyData.obj_Data);
        }
    }

    private void Start()
    {
        ChangePhase(BattlePhase.Start); // 방 체크
    }

    public void BattleReady() // 전투 방일 시 실행되는 메서드
    {
        Enemy[] entity = FindObjectsOfType<Enemy>(); // 몬스터를 찾음
        battleEnded = false;

        ui.party_List.SetActive(true);
        deploy_area.SetActive(true);
        unit_deploy_area.SetActive(true);

        foreach (Enemy obj in entity)
        {
            NavMeshAgent nav = obj.GetComponent<NavMeshAgent>();

            if (obj.gameObject != null)
            {
                deploy_Enemy_List.Add(obj.gameObject);
            }

            if (nav != null)
            {
                nav.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
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

        switch (phase) // 각 상태에 진입 했을 때 실행되는 switch문
        {
            case BattlePhase.Start:
                if (!ui.in_Portal.activeSelf)
                {
                    ui.in_Portal.SetActive(true);
                    ui.in_Portal.GetComponent<FadeEffect>().fadeout = true;
                }

                if (room.isMoveDone || isFirstEnter)
                {
                    CheckRoom();
                    isFirstEnter = false;
                }
                
                break;
            case BattlePhase.Rest:
                if (!ui.out_Portal.activeSelf)
                {
                    ui.out_Portal.SetActive(true);
                    ui.out_Portal.GetComponent<FadeEffect>().fadeout = true;
                }
                break;
            case BattlePhase.Deploy:
                if (ui.out_Portal.activeSelf)
                    ui.out_Portal.GetComponent<FadeEffect>().fadein = true;
                BattleReady();
                break;
            case BattlePhase.Battle:
                if (ui.in_Portal.activeSelf)
                    ui.in_Portal.GetComponent<FadeEffect>().fadein = true;
                break;
            case BattlePhase.End:
                StartCoroutine(EndBattle());
                break;
        }
    }

    public void BattleStartButton()
    {
        StartCoroutine(BattleStart());
    }


    public IEnumerator BattleStart()
    {
        ui.OpenPopup(ui.battle_Start_Banner);
        yield return StartCoroutine(ui.StartBanner(ui.battle_Start_Banner));
        yield return new WaitForSeconds(0.15f);

        if (deploy_Player_List.Count == 0)
        {
            Debug.Log("하나 이상의 플레이어를 배치하세요");
            yield break;
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
                        drag.enabled = false; // 배틀 시작 시에는 플레이어들이 드래그가 안되도록 방지.
                    }
                }

                // 배틀 시작 시 방 안에 있는 적들의 정보의 경험치량을 계산해서 임시변수에 넣음;
                foreach (GameObject enemy in deploy_Enemy_List)
                {
                    float enemy_Exp = enemy.GetComponent<Enemy>().exp_Cnt;

                    exp_Cnt += enemy_Exp;
                    Debug.Log("얻을 수 있는 경험치 량 : " + exp_Cnt);
                }
            }
        }
    }

    private IEnumerator EndBattle()
    {
        if (_curphase == BattlePhase.End && !battleEnded)
        {
            if (deploy_Player_List.Count == 0)
            {
                ui.OpenPopup(ui.def_Banner);
                yield return StartCoroutine(ui.Def_Banner());
                yield return new WaitForSeconds(0.15f);
                ui.OpenPopup(ui.def_Popup);
            }
            else if (deploy_Enemy_List.Count == 0)
            {
                ui.OpenPopup(ui.vic_Banner);
                yield return StartCoroutine(ui.StartBanner(ui.vic_Banner));
                yield return new WaitForSeconds(0.15f);
               
                if (!ui.in_Portal.activeSelf)
                {
                    ui.in_Portal.SetActive(true);
                    ui.in_Portal.GetComponent<FadeEffect>().fadeout = true;
                }
                if (!ui.out_Portal.activeSelf)
                {
                    ui.out_Portal.SetActive(true);
                    ui.out_Portal.GetComponent<FadeEffect>().fadeout = true;
                }

                ui.OpenPopup(ui.reward_Popup);

                Debug.Log("얻은 경험치 : " + exp_Cnt);
                int ran_Gold = Random.Range(50, 301);
                RewardPopupInit popup = ui.reward_Popup.GetComponent<RewardPopupInit>();
                popup.Init("전투 승리", false);


                GameObject gold_Obj = Instantiate(ui.reward_Prefab, popup.inner_Main);
                gold_Obj.GetComponent<RewardInit>().Init(ui.reward_Icons[0], ran_Gold + " Gold");
                total_Gold += ran_Gold;

                GameObject exp_Obj = Instantiate(ui.reward_Prefab, popup.inner_Main);
                exp_Obj.GetComponent<RewardInit>().Init(ui.reward_Icons[1], exp_Cnt + " Exp");
                total_Exp += exp_Cnt;

                GameMgr.playerData[0].player_Gold += ran_Gold;
                GameMgr.playerData[0].player_cur_Exp += exp_Cnt;
            }


            if (deploy_Enemy_List.Count == 0 && (room.rooms.Length - 1 == room.room_Count))
            {
                if (ui.in_Portal.activeSelf)
                {
                    ui.in_Portal.GetComponent<FadeEffect>().fadein = true;
                }
                if (!ui.out_Portal.activeSelf)
                {
                    ui.out_Portal.SetActive(true);
                    ui.out_Portal.GetComponent<FadeEffect>().fadeout = true;
                }


                Debug.Log("버튼 생성");
                DestroyImmediate(ui.out_Portal.GetComponent<Button>());
                ui.out_Portal.AddComponent<Button>().onClick.AddListener(() => TotalReward());
            }

            BaseEntity[] unit = FindObjectsOfType<BaseEntity>();

            foreach (BaseEntity obj in unit)
            {
                Ally ally = obj as Ally;
                if (ally != null)
                    ally.UpdateCurrentHPToSingle();
                Destroy(obj.gameObject);

                foreach (Transform arrow_Obj in pool.obj_Parent)
                {
                    Destroy(arrow_Obj.gameObject);
                }

                pool.Poolclear();
            }

            deploy_Player_List.Clear();
            deploy_Enemy_List.Clear();
        }

        exp_Cnt = 0;
        battleEnded = true;
    }


    public void TotalReward()
    {
        if (_curphase == BattlePhase.End)
        {
            Debug.Log("실행됨");
            if (deploy_Enemy_List.Count == 0 && (room.rooms.Length - 1 == room.room_Count))
            {
                if (!ui.out_Portal.activeSelf)
                {
                    ui.out_Portal.SetActive(true);
                    ui.out_Portal.GetComponent<FadeEffect>().fadeout = true;
                }

                ui.OpenPopup(ui.vic_Popup);
                RewardPopupInit popup = ui.vic_Popup.GetComponent<RewardPopupInit>();

                foreach (Transform child in popup.inner_Main.transform)
                {
                    Destroy(child.gameObject);
                }

                GameObject gold_Obj = Instantiate(ui.reward_Prefab, popup.inner_Main);
                gold_Obj.GetComponent<RewardInit>().Init(ui.reward_Icons[0], total_Gold + " Gold");

                GameObject exp_Obj = Instantiate(ui.reward_Prefab, popup.inner_Main);
                exp_Obj.GetComponent<RewardInit>().Init(ui.reward_Icons[1], total_Exp + " Exp");
            }
        }
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

        total_Gold = 0;
        total_Exp = 0;

        GameMgr.playerData[0].cur_Player_Sn -= 5;
        GameMgr.playerData[0].cur_Player_Hp = GameMgr.playerData[0].max_Player_Hp;
        //GameUiMgr.single.GameSave();

        SceneManager.LoadScene("Town");
    }

    public void DestroyRewardPopup()
    {
        RewardPopupInit popup = ui.reward_Popup.GetComponent<RewardPopupInit>();
        
        foreach (Transform child in popup.inner_Main.transform)
        {
            Destroy(child.gameObject);
        }
        
    }

}
