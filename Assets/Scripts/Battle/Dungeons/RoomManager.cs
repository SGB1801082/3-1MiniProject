
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RoomManager : MonoBehaviour
{
    public Transform[] rooms;
    public int room_Count = 0;
    public GameObject popup;

    public Transform currentRoom;
    private Vector3 velocity = Vector3.zero;

    void Awake()
    {
        // 초기 방 설정
        currentRoom = rooms[room_Count];

        foreach (Transform obj in rooms) 
        {
            if (currentRoom == obj)
            {
                obj.gameObject.SetActive(true);
                foreach (Transform child in obj)
                {
                    if (child.CompareTag("Enemy"))
                    {
                        child.gameObject.SetActive(true);
                    }
                }

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
        popup.SetActive(false);

        StartCoroutine(MoveCamera());
    }

    private IEnumerator MoveCamera()
    {
        Vector3 targetPosition = new Vector3(currentRoom.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);

        foreach (Transform obj in rooms)
        {
            if (currentRoom == obj)
            {
                foreach (Transform child in obj)
                {
                    if (child.CompareTag("Enemy"))
                    {
                        child.gameObject.SetActive(true);
                    }
                }
            }
            else
            {
                obj.gameObject.SetActive(false);
            }
        }

        while (Vector3.Distance(Camera.main.transform.position, targetPosition) > 0.1f)
        {
            Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, targetPosition, ref velocity, 0.3f);
            yield return null; // 다음 프레임까지 대기
        }

        // 목표 위치에 정확히 맞춤
        Camera.main.transform.position = targetPosition;

        // 상태 변경
        BattleManager.Instance.ChangePhase(BattleManager.BattlePhase.Deploy);

        



        BaseEntity[] enemy = FindObjectsOfType<BaseEntity>();

        foreach (BaseEntity obj in enemy)
        {
            BattleManager.Instance.deploy_Enemy_List.Add(obj.gameObject);
        }

    }


}
