using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item_Player : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image imgCharctor;
    [Header("캐릭터 이름")]
    [SerializeField] private TMP_InputField field_InputPlayerName;
    [SerializeField] private TextMeshProUGUI textPlayerName;
    public string playerName;

    [Header("캐릭터 직업")]
    [SerializeField] private TextMeshProUGUI textPlayerJob;// 인스펙터창에 등록한 직업 이름을 게임씬에 표시하기위한 변수
    [SerializeField] private string playerJob;// 인스펙터창에 등록한 직업이름을 직통으로 가져다 쓸 것.

    private void Awake()
    {
        field_InputPlayerName.onValueChanged.AddListener(OnInputValueChanged);
        textPlayerJob.text = playerJob;// 씬이 시작되면 사전에 인스펙터에 등록한 직업이름이 표시됨
        SelectPanelAlpha(0.1f);//OnUnselect();
    }

    private void OnInputValueChanged(string field_InputPlayerName) {
        playerName = field_InputPlayerName;
    }
    public string GetName() => playerName;

    public string GetJob() => textPlayerJob.text;//public string GetName() => textPlayerJob.text;
    public void SelectPanelAlpha(float alpha)
    {
        Color currentColor = imgCharctor.color;
        currentColor.a = alpha;
        imgCharctor.color = currentColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PanelSelectPlayer.single.OnFocusePlayerItem(this);
    }
}
