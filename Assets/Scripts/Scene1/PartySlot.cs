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
    public bool moveInChek;
    public void Init(PartyData _data)// PartyData (_data, _Prefab) 으로할까.. 파티리소스Mgr만들어서 프리펩등록하고 아이콘 등록하는식으로...고민중
    {
        Debug.Log("PartySlot Init");
        //Lv, Name, HP, Atk
        this.partyData = _data;
        this.partyIcon.sprite = _data.spPartyIcon;

        //this.text_Name.text = _data.strName;
        this.text_Cost.text = _data.cost.ToString();
    }

    public void OnClick()
    {
        if (moveInChek == false)
        {
            block.SetActive(true);

            moveInChek = true;
            //TODO: 클릭되었으니 PlayerPartyList에 내 partyData를 넘겨 Add해 주고, 새로그려야함
            GameUiMgr.single.ClickedPartySlot(this.partyData);
            //여기에서  파티슬롯에 Add해주는데 새로 생성하는 방식이아니라 슬롯에 내 정보를 그대로 덮어씌워서 내껄로만들면서 Block을 false로 해주고 그 상태에서 다시 클릭되면 리스트에서빠지면서 내 본체의 block을 false로 해주기.
        }
        else
        {
            //하단 MoveIn Slot List의 PartySlot이 클릭되면 해당 MoveInSlot List에 활성화된 슬롯이 비활성화/제거 되고
            GameUiMgr.single.poolMoveInSlot[partySlotIndex].gameObject.SetActive(false);
            GameUiMgr.single.poolMoveInSlot.Remove(GameUiMgr.single.poolMoveInSlot[partySlotIndex]);
            //Body의 PartyBordSlot이 다시 상호작용가능하게 되어야함.
            block.SetActive(false);
        }

    }
    public void BaseStat()
    {
        
    }
}
