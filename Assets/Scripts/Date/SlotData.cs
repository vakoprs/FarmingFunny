using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]//可序列化，增到Inspector界面
public class SlotData
{
    public ItemData item;//数据信息
    public int count = 0;//数量
    public int money=0;
    //public int countM;

    private Action OnChange;

    public void MoveSlot(SlotData data)
    {
        //移动物品
        this.item = data.item;
        this.count = data.count;
        this.money = data.money;
        OnChange?.Invoke();
    }

    public bool IsEmpty()
    {
        return count == 0;
    }
    //判断物品是否可以继续添加
    public bool CanAddItem()
    {
        return count < item.maxCount;
    }
    //增加同类的数量
    public void AddOne()
    {
        money += item.SellMoney;
        count++;
        OnChange?.Invoke();//不等于空再调用
    }
    //无item增加item
    public void AddItem(ItemData item)
    {
        this.item = item;
        count = 1;
        money = this.item.SellMoney;
        OnChange?.Invoke();//不等于空再调用
    }

    public int Getmoney(ItemData item)
    {
        this.money = this.item.SellMoney;
        return money;
    }

    

    public void ReduceOne()
    {
        money -= item.SellMoney;
        count--;
        if (count == 0)
        {
            Clear();
        }
        else
        {
            OnChange?.Invoke();
        }
    }
    public void Clear()
    {
        item = null;
        count = 0;
        OnChange?.Invoke();
    }
   
    //监听数据变化，更新背包格值
    public void AddListener(Action OnChange)
    {
        this.OnChange=OnChange;
        
    }
}
