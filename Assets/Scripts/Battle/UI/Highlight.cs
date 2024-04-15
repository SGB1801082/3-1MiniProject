using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    public float activationDistance = 0.5f; // 활성화 거리
    [SerializeField] private List<GameObject> highlights = new List<GameObject>();

    private void Start()
    {
        GameObject[] highlight = GameObject.FindGameObjectsWithTag("Highlight");

        foreach (GameObject high in highlight) 
        {
            highlights.Add(high);
            high.SetActive(false);
        }
    }

    private void Update()
    {
        if (BattleManager.Instance._curphase == BattleManager.BattlePhase.Deploy)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            SetAllHighlightsActive(false);

            foreach (GameObject player in players)
            {
                GameObject closest_Obj = OnHighLight(player.transform.position);

                if (Vector3.Distance(closest_Obj.transform.position, player.transform.position) <= activationDistance)
                {
                    closest_Obj.SetActive(true);
                }
            }
        }
    }

    private GameObject OnHighLight(Vector3 player)
    {
        GameObject closest = null;

        float min = float.MaxValue;

        foreach (GameObject on in highlights) 
        {
            float dis = Vector3.Distance(player, on.transform.position);
            if (dis < min) 
            {
                min = dis;
                closest = on;
            }
        }
        return closest;
    }

    private void SetAllHighlightsActive(bool active)
    {
        foreach (GameObject highlight in highlights)
        {
            highlight.SetActive(active);
        }
    }


    /*public float activationDistance = 0.5f; // 활성화 거리


    private void Update()
    {
        // 가장 가까운 플레이어 찾기
        GameObject closestPlayer = FindClosestPlayer();

        // 활성화 거리 내에 있을 경우에만 해당 하이라이트를 활성화
        if (closestPlayer != null)
        {
            foreach (Transform childTransform in transform)
            {
                if (Vector3.Distance(childTransform.position, closestPlayer.transform.position) <= activationDistance)
                {
                    childTransform.gameObject.SetActive(true);
                }
                else
                {
                    childTransform.gameObject.SetActive(false);
                }
            }
        }
    }

    private GameObject FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject closestPlayer = null;
        float minDistance = float.MaxValue;

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestPlayer = player;
            }
        }

        return closestPlayer;
    }*/
}
