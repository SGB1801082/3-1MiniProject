using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BattleManager : MonoBehaviour
{
    private static BattleManager instance = null;


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
        Battle,
        End
    }


    public BattlePhase _curphase;
    private List<GameObject> _entity = new List<GameObject>();

    private int _mapId;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        ChangePhase(BattlePhase.Start); // 방 체크
    }

  


    private void Start()
    {
        if (_mapId != 0 || _mapId != 2)
        {
            ChangePhase(BattlePhase.Deploy); // 방 체크해서 전투방이면 실행
        }
    }

    public void BattleReady()
    {
        // 파티원을 초기 위치에 배치하는 메서드나 코드 작성

        BaseEntity[] entity = FindObjectsOfType<BaseEntity>(); // 몬스터를 넣음

        foreach (BaseEntity obj in entity)
        {
            _entity.Add(obj.gameObject);
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

            obj.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if (_curphase == BattlePhase.Deploy)
        {
            BattleReady();
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
        ChangePhase(BattlePhase.Battle);

        if (_curphase == BattlePhase.Battle)
        {
            BaseEntity[] entity = FindObjectsOfType<BaseEntity>(); // 활성화 된 플레이어나 몬스터를 찾아서 리스트에 넣음

            foreach (BaseEntity obj in entity)
            {
                _entity.Add(obj.gameObject);
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

                obj.enabled = true;
            }
        }
    }


    private void CheckMap(int map)
    {
        switch (map) // 방 코드
        {
            case 0: // 휴식방
                _mapId = 0;
                break;
            case 1: // 전투방
                _mapId = 1;
                break;
            case 2: // 보물상자 방
                _mapId = 2;
                break;
            case 3: // 중간보스 방
                _mapId = 3;
                break;
            case 4: // 보스 방
                _mapId = 4;
                break;
        }
    }


}
