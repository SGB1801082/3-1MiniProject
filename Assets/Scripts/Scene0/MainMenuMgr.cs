using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuMgr : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject OptionMenu;
    [SerializeField] AddUserName addUserName;

    [SerializeField] Button btnLoadGame;
    private void Awake()
    {
        Screen.SetResolution(1920,1080,FullScreenMode.Windowed);// 게임 시작 시 1920*1080 창모드 실행

        mainMenu.SetActive(true);
        OptionMenu.SetActive(false);
        addUserName.gameObject.SetActive(false);

        if (SaveSystem.DataCheck("save") == false )
        {
            btnLoadGame.gameObject.SetActive(false);
        }else
            btnLoadGame.gameObject.SetActive(true);


        string SavePath = Path.Combine(Application.persistentDataPath, "saves/");
        Debug.Log(SavePath);
    }

    public void OnClickedGameStart()
    {
        mainMenu.SetActive(false);
        addUserName.gameObject.SetActive(true);
    }
    public void OnClickedOptions()
    {
        //mainMenu.SetActive(false);
        OptionMenu.SetActive(true);
    }
    public void OnClickedReLoadGame()
    {
        GameMgr.single.IsGameLoad(true);
        SceneManager.LoadScene("Scene1");
    }


    public void OnClickedSelectQuite()
    {
        mainMenu.SetActive(true);
        addUserName.gameObject.SetActive(false);

    }
    public void OnClickedOptionsQuite()
    {
        //mainMenu.SetActive(true);
        OptionMenu.SetActive(false);
    }

    public void OnClickedQuite()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

}
