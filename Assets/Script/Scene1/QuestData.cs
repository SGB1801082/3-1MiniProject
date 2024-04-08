using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData //오브젝트에 집어넣는게아니라 스크립트를 통해 접근하여 사용할것이기때문에 기본클래스를 상속하지않는 구조체 클래스로 선언.
{
    public string questName;//진행 중인 퀘스트 id
    public int[] npcId;

    //구조체 생성을 위한 매개변수 생성자 작성
    public QuestData(string questName, int[] npcId)
    {
        this.questName = questName;
        this.npcId = npcId;
    }
}
