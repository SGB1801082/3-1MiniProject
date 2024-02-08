using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //패널, 캐릭터 선택 다 얘가 관리한다. 게임매니저는 하나만있어야한다 = 플레이어 = 싱글톤 적용해서 메모리에 올려야겠죠? || 게임 매니저는 최상위에 붙여야 한다.
    public static GameManager single { get; private set; }//get은 public 하게할수있지만 set은 여기서만 할 수 있다.
    public static PlayerData playerData { get; private set; }//전체게임에서 하나만 있는값

    [SerializeField] PanelSelectPlayer panel_SelectPlayer;
    [SerializeField] Inventory_Mgr inventoryMgr;
    //데이터 처리법

    private void Awake()
    {
        single = this; //너는 이거를 써라 ???

        playerData = null;// 플레이어 데이터 초기화

        panel_SelectPlayer.gameObject.SetActive(true);//시작하면 캐릭터생성 on, 인벤토리 off
        inventoryMgr.gameObject.SetActive(false);
    }

    public bool OnSelectPlayer(string name)
    {
        playerData = new PlayerData(name);  // 플레이어데이터를 생성할때는 이름을넣어주기로 규칙을정했으니 name넣어준다.
        //Debug.Log(name);
        bool succ = playerData != null;
        if (!succ) { return false; }

        //캐릭터 생성 성공
        panel_SelectPlayer.gameObject.SetActive(false);//캐릭터 선택 후 게임시작 시  캐릭터 선택창 off 인벤토리 on
        inventoryMgr.gameObject.SetActive(true);

        return playerData != null;
    }
}
