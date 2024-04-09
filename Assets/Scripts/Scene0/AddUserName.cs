using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddUserName : MonoBehaviour
{
    [Header("ĳ���� �̸�")]
    [SerializeField] private TMP_InputField field_InputPlayerName;
    public string playerName;

    // Btn Start
    [SerializeField] private Button btnStart;

    private void Awake()
    {
        field_InputPlayerName.onValueChanged.AddListener(OnInputValueChanged);
        btnStart.interactable = false;
    }

    private void OnInputValueChanged(string field_InputPlayerName)
    {
        /*if (string.IsNullOrEmpty(field_InputPlayerName))
        {
            btnStart.interactable = false;
        }
        else
        {
            playerName = field_InputPlayerName;
            btnStart.interactable = true;
        }*/

        playerName =field_InputPlayerName;
        btnStart.interactable = !string.IsNullOrEmpty(field_InputPlayerName);

    }
    private string RemoveUnderLine(string inputText)
    {
        string removeText = inputText.Replace("<u>", "").Replace("</u>", "");
        return removeText;
    }

    public void OnStartGame()
    {
        RemoveUnderLine(playerName);
        if (string.IsNullOrEmpty(playerName))
        {
            btnStart.interactable = false;
            return;
        }
        GameMgr.single.OnSelectPlayer(playerName);

    }

}