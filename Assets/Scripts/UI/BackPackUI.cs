using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPackUI : MonoBehaviour
{
    private GameObject parentUI;

    public List<SlotUI> slotuiList;//集合保存所有的UI
    // Start is called before the first frame update
    void Awake()
    {
        parentUI = transform.Find("ParentUI").gameObject;
    }//找到物体
    void Start()
    {
        InitUI();
       
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleUI();
        }
    }

    

    //控制每一个具体的slotUI所在位置，和背包里面的一一对应
   void InitUI()
    {
        try
        {
            slotuiList = new List<SlotUI>(new SlotUI[27]);//先创建一个数组长度为27，转换成list长度
            SlotUI[] slotUiArray = transform.GetComponentsInChildren<SlotUI>();
            foreach (SlotUI slotUi in slotUiArray)
            {
                slotuiList[slotUi.index] = slotUi;
            }
            UpdateUI();
        }
        catch(Exception e)
        {
            Debug.LogError(e.Message);
        }
        
    }

    public void UpdateUI()
    {
        try
        {
            List<SlotData> slotdatalist = InventoryManager.Instance.backpack.slotList;
            for (int i = 0; i < slotdatalist.Count; i++)
            {
                slotuiList[i].SetData(slotdatalist[i]);//让ui和数据一一对应
            }
        }
        catch(Exception e)
        {
            Debug.LogError(e.Message);
        }
      
    }

    //激活与不激活的切换
    private void ToggleUI()
    {
        try
        {
            parentUI.SetActive(!parentUI.activeSelf);
        }
        catch(NullReferenceException e)
        {
            Debug.LogError(e.Message);
        }
        
    }
    
    public void OnCloseClick()
    {
        ToggleUI();
    }
}
