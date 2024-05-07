using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public Transform[] rooms;
    int room_Count = 0;
    public GameObject popup;

    private Transform currentRoom;

    void Start()
    {
        // 초기 방 설정
        currentRoom = rooms[room_Count];

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

    public void ChangeRoom()
    {
        // 새로운 방으로 변경
        if ((rooms.Length - 1) == room_Count) 
        {
            SceneManager.LoadScene("Scene1");
        }
        else
        {
            currentRoom = rooms[++room_Count];
        }

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



        Vector3 newPosition = Camera.main.transform.position;
        newPosition.x = currentRoom.position.x;
        Camera.main.transform.position = newPosition;

        BaseEntity[] enemy = FindObjectsOfType<BaseEntity>();

        foreach (BaseEntity obj in enemy) 
        {
            BattleManager.Instance.deploy_Enemy_List.Add(obj.gameObject);
        }

        
    }
}
