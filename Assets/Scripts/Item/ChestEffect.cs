using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Create/Item/ItemEffect/Chest", fileName = "New Effect")]
public class ChestEffect : ItemEffect
{
    public Item[] items;
    /*
    public override void DoEffect()
    {   
        if(items != null)
            foreach(Item item in items)
                Inventory.instance.GetItem(item);      
    }
    */

}
