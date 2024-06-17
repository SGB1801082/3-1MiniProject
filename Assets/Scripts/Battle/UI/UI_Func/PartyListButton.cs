using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyListButton : MonoBehaviour
{
    public GameObject obj_Side;
    public Sprite open_party;
    public Sprite close_party;
    public Vector2 vec_From;
    public Vector2 vec_To;
    public float f_Set_Timer;
    public bool isOpen = false;
    [SerializeField] bool isMove = false;

    private void Start()
    {
        obj_Side.GetComponent<RectTransform>().anchoredPosition = vec_From;
        isMove = false;
    }

    public void Clicked_Side()
    {
        if (isMove) return;

        if (!isOpen)
        {
            Open_Side();
        }
        else
        {
            Close_Side();
        }
    }

    public void Open_Side()
    {
        StartCoroutine(Open_Side_Co());
    }

    private IEnumerator Open_Side_Co()
    {
        isMove = true;
        Debug.Log("열리는 중");
        float timer = 0;
        while (timer < f_Set_Timer)
        {
            timer += Time.deltaTime;
            Vector2 temp = Vector2.Lerp(vec_From, vec_To, timer / f_Set_Timer);
            yield return null;
            obj_Side.GetComponent<RectTransform>().anchoredPosition = temp;
        }
        obj_Side.GetComponent<RectTransform>().anchoredPosition = vec_To;

        isOpen = true;
        this.gameObject.GetComponent<Image>().sprite = close_party;
        isMove = false;
        yield break;
    }

    public void Close_Side()
    {
        StartCoroutine(Close_Side_Co());
    }

    private IEnumerator Close_Side_Co()
    {
        isMove = true;
        Debug.Log("닫히는 중");
        float timer = 0;
        while (timer < f_Set_Timer)
        {
            timer += Time.deltaTime;
            Vector2 temp = Vector2.Lerp(vec_To, vec_From, timer / f_Set_Timer);
            yield return null;
            obj_Side.GetComponent<RectTransform>().anchoredPosition = temp;
        }
        obj_Side.GetComponent<RectTransform>().anchoredPosition = vec_From;

        isOpen = false;
        this.gameObject.GetComponent<Image>().sprite = open_party;
        isMove = false;
        yield break;
    }


}
