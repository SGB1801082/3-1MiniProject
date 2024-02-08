using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescourceMgr : MonoBehaviour
{
    //아이템에 임시로 리소스를넣었는데 나중에 동적으로할때 리소스를관리할 매니저
    [SerializeField] private List<Sprite> listArmors;
    [SerializeField] private List<Sprite> listBoots;
    [SerializeField] private List<Sprite> listCaps;
    [SerializeField] private List<Sprite> listGloves;

    private static Dictionary<string, Sprite> dictArmors;//값으로만 쓸 것
    private static Dictionary<string, Sprite> dictBoots;//값으로만 쓸 것
    private static Dictionary<string, Sprite> dictCaps;
    private static Dictionary<string, Sprite> dictGloves;

    private void Awake()
    {
        dictArmors = new Dictionary<string, Sprite>();
        foreach(Sprite sp in listArmors)
        {
            dictArmors.Add(MakeName(sp.name), sp);
        }

        dictBoots = new Dictionary<string, Sprite>();
        foreach (Sprite sp in listBoots)
        {
            dictBoots.Add(MakeName(sp.name), sp);
        }

        dictCaps = new Dictionary<string, Sprite>();
        foreach (Sprite sp in listCaps)
        {
            dictCaps.Add(MakeName(sp.name), sp);
        }

        dictGloves = new Dictionary<string, Sprite>();
        foreach (Sprite sp in listGloves)
        {
            dictGloves.Add(MakeName(sp.name), sp);
        }

        /* foreach 문과 동일한작용을 하는 for문
        for(int i =0; i< listArmors.Count; i++)
        {
            Sprite sp = listArmors[i];
            dictArmors.Add(sp.name, sp);
        }
        */
    }
    private static string MakeName(string str)
    {
        return str.ToLower().Replace(" ", "");
    }

    //static 메서드안에서 쓰이는 값들은 전부 static이여야한다.
    public static Sprite GetArmor(string name)
    {
        name = MakeName(name);
        if (!dictArmors.TryGetValue(name, out Sprite sp)) 
        { 
            return null; 
        }
        return sp;
    }

    public static Sprite GetBoots(string name)
    {
        name = MakeName(name);
        if (!dictBoots.TryGetValue(name, out Sprite sp))
        {
            return null;
        }
        return sp;
    }

    public static Sprite GetCap(string name)
    {
        name = MakeName(name);
        if (!dictCaps.TryGetValue(name, out Sprite sp))
        {
            return null;
        }
        return sp;
    }

    public static Sprite GetGloves(string name)
    {
        name = MakeName(name);
        if (!dictGloves.TryGetValue(name, out Sprite sp))
        {
            return null;
        }
        return sp;
    }
}
