using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData //여기안에 보유한 장비, 장착한 장비 등 내 게임의 데이터가 들어간다. 여기선 red, blue 같은 캐릭터이름이들어갈 것
{
    public readonly string NAME;// 생성자메서드안에서 지정할수있는데, 이후에는 바꿀수 없는 값.

    public PlayerData(string name)
    {
        this.NAME = name;
    }
}
