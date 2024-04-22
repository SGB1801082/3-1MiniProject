using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMgr : MonoBehaviour
{
    public int questId;
    public int questActionIndex;// 퀘스트 대화순서
    private Dictionary<int, QuestData> dict_questList;

    [Header("Quest Object List")]
    public GameObject[] aryQuestObj;

    [Header("Quest Icons")]
    public GameObject[] questIcons;

    private void Awake()
    {
        dict_questList = new Dictionary<int, QuestData>();
        GenerateQuestData();

        foreach (var icon in questIcons)
        {
            icon.gameObject.SetActive(false);
        }
        questIcons[0].SetActive(true);
    }
    private void GenerateQuestData() 
    { 
        dict_questList.Add(0, new QuestData("모험의 시작", new int[] { 1000 }));
        // Add메서드로 questID, questData를 데이터사전(= dict_questList)에 저장. 구조체 매개변수 생성자의 int배열에는 첫 마을 방문 퀘스트에 연관된 npcID를 입력
        dict_questList.Add(10, new QuestData("마을의 전설 듣기", new int[] { 1000, 2000 }));
        dict_questList.Add(20, new QuestData("루도의 책 찾아주기", new int[] { 9000, 2000 }));
        dict_questList.Add(30, new QuestData("마을의 전설 듣기 퀘스트 클리어!", new int[] { 10000, 4000 }));
    }

    public int GetQuestTalkIndex(int id_Npc) // Npc의 Id를 매개변수로 받아서 퀘스트번호를 반환하는 메서드
    {

        return questId + questActionIndex;
    }

    public string CheckQuest(int id_Npc)
    {
        //순서에 맞게 대화했을때만 퀘스트 대화순서를 올림
        if (id_Npc == dict_questList[questId].npcId[questActionIndex])
        {
            questActionIndex++;
        }

        //Control Quest Objcet
        ControlQuestObejct();

        //Talk Complete & Next Quest
        if (questActionIndex == dict_questList[questId].npcId.Length )
        {
            NextQuest();
        }

        //Quest Name return
        return dict_questList[questId].questName;
    }
    public string CheckQuest()
    {
        return dict_questList[questId].questName;///Quest Name return
    }

    private void NextQuest()
    {
        //새로운 퀘스트로 이어졌다면 바꾸고 초기화
        questId += 10;
        questActionIndex = 0;
    }

    public void ControlQuestObejct()
    {
        Item questItem;
        switch (questId)
        {
            case 10:
                if (questActionIndex == 0)
                {
                    //questIcons[0].SetActive(true);
                }

                if (questActionIndex == 1)
                {
                    questIcons[0].SetActive(false);
                }

                if (questActionIndex == 2)
                {
                    aryQuestObj[0].SetActive(true);
                }
                break;
            case 20:
                questItem = ItemResources.instance.itemRS[0];
                if (questActionIndex == 0)
                {
                    aryQuestObj[0].SetActive(true);

                }
                else if (questActionIndex == 1)
                {
                    aryQuestObj[0].SetActive(false);
                    Inventory.single.AddItem(questItem);
                    GameUiMgr.single.RedrawSlotUI();

                }
                else if (questActionIndex == 2)
                {
                    Debug.Log("Case 22");
                    Inventory.single.RemoveItem(questItem.itemIndex);
                    GameUiMgr.single.RedrawSlotUI();
                }
                break;
            case 30:
                if (questActionIndex == 0)
                {
                    Debug.Log("Case 30");
                }
                break;
        }
    }
}
