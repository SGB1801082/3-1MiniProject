using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private float hoverDuration = 1f; // 호버 애니메이션 지속 시간
    public float hover = 75f; // 호버 시 이미지가 올라가는 높이

    private Vector3 originalPosition; // 이미지의 원래 위치
    private bool isHovering = false; // 호버 중인지 여부
    private Coroutine hoverCoroutine;

    private void Start()
    {
        // 이미지의 원래 위치 저장
        originalPosition = transform.position;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 마우스가 이미지에 진입하면 호버 애니메이션 시작
        if (hoverCoroutine != null)
        {
            StopCoroutine(hoverCoroutine);
        }
        hoverCoroutine = StartCoroutine(HoverAnimation(true));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 마우스가 이미지에서 나가면 호버 애니메이션 종료
        if (hoverCoroutine != null)
        {
            StopCoroutine(hoverCoroutine);
        }
        hoverCoroutine = StartCoroutine(HoverAnimation(false));
    }

    private IEnumerator HoverAnimation(bool isHovering)
    {
        // 이미지가 이미 호버 중이면 중복 실행 방지
        if (this.isHovering == isHovering)
            yield break;

        this.isHovering = isHovering;

        Vector3 targetPosition;

        // 호버 애니메이션 ( 배치 파티창은 오른쪽으로, 아이템 창은 올라오도록 )

        targetPosition = isHovering ? originalPosition + Vector3.up * hover : originalPosition;

        
       
        float elapsedTime = 0f;

        while (elapsedTime < hoverDuration)
        {
            // 시간에 따라 이미지를 부드럽게 이동시킴
            transform.position = Vector3.Lerp(transform.position, targetPosition, elapsedTime / hoverDuration);
            elapsedTime += Time.deltaTime;
            yield return null; // 다음 프레임까지 대기
        }

        // 애니메이션 완료 후 정확한 위치로 보정
        transform.position = targetPosition;
    }
}
