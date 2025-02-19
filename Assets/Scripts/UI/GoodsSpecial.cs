using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoodsSpecial : MonoBehaviour
{
    public static GoodsSpecial Instance { get; private set; }
    //public GameObject GoodsOld;
    //public GameObject GoodsNew;
    //public ItemData item;
    //public int count;
    public int price;
    private string name = "";
    public Image Image;
    public Sprite sprite;
    public Button BuyButton;
    public Text Information;
    public GameObject BuyFailure;
    public Text BuyFailureMsg;
    public bool canBuy;

    //public GameObject newHouse;
    //public Scene scene1;
    public void Init()
    {
        this.canBuy= true;
        //this.item = item;
        this.Image.sprite = sprite;
        //this.name = item.type.ToString();
        //this.price = item.buyMoney;
        string inf = "½ð¶î£º" + price.ToString();
        this.Information.text = inf;
    }
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            Instance = this;
            Init();
            BuyButton.onClick.AddListener(OnBuyButtonClicked);
            BuyFailureMsg.text = "";
            BuyFailure.SetActive(false);
            if (Instance == null)
            {
                throw new farm.InstanceNullException("ÊµÀýÎª¿Õ");
            }
        }
        catch(farm.InstanceNullException e)
        {
            Debug.Log(e.Message);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!canBuy)
        {
            
            BuyButton.interactable = false;
        }
    }
    public void OnBuyButtonClicked()
    {
        ItemMoveHandler.Instance.updateHouse(price);
    }
    public void buyFailed()
    {
        BuyFailure.SetActive(true);
        BuyFailureMsg.text = "Óà¶î²»×ã£¬¹ºÂòÊ§°Ü£¡";
        Invoke("Disappear", 2f);
    }
    public void buySuccess()
    { 
        BuyFailure.SetActive(true);
        BuyFailureMsg.text = "¹ºÂò³É¹¦£¡";
        canBuy=false;
        Invoke("Disappear", 2f);
    }

    public void Disappear()
    {

        BuyFailure.SetActive(false);
        BuyFailureMsg.text = null;
    }
}
