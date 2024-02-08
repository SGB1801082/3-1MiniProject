using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Equiment   // Data Only
{
    //public abstract string Sp; or
    protected string strIconName; //상속받은 자식클래스가 변수에 접근가능하게 하기위함. 
    public Equiment(string iconName)
    {
        this.strIconName = iconName;
    }
    public abstract Sprite GetIcon();//추상화 클래스
}
