using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarUI : MonoBehaviour
{
    public List<ToolbarSlotUI> slotuiList;//集合保存所有的UI
    public ToolbarSlotUI selectedSlotUI;//代表当前选择的格子

    private void Awake()
    {
        
    }

    private void Start()
    {
        InitUI();
    }

    private void Update()
    {
        ToolbarSelectControl();
    }
    //得到selectedUI
    public ToolbarSlotUI GetSelectedSlotUI()
    {
        return selectedSlotUI;
    }

    //控制每一个具体的slotUI所在位置，和背包里面的一一对应
    void InitUI()
    {
        slotuiList = new List<ToolbarSlotUI>(new ToolbarSlotUI[9]);//先创建一个数组长度为27，转换成list长度
        ToolbarSlotUI [] slotUiArray=transform.GetComponentsInChildren<ToolbarSlotUI>();
        foreach (ToolbarSlotUI slotUi in slotUiArray)
        {
            slotuiList[slotUi.index] = slotUi;
        }
        UpdateUI();
    }
    
    public void UpdateUI()
    {
        List<SlotData> slotdatalist = InventoryManager.Instance.toolbarData.slotList;
        for (int i = 0; i < slotdatalist.Count; i++)
        {
            slotuiList[i].SetData(slotdatalist[i]);//让ui和数据一一对应
        }
    }

    void ToolbarSelectControl()
    {
        //按键检测
        for (int i = (int)KeyCode.Alpha1; i <= (int)KeyCode.Alpha9; i++)
        {
            if (Input.GetKeyDown((KeyCode)i))
            {
                try
                {
                    if (selectedSlotUI != null)
                    {
                        //按下的选中，其余的取消选中，若是已经选过就取消
                        selectedSlotUI.UnHighlight();
                    }
                    else throw new farm.SlotDataNullException("Null");
                    //转换成索引,按下键后切换图片
                    int index = i - (int)KeyCode.Alpha1;
                    selectedSlotUI = slotuiList[index];
                    selectedSlotUI.Highlight();
                }
                catch(farm.SlotDataNullException e)
                {
                    Debug.Log(e.Message);
                }
               
            }
        }
    }
}
