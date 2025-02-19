using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;


public class NewBehaviourScript : MonoBehaviour
{
    public GameObject FishImage;
    public float timeStart;
    public Text textBox;
    public bool getFish;
    public bool startFish;
    public bool readyToFish;
    public Animator animator;
    public ToolbarUI toolbarUI;

    public List<ItemType> fishTypes=new List<ItemType>();
   
    // Start is called before the first frame update
    void Start()
    {
        
        FishImage.SetActive(false);
        textBox.text = null;
        fishTypes.Add(ItemType.WhiteWhiteFishBig);
        fishTypes.Add(ItemType.WhiteWhiteFishMiddle);
        fishTypes.Add(ItemType.WhiteWhiteFishSmall);

    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (startFish == false && readyToFish && Input.GetKeyDown(KeyCode.G))
            {
                timeStart = UnityEngine.Random.Range(10, 15);
                textBox.text = "钓鱼中。。。。。。";
                startFish = true;
                getFish = false;
            }

            if (getFish != true && startFish == true)
            {
                animator.SetBool("startFish", true);
                if (Input.GetKeyDown(KeyCode.G) && timeStart <= 5)
                {
                    int fishNum = UnityEngine.Random.Range(1, 3);
                    ItemType type = fishTypes[fishNum];
                    string fishName = type.ToString();
                    InventoryManager.Instance.AddToBackpack(type);
                    textBox.text = "恭喜抓到鱼啦！按G再来一次！";//抓到的鱼的名字后面加
                    FishImage.SetActive(false);
                    timeStart = 0;
                    startFish = false;
                    animator.SetBool("startFish", false);
                    getFish = true;
                    animator.SetBool("getFish", true);
                    readyToFish = true;
                    //此处补鱼种类加鱼数量加一的代码。
                }
                else
                {
                    timeStart -= Time.deltaTime;
                    if (timeStart <= 6 && timeStart > 0)
                    {
                        FishImage.SetActive(true);
                        textBox.text = "鱼上钩了！快点抓住它！ " + ((int)timeStart).ToString();
                    }
                    if (timeStart <= 0)
                    {
                        textBox.text = "鱼跑啦T_T 按G再来一次吧！";
                        FishImage.SetActive(false);
                        startFish = false;
                        animator.SetBool("startFish", false);
                    }
                }
            }
        }
        catch(IndexOutOfRangeException e)
        {
            Debug.LogError(e.Message);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            if (!collision.CompareTag("Player"))
            {
                throw new farm.ColliderException(collision.gameObject.name);
            }
            if (collision.CompareTag("Player") && startFish == false && toolbarUI.GetSelectedSlotUI() != null && toolbarUI.selectedSlotUI.GetData().item.type == ItemType.FishgrodBlue)
            {
                readyToFish = true;
                textBox.text = "按G开始钓鱼！";
            }
        }
        catch(farm.ColliderException e)
        {
            Debug.Log(e.Message);
        }
        
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        startFish = false;
        animator.SetBool("startFish", false);
        getFish = false;
        animator.SetBool("getFish", false);
        readyToFish = false;
        textBox.text = null;

    }

}
