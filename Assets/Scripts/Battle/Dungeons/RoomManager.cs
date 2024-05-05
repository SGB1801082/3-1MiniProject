using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public Transform[] rooms;
    public GameObject popup;

    private Transform currentRoom;

    void Start()
    {
        // 초기 방 설정
        currentRoom = rooms[0];

        foreach (Transform obj in rooms) 
        {
            if (currentRoom == obj)
            {
                obj.gameObject.SetActive(true);
            }
            else
            {
                obj.gameObject.SetActive(false);
            }
        }


    }

    public void ChangeRoom(int roomIndex)
    {
        // 새로운 방으로 변경
        currentRoom = rooms[roomIndex];
        BattleManager.Instance.ChangePhase(BattleManager.BattlePhase.Deploy);
        popup.SetActive(false);

        foreach (Transform obj in rooms)
        {
            if (currentRoom == obj)
            {
                obj.gameObject.SetActive(true);
            }
            else
            {
                obj.gameObject.SetActive(false);
            }
        }

        

        Camera.main.transform.position = currentRoom.position;

        // 방을 변경할 때마다 Deloy 페이즈로 돌아감
        
    }
}
