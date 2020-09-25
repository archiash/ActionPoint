using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager SM;

    public List<Item> shoplist = new List<Item>();
    public Transform shopParent;

    public GameObject pfShopSlot;
    public void UpdateShop()
    {
        ClearShopList();

        foreach(Item item in shoplist)
        {
            GameObject newShopSlot = Instantiate(pfShopSlot, shopParent);
            newShopSlot.GetComponent<ShopSlot>().item = item;
            newShopSlot.GetComponent<ShopSlot>().OnCreate();
        }
    }

    public void ClearShopList()
    {

        Transform[] transforms = shopParent.GetComponentsInChildren<Transform>();

        foreach (Transform x in transforms)
        {
            if (x.GetComponent<HuntingBar>())
            {
                Destroy(x.gameObject);
            }
        }
    }

    private void Start()
    {
        if(SM == null)
            SM = this;

        UpdateShop();

    }
}
