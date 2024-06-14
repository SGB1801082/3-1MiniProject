using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    public float activationDistance = 0.5f; // 활성화 거리
    [SerializeField] private List<GameObject> highlights = new List<GameObject>();
    DeployInit deploy;

    private void Awake()
    {
       deploy = gameObject.transform.parent.GetComponent<DeployInit>();
    }

    private void Start()
    {
        foreach (GameObject high in deploy.highlight)
        {
            highlights.Add(high);
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
}
