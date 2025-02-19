using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Vector3 = UnityEngine.Vector3;

public class ItemMoveHandler : MonoBehaviour
{
    public static ItemMoveHandler Instance { get;private set; }
    private Image icon;
    private SlotUI selectedSlotUI;
    private SlotData selectedSlotData;
    private PlayerController player;
    private bool isCtrlDown = false;
    public TextMeshProUGUI countText;
    public float zoomsize;

    public ItemData money;
    private int countM;
    //private SlotData Pocket;
    private void Awake()
    {
        try
        {
            Instance = this;
            //ItemMoveHandler.countM=5000;
            Instance.countM = money.buyMoney;
            Debug.Log("get money");
            countText.text = Instance.countM.ToString();
            Debug.Log("show money");
            icon = GameObject.FindGameObjectWithTag("case").transform.GetComponent<Image>();
            HideIcon();
            player = GameObject.FindAnyObjectByType<PlayerController>();
        }
        catch(NullReferenceException e)
        {
            Debug.LogWarning(e.Message);
        }
        
    }
    private void Start()
    {
        Instance.countM = money.buyMoney;
        countText.text = Instance.countM.ToString();

    }

    private void Update()
    {
        Instance.countM=money.buyMoney;
        if (icon.enabled)
        {
            //将屏幕上的点击点转化为本地的点,鼠标移动物体
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), Input.mousePosition,
                null, out position);
            icon.GetComponent<RectTransform>().anchoredPosition = position;
            
            
        }

        if (Input.GetMouseButtonDown(0))
        {
            //icon.transform.localScale = new Vector3(zoomsize, zoomsize, 1.0f);
            //如果鼠标不在物品上，将东西进行丢弃
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                ThrowItem();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCtrlDown = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCtrlDown = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            ClearHand();
        }
        //countText.text=Instance.countM.ToString();
        changeMoney();
    }
    

    //监听哪个凹槽被点击了
    public void OnSlotClick(SlotUI slot)//点击的
    {
        //判断手上是否为空
        if (selectedSlotData != null)
        {
            //不为空
            //当前点击了一个空格子
            if (slot.GetData().IsEmpty())
            {
                MoveItem(selectedSlotData,slot.GetData());
            }
        }
        else
        {
            //为空
            if (slot.GetData().IsEmpty()) return;
            selectedSlotData = slot.GetData();
            ShowIcon(selectedSlotData.item.sprite);
        }
    }

    void HideIcon()
    {
        icon.enabled = false;
    }

    void ShowIcon(Sprite sprite)
    {
        icon.sprite = sprite;
        icon.enabled = true;
    }

    void ClearHand()
    {
        HideIcon();
        selectedSlotData = null;
    }//抛出时清空

    private void ThrowItem()
    {
        try
        {

            if (selectedSlotData != null)
            {
                //设置按住Control键，只扔出一个物品
                GameObject prefab = selectedSlotData.item.prefab;
                int count = selectedSlotData.count;
                if (isCtrlDown)
                {
                    player.ThrowItem(prefab, 1);
                    selectedSlotData.ReduceOne();

                }
                else
                {
                    player.ThrowItem(prefab, count);
                    selectedSlotData.Clear();
                }
                if (selectedSlotData.IsEmpty())
                {
                    ClearHand();
                }
            } else throw new farm.SlotDataNullException("选择为空");

        }catch(farm.SlotDataNullException e)
        {
            Debug.LogException(e);
        }
        
    }
    //移动物品到新格子
    private void MoveItem(SlotData fromdata, SlotData todata)
    {
        if (isCtrlDown)
        {
            todata.AddItem(fromdata.item);
            fromdata.ReduceOne();
        }
        else
        {
            todata.MoveSlot(fromdata);
            fromdata.Clear(); //隐藏图标，清空
        }

        if (selectedSlotData.IsEmpty())
        {
            ClearHand();
        }
    }

    void ClearHandFoeced()
    {
        HideIcon();
        selectedSlotData = null;
    }
    private void changeMoney()
    { 
        if (Input.GetKeyDown(KeyCode.K)&&player.flag)
        {
            money.buyMoney += Instance.selectedSlotData.item.SellMoney;
            Instance.countM = money.buyMoney;
            countText.text=Instance.countM.ToString();
            //countText.text=Instance.selectedSlotData.money.ToString();
            Instance.selectedSlotData.Clear();
            Instance.ClearHandFoeced();
        }
    }

    //买东西
    public void buyGoods(int price, GameObject item)
    {
        if (Instance.countM < price)
        {
            GoodsUI.Instance.buyFailed();
        }
        else
        {
            InventoryManager.Instance.AddToBackpack(item.GetComponent<Pickable>().type);
            Instance.countM -= price;
            money.buyMoney -= price;
            countText.text = Instance.countM.ToString();
            
            Debug.Log(item.GetComponent<Pickable>().type.ToString() + "放进去了");
        }
    }

    public void buyTreasure(ItemData item,int price,int amount)
    {
        if (Instance.countM < price)
        {
            TreasureUI.Instance.buyFailed();
        };
        List<SlotData> slotdatalist = InventoryManager.Instance.backpack.slotList;
        for (int i = 0; i < slotdatalist.Count; i++)
        {
            if (slotdatalist[i].item == item)
            {
                if (slotdatalist[i].count < amount)
                {
                    TreasureUI.Instance.buyFailed();
                }else
                {     
                    TreasureUI.Instance.buySuccess();
                    slotdatalist[i].count -= amount;
                    Instance.countM -= price;
                    money.buyMoney -= price;
                    countText.text = Instance.countM.ToString();
                }
            }
        }

    }
    public void updateHouse(int price)
    {
        if(Instance.countM < price)
        {
            GoodsSpecial.Instance.buyFailed();
        }
        else
        {
            GoodsSpecial.Instance.buySuccess();
            Instance.countM -= price;
            money.buyMoney -= price;
            countText.text=Instance.countM.ToString();
        }
    }

    public void buyAnimals(ItemData item)
    {
        if (Instance.countM < item.buyMoney)
        {
            GoodsAnimal.Instance.buyFailed();
        }
        else
        {
            GoodsAnimal.Instance.buySuccess();
            Instance.countM -= item.buyMoney;
            money.buyMoney -= item.buyMoney;
            countText.text=Instance.countM.ToString();
            GoodsAnimal.Instance.SetAnimals(item);
        }
    }


}
