using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GoodsAnimal : MonoBehaviour
{
    private int animalID;
    public static GoodsAnimal Instance { get; private set; }
    public ItemData item;
    //public int count;
    private int price;
    private string name = "";
    public Image Image;
    public Button BuyButton;
    public Text Information;
    public GameObject BuyFailure;
    public Text BuyFailureMsg;
    private GameObject AnimalPrefab;

    public GameObject parentScene;
    public void Init()
    {
        parentScene.SetActive(false);
        //this.animalID=Instance.animalID;
        //this.item = item;
        this.Image.sprite = item.sprite;
        this.name = item.type.ToString();
        this.price = item.buyMoney;
        string inf = "金额：" + price.ToString();
        this.Information.text = inf;
        this.AnimalPrefab = item.prefab;
    }


    // Start is called before the first frame update
    void Start()
    {
        try
        {
            if (item == null)
            {
                throw new farm.GoodNullReferenceException("商品为空");
            }
            Instance = this;
            Init();
            BuyButton.onClick.AddListener(OnBuyButtonClicked);
            BuyFailureMsg.text = "";
            BuyFailure.SetActive(false);
            
        }
        catch(farm.GoodNullReferenceException e)
        {
            Debug.LogWarning("NULL WARNING");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnBuyButtonClicked()
    {
        ItemMoveHandler.Instance.buyAnimals(item);
    }

    public void buyFailed()
    {
        BuyFailure.SetActive(true);
        BuyFailureMsg.text = "余额不足，购买失败！";
        Invoke("Disappear", 2f);
    }
    public void buySuccess()
    {
        BuyFailure.SetActive(true);
        BuyFailureMsg.text = "购买成功！";
        Invoke("Disappear", 2f);
    }
    public void Disappear()
    {
        BuyFailure.SetActive(false);
        BuyFailureMsg.text = null;
    }

    public void SetAnimals(ItemData animal)
    {
        try
        {
            Debug.Log("生成" + animal.prefab.ToString() + "中");
            AnimalController animalController = animal.prefab.GetComponent<AnimalController>();

            //string SceneName=animalController.SceneName;
            //Debug.Log(SceneName);
            ////GameObject parent =GameObject.Find(SceneName);
            //Debug.Log("找到了父物体");
            //parentScene.SetActive(true);

            Transform parentTransform = parentScene.transform;
            GameObject animalObject = Instantiate(animal.prefab, transform.position, transform.rotation);
            animalObject.transform.SetParent(parentScene.transform, false);
            animalObject.transform.position = new Vector3(0, 0, 0);
            animalObject.GetComponent<AnimalController>().Type = animal.type.ToString();
            int id = UnityEngine.Random.Range(0, 200);
            animalObject.GetComponent<AnimalController>().AnimalID = id;
            Animal.InsertAnimalToDatabase(animalObject);
            parentScene.SetActive(false);
            Instance.animalID += 1;
            //animalObject.SetActive(false);
            Debug.Log("生成成功");
        }
        catch(System.Exception e)
        {
            Debug.LogException(e);
        }
        
    }
}
