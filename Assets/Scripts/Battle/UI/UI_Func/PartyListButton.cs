using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyListButton : MonoBehaviour
{
    public GameObject targetObject; // 열고 닫을 대상 오브젝트
    public float animationDuration = 1f; // 애니메이션 지속 시간
    public float open = 200f; // 열린 상태에서의 높이
    private Vector3 originalPosition; // 오브젝트의 원래 위치
    private bool isOpen = false; // 열린 상태 여부
    private Coroutine toggleCoroutine;
    private bool isFirst = true;
    bool allInactive = true;
    private Transform[] list;

    private void Start()
    {
        originalPosition = targetObject.transform.position;
        list = GameObject.Find("Party_Inner").GetComponentsInChildren<Transform>();
    }

    private void OnEnable()
    {
        isFirst = true;
    }

    private void Update()
    {
        if (BattleManager.Instance._curphase == BattleManager.BattlePhase.Deploy && isFirst)
        {
            Toggle();
            isFirst = false;
        }
    }

    public void Toggle()
    {
        if (toggleCoroutine != null)
        {
            return;
        }
        toggleCoroutine = StartCoroutine(ToggleAnimation());
    }

    private IEnumerator ToggleAnimation()
    {
        float elapsedTime = 0f;
        Vector3 targetPosition;

        if (isOpen)
        {
            targetPosition = originalPosition;
        }
        else
        {
            targetPosition = originalPosition + Vector3.right * open;
        }

        while (elapsedTime < animationDuration)
        {
            targetObject.transform.position = Vector3.Lerp(targetObject.transform.position, targetPosition, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        targetObject.transform.position = targetPosition;
        isOpen = !isOpen;

        toggleCoroutine = null;
    }
}
