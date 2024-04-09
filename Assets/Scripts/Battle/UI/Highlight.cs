using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    public float activationDistance = 0.1f; // 활성화 거리

    private void Update()
    {
        // 현재 하이라이트 오브젝트의 위치
        Vector3 highlightPosition = transform.position;

        // 가장 가까운 플레이어 찾기
        GameObject closestPlayer = FindClosestPlayer(highlightPosition);

        // 활성화 거리 내에 있을 경우에만 해당 하이라이트를 활성화
        if (closestPlayer != null && Vector3.Distance(highlightPosition, closestPlayer.transform.position) <= activationDistance)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private GameObject FindClosestPlayer(Vector3 position)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject closestPlayer = null;
        float minDistance = float.MaxValue;

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(position, player.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestPlayer = player;
            }
        }

        return closestPlayer;
    }
}
