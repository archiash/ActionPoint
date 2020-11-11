using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

[System.Serializable]
public class DropTable
{
    public DropItem[] items;

    public StackItem DropLoot()
    {
        int dropIndex = -1;
        int random = Random.Range(0, 101);
        for(int i = 0;i < items.Length;i++)
        {
            if(random <= items[i].rateDrop)
            {
                if (dropIndex >= 0)
                {
                    if (items[i].rateDrop < items[dropIndex].rateDrop)
                        dropIndex = i;
                }
                dropIndex = i;
            }
        }
        if(dropIndex < 0)
        {
            return null;
        }
        int dropAmount = Random.Range(items[dropIndex].minDrop, items[dropIndex].maxDrop + 1);
        return new StackItem(items[dropIndex].item,dropAmount);
    }
}
