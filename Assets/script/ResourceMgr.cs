using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceMgr : MonoBehaviour
{
    [SerializeField] private List<Sprite> listArmors;
    [SerializeField] private List<Sprite> listBoots;
    [SerializeField] private List<Sprite> listGloves;
    [SerializeField] private List<Sprite> listHelmet;
    private static Dictionary<string, Sprite> dictArmors;
    private static Dictionary<string, Sprite> dictBoots;
    private static Dictionary<string, Sprite> dictGloves;
    private static Dictionary<string, Sprite> dictHelmet;
    private void Awake()
    {
        dictArmors = new Dictionary<string, Sprite>();
        dictBoots = new Dictionary<string, Sprite>();
        dictGloves = new Dictionary<string, Sprite>();
        dictHelmet = new Dictionary<string, Sprite>();
        foreach (Sprite sp in listArmors)
        {
            dictArmors.Add(MakeName(sp.name), sp);
        }
        foreach (Sprite sp in listBoots)
        {
            dictBoots.Add(MakeName(sp.name), sp);
        }
        foreach (Sprite sp in listGloves)
        {
            dictGloves.Add(MakeName(sp.name), sp);
        }
        foreach (Sprite sp in listHelmet)
        {
            dictHelmet.Add(MakeName(sp.name), sp);
        }
    }
    private static string MakeName(string str)
    {
        return str.ToLower().Replace(" ", "");
    }

    public static Sprite GetArmor(string name)
    {

        name = MakeName(name);
        if (!dictArmors.TryGetValue(name, out Sprite sp))
            return null;

        return sp;

    }
    public static Sprite GetBoots(string name)
    {

        name = MakeName(name);
        if (!dictBoots.TryGetValue(name, out Sprite sp))
            return null;

        return sp;
    }
    public static Sprite GetGloves(string name)
    {

        name = MakeName(name);
        if (!dictGloves.TryGetValue(name, out Sprite sp))
            return null;

        return sp;
    }
    public static Sprite GetHelmet(string name)
    {

        name = MakeName(name);
        if (!dictHelmet.TryGetValue(name, out Sprite sp))
            return null;

        return sp;
    }
}
