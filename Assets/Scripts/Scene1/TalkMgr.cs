using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkMgr : MonoBehaviour // 대화 데이터를 관리할 매니저 스크립트
{
    private Dictionary<int, string[]> dictTalkData;//오브젝트 id를 받으면 문장(대사)이 들어있는배열(대화)을 반환하는 변수
    private Dictionary<int, Sprite> dictPortraitSprite;// 초상화 데이터를 저장할 딕셔너리 <초상화에 해당하는 npc id, 해당초상화 이미지 << 초상화 스프라이트를 저장하는 배열의 요소가 들어갈 것>
    public Sprite[] aryPortraitSprite;// 초상화 스프라이트를 저장할 배열
    private void Awake()
    {
        dictTalkData = new Dictionary<int, string[]>(); // 초기화
        dictPortraitSprite = new Dictionary<int, Sprite>();
        GenerateTalkData();// 만들어진 오브젝트들의 상호작용 대사를 호출
    }

    private void GenerateTalkData()// 오브젝트들의 상호작용 대사를 만들어서 스크립트 실행할때 호출되게 함
    {
        //Talk Data - NPC A: 1000, NPC B: 2000, BOX: 100, 
        dictTalkData.Add(1000, new string[] { "안녕! :0", "이 곳에 처음 왔구나?:1", "개쩌는 김치피자탕수육을 만들어 주렴:2" });// 하나의 대화에는 여러 문장이 있으므로 배열로 선언
        dictTalkData.Add(2000, new string[] { "[플레이어이름]! :0","던전마을로 가는거야? :1", "몸조심해! :2" });
        //dictTalkData.Add(1000, new string[] { "안녕!" + GameMgr.playerData.NAME + ":0", "이 곳에 처음 왔구나?:1", "개쩌는 김치피자탕수육을 만들어주렴:2" });// 하나의 대화에는 여러 문장이 있으므로 배열로 선언
        //dictTalkData.Add(2000, new string[] { GameMgr.playerData.NAME+"! :0","던전마을로 가는거야? :1", "몸조심해! :2" });
        dictTalkData.Add(5000, new string[] { "상자" });
        dictTalkData.Add(6000, new string[] { "표지판" });
        dictTalkData.Add(7000, new string[] { "문"});
        dictTalkData.Add(8000, new string[] { "던전"});

        //Quest talk
        //dictTalkData.Add(01 + 1000, new string[] { "안녕! :0", "이 곳에 처음 왔구나?:1", "나는 루나라고해!:2" });
        dictTalkData.Add(10 + 1000, new string[] { "어서와 :0", "이 곳의 전설에 대해 들으러 왔구나?:1", "자세한 내용은 루도에게 들으면 돼:1" });
        dictTalkData.Add(11 + 1000, new string[] { "아직 못 만났어?:1", "루도는 오른쪽으로 가면 만날 수 있을거야.:1" });
        dictTalkData.Add(11 + 2000, new string[] { "우리 마을의 전설에 대해 알고싶구나? :0", "그렇다면 내 부탁을 하나 들어주렴 :1", "호수 근처에서 떨어트린 책을 찾아줘 :2" });

        dictTalkData.Add(20 + 1000, new string[] { "루도의 책? :0", "호수에서 책을 잃어버리면 못쓰지!:3", "나중에 루도에게 한마디 해야겠어.:3" });
        dictTalkData.Add(20 + 2000, new string[] { "찾으면 꼭 가져다줘 :1"});
        dictTalkData.Add(20 + 9000, new string[] { "책을 발견했다." });

        dictTalkData.Add(21 + 2000, new string[] { "앗, 찾아줘서 고마워 :2"});

        //Portrait Data, 0:Idel, 1: Talk, 2: Happy, 3: Angry
        // (내가 지정한 Npc의 ID + NPC상태에 따른 변수), 스프라이트배열 aryPortraitSprite에 저장된 스프라이트 이미지 << 이건 추후에 배열번호가아니라 배열에저장된 스프라이트 이름으로 주는식으로 변경할수있을듯 
        dictPortraitSprite.Add(1000 + 0, aryPortraitSprite[0]);// Idel
        dictPortraitSprite.Add(1000 + 1, aryPortraitSprite[1]);
        dictPortraitSprite.Add(1000 + 2, aryPortraitSprite[2]);
        dictPortraitSprite.Add(1000 + 3, aryPortraitSprite[3]);

        dictPortraitSprite.Add(2000 + 0, aryPortraitSprite[4]);// Idel
        dictPortraitSprite.Add(2000 + 1, aryPortraitSprite[5]);
        dictPortraitSprite.Add(2000 + 2, aryPortraitSprite[6]);
        dictPortraitSprite.Add(2000 + 3, aryPortraitSprite[7]);
    }

    public string GetTalk(int objectID, int talkDataIndex)// 지정된 대화 문장을 반환하는 함수
    {
        //Dictionary에 Key가 있는지 검사하는 함수
        if (!dictTalkData.ContainsKey(objectID))
        {
            if (!dictTalkData.ContainsKey(objectID - objectID%10))
                return GetTalk(objectID - objectID % 100, talkDataIndex);// Get First Talk
            else
                return GetTalk(objectID - objectID % 10, talkDataIndex);// Get First Quest Talk
        }

        if (talkDataIndex == dictTalkData[objectID].Length)
            return null;
        else
            return dictTalkData[objectID][talkDataIndex];

    }
    public Sprite GetPortrait(int npcId, int portraitIndex)// 딕셔너리에 저장된 스프라이트를 지정하여 반환시키는 메서드
    {
        return dictPortraitSprite[npcId + portraitIndex];
    }
}
