using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyStat : MonoBehaviour
{
    public static PartyStat single;
    private void Awake()
    {
        single = this;
    }

}
