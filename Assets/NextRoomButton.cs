using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoomButton : MonoBehaviour
{
    public void OnMouseDown()
    {
        if (!BattleManager.Instance.dialogue.isTutorial || BattleManager.Instance._curphase == BattleManager.BattlePhase.Rest || BattleManager.Instance._curphase == BattleManager.BattlePhase.End)
        {
            BattleManager.Instance.room.ChangeRoom();
        }
    }
}
