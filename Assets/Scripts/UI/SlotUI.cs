using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class SlotUI : MonoBehaviour,IPointerClickHandler
{
    public int index = 0;
    //设置索引方便背包内UI和背包一一对应
    private SlotData data;//槽数据
    
    public Image iconImage;
    public TextMeshProUGUI countText;
    public void SetData(SlotData data)
    {
        this.data = data;
        data.AddListener(OnDataChange);
        UpdateGridUI();
    }

    public SlotData GetData()
    {
        return data;
    }

    private void OnDataChange()
    {
        UpdateGridUI();
    }

    private void UpdateGridUI()
    {
        //每次更新的时候为空就禁用，不为空就启用
        if (data.item == null)
        {
            iconImage.enabled = false;
            countText.enabled = false;
        }
        else
        {
            iconImage.enabled = true;
            countText.enabled = true;
            iconImage.sprite = data.item.sprite;//更新图标
            countText.text = data.count.ToString();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemMoveHandler.Instance.OnSlotClick(this);
    }
}
