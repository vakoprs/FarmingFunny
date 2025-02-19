using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureUI : MonoBehaviour
{
    public static TreasureUI Instance { get; private set; }
    private int price;
    public int needMater;
    private string name = "";
    public Image Image;
    public Button BuyButton;
    public Text Information;
    public GameObject BuyFailure;
    public Text BuyFailureMsg;
    public ItemData material;
    public ItemData product;
    
    // Start is called before the first frame update
    public void Init()
    {
        
        //this.item = item;
        this.Image.sprite = product.sprite;
        //this.name = item.type.ToString();
        this.price = product.buyMoney;
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
            if (material == null || product == null)
            {
                throw new farm.GoodNullReferenceException("ItemDataÎª¿Õ");
            }
        }catch(farm.GoodNullReferenceException e)
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
        ItemMoveHandler.Instance.buyTreasure(material, price,needMater);
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
        InventoryManager.Instance.AddToBackpack(product.type);
        Invoke("Disappear", 2f);
    }

    public void Disappear()
    {

        BuyFailure.SetActive(false);
        BuyFailureMsg.text = null;
    }
}
