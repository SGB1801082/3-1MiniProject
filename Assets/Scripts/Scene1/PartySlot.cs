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

    public string strPartyName;
    public int intPartyCost;

    public TextMeshProUGUI text_Name;
    public TextMeshProUGUI text_Cost;
    public TextMeshProUGUI text_Lv;

    public GameObject block;
    public bool moveInChek;

    //05-22
    public Button btnMy;//상호작용을 막기위해 추가
    public void Init(PartyData _data)// PartyData (_data, _Prefab) 으로할까.. 파티리소스Mgr만들어서 프리펩등록하고 아이콘 등록하는식으로...고민중
    {
        this.moveInChek = false;
        Debug.Log("PartySlot Init");
        //Lv, Name, HP, Atk
        this.partyData = _data;
        this.partyIcon.sprite = _data.spPartyIcon;

        this.strPartyName = _data.Type;
        this.text_Name.text = strPartyName;

        this.intPartyCost = _data.cost;
        this.text_Cost.text = intPartyCost.ToString();

        this.text_Lv.text = "Lv "+_data.level.ToString();
    }

    public void OnClick()
    {
        if (moveInChek == true)
        {
            GameUiMgr.single.RestorePartySlot(this.partySlotIndex);
            return;
        }

        if (GameUiMgr.single.ClickedPartySlot(this.partyData))
        {
            Debug.Log("Generate MoveInSlot");
            block.SetActive(true);
            moveInChek = true;
            btnMy.interactable = false;
        }

        /*if (moveInChek == false)
        {
            
            //TODO: 클릭되었으니 PlayerPartyList에 내 partyData를 넘겨 Add해 주고, 새로그려야함
            //여기에서  파티슬롯에 Add해주는데 새로 생성하는 방식이아니라 슬롯에 내 정보를 그대로 덮어씌워서 내껄로만들면서 Block을 false로 해주고 그 상태에서 다시 클릭되면 리스트에서빠지면서 내 본체의 block을 false로 해주기.
        }
        else
        {
            Debug.Log("Delete MoveInSlot");
            //하단 MoveIn Slot List의 PartySlot이 클릭되면 해당 MoveInSlot List에 활성화된 슬롯이 비활성화/제거 되고
            GameUiMgr.single.poolMoveInSlot[partySlotIndex].gameObject.SetActive(false);
            GameUiMgr.single.poolMoveInSlot.Remove(GameUiMgr.single.poolMoveInSlot[partySlotIndex]);
            //Body의 PartyBordSlot이 다시 상호작용가능하게 되어야함.
            block.SetActive(false);
        }*/

    }
    public void ReSetPartySlot()
    {
        this.partyData = null;
    }
}
