using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
    public static EquipmentPanel instance;

    private void Awake()
    {
        instance ??= this;
    }

}

