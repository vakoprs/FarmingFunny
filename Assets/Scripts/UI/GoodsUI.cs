using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class GoodsUI : MonoBehaviour
{
    public static GoodsUI Instance { get; private set; }
    public ItemData item;
    //public int count;
    private int price;
    private string name = "";
    public Image Image;
    public Button BuyButton;
    public Text Information;
    public GameObject BuyFailure;
    public Text BuyFailureMsg;
    private GameObject Good;

    public void Init()
    {
        
        //this.item = item;
        this.Image.sprite = item.sprite;
        this.name = item.type.ToString();
        this.price = item.buyMoney;
        string inf = "��" + price.ToString();
        this.Information.text = inf;
    }//name + "  " + 
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            if (item == null)
            {
                throw new farm.GoodNullReferenceException("��ƷΪ��");
            }
            Instance = this;
            Init();
            Good = item.prefab;
            BuyButton.onClick.AddListener(OnBuyButtonClicked);
            BuyFailureMsg.text = "";
            BuyFailure.SetActive(false);
            if (Instance == null)
            {
                throw new farm.InstanceNullException("ʵ��Ϊ��");
            }
        }
        catch(farm.GoodNullReferenceException e)
        {
            Debug.LogError(e.Message);
        }
        catch(farm.InstanceNullException e)
        {
            Debug.Log(e.Message);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnBuyButtonClicked()
    {
        ItemMoveHandler.Instance.buyGoods(this.price,Good);
    }
    public void buyFailed()
    {
        BuyFailure.SetActive(true);
        BuyFailureMsg.text = "���㣬����ʧ�ܣ�";
        Invoke("Disappear", 2f);
    }

    public void Disappear()
    {
        
        BuyFailure.SetActive(false);
        BuyFailureMsg.text = null;
    }
}
