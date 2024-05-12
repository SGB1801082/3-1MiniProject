using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartySlot : MonoBehaviour
{
    public int partySlotIndex;

    public PartyData partyData;

    public Image partyIcon;
    public TextMeshProUGUI text_Name;
    public TextMeshProUGUI text_Cost;

    public GameObject block;

    public PartySlot(PartyData _data)// PartyData (_data, _Prefab) 으로할까.. 파티리소스Mgr만들어서 프리펩등록하고 아이콘 등록하는식으로...고민중
    {
        this.partyData = _data;
        this.partyIcon.sprite = _data.spPartyIcon;
        this.text_Name.text = _data.strName;
        this.text_Cost.text = _data.cost.ToString();
    }

    public void OnClick()
    {
        //if (true){ } 대충 여기다가 PlayerList수가 < 3 일때만 메서드들 실행되고 아니면 return ~   
        block.SetActive(true);
        //TODO: 클릭되었으니 PlayerPartyList에 내 partyData를 넘겨 Add해 주고
    }
}
