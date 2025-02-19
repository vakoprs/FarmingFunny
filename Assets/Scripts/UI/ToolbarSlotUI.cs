using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolbarSlotUI : SlotUI
{
    //通过1-9快捷键选中，若选中则切换显示图片
    public Sprite slotLight;
    public Sprite slotDark;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();//获取图片
    }

    public void Highlight()
    {
        image.sprite = slotDark;
    }

    public void UnHighlight()
    {
        image.sprite = slotLight;
    }

    public int Getmoeny(ItemData item)
    {
        return item.SellMoney;
    }
}
