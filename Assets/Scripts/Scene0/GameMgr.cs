using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour
{
    public static GameMgr single { get; private set; }
    //public static List<PlayerData> playerData { get; private set; }//여기 수정함 06-02
    public static PlayerData playerData { get; private set; }//임시 버그 틀어막기용

    private bool loadChecker = false;

    private void Awake()
    {
        single = this;

        playerData = null;
    }

    public bool OnSelectPlayer(string name)
    {
        //playerData[0] = new PlayerData(name); // 여기 수정함 06-02

        playerData = new PlayerData(name); // 임시 버그 틀어막기용

        bool succ = playerData != null;
        if (!succ)
            return false;
        if (name == null)
            return false;

        Debug.Log("캐릭터 생성 성공");

        if (loadChecker == false)
        {
            SceneManager.LoadScene("Town");
        }

        return true;
        
    }

    public bool IsGameLoad(bool cheker)
    {
        this.loadChecker = cheker;
        return loadChecker;
    }
    public bool LoadChecker()
    {
        return this.loadChecker;
    }

}

