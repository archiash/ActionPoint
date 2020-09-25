using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropItem
{
    public Item item;
    [Range(0,100)]
    public int rateDrop;

    public int minDrop;
    public int maxDrop;
}
