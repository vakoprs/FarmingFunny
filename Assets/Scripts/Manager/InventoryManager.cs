using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get;  set; }
    //字典保存物品信息
    private Dictionary<ItemType, ItemData> itemDataDict = new Dictionary<ItemType, ItemData>();
   // [HideInInspector]
    //背包
    public InventoryData backpack;
    //工具栏
    //[HideInInspector]
    public InventoryData toolbarData;
    private void Awake()
    {
        Instance = this;
        Init();
    }
    //初始化,数组填充到字典里面
    private void Init()
    {
        ItemData[] toolArray = Resources.LoadAll<ItemData>("Data/Tool");
        ItemData[] fishArray = Resources.LoadAll<ItemData>("Data/Fish");
        ItemData[] fruitArray = Resources.LoadAll<ItemData>("Data/fruit");
        ItemData[] mineralArray = Resources.LoadAll<ItemData>("Data/mineral");
        ItemData[] seedArray = Resources.LoadAll<ItemData>("Data/seed");
        //ItemData[] goodsArray = Resources.LoadAll<ItemData>("Data/Products");
        
        foreach (ItemData data in toolArray)
        {
            itemDataDict.Add(data.type, data);
        }
        foreach (ItemData data in fishArray)
        {
            itemDataDict.Add(data.type, data);
        }
        foreach (ItemData data in fruitArray)
        {
            itemDataDict.Add(data.type, data);
        }
        foreach (ItemData data in mineralArray)
        {
            itemDataDict.Add(data.type, data);
        }
        foreach (ItemData data in seedArray)
        {
            itemDataDict.Add(data.type, data);
        }
        //foreach (ItemData data in goodsArray)
        //{
        //    itemDataDict.Add(data.type, data);
        //}
     
        backpack = Resources.Load<InventoryData>("Data/bag/Backpack");
        toolbarData = Resources.Load<InventoryData>("Data/bag/Toolbar");
    }

    private ItemData getItemData(ItemType type)
    {
        
        ItemData data;
        bool isSuccess=itemDataDict.TryGetValue(type, out data);
        if (isSuccess)
        {
            return data;
        }
        else
        {
            Debug.LogWarning("你传递的type"+type+"不存在，无法得到物品信息");
            return null;
        }
    }

    //增加物品到背包，通过ItemType区分物品
    public void AddToBackpack(ItemType type)
    {
        try
        {
            ItemData item = getItemData(type);
            if (item == null)
            {
                throw new farm.SlotDataNullException(type+"不存在");
                return;

            }

            foreach (SlotData slotData in backpack.slotList)
            {
                //物品已经存在，同类型
                if (slotData.item == item && slotData.CanAddItem())
                {
                    slotData.AddOne();
                    return;
                }
            }
            //无同类型，找空槽
            foreach (SlotData slotData in backpack.slotList)
            {
                if (slotData.count == 0)
                {
                    slotData.AddItem(item);
                    return;
                }
            }
            //背包满了
            Debug.LogWarning("你的背包" + backpack + "无法放入物品，已满");

        }
        catch (farm.SlotDataNullException e)
        {
            Debug.LogError(e);
        }
    }
}
