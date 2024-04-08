using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour
{
    public static GameMgr single { get; private set; }
    public static PlayerData playerData { get; private set; }

    private bool loadChecker = false;

    private void Awake()
    {
        single = this;

        playerData = null;
    }

    public bool OnSelectPlayer(string name)
    {
        playerData = new PlayerData(name);

        bool succ = playerData != null;
        if (!succ)
            return false;
        if (name == null)
            return false;

        Debug.Log("캐릭터 생성 성공");

        if (loadChecker == false)
        {
            SceneManager.LoadScene("Scene1");
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

