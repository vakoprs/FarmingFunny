using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    public string name="小李";
    private Rigidbody2D rb;//???ǵ?rigidbody
    public float Movespeed;//?????ƶ??ٶ?
    Vector2 moveDirection;//????
    public Animator animator;//???Ƕ?????????
    Collider2D col;//??????ײ??
    Collision2D collision;

    public int sleep = 0;//˯??û
    private ItemMoveHandler movehelper;
    public bool flag = false;
    public ToolbarUI toolbarUI;
    public int fatigue = 90;//疲劳度

    public bool canChop = false;

    public GameObject sick;
    public GameObject happy;

    public GameObject dialogFrame;
    public Text alert;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            rb = GetComponent<Rigidbody2D>();
            //animator = GetComponent<Animator>();
            col = GetComponent<Collider2D>();
            //collision=GetComponent<Collision2D>();
            sick.SetActive(false);
            happy.SetActive(false);
            dialogFrame.SetActive(false);
            alert.text = "";

        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (fatigue > 80)
            {
                sick.SetActive(true);
            }
            else sick.SetActive(false);

            if (happy.activeSelf)
            {
                Invoke("disappear", 3f);
            }
            Movement();
            OnTriggerEnter2D(col);
            useHoe();
            getWood();

            useSeed(GetSelectedUI());
            useWater();
            Mining();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        

        
    }

    public void setFatigue(int n)
    {
        fatigue = n;
    }
    public int getFatigue()
    {
        return fatigue;
    }
    public void disappear()
    {
        happy.SetActive(false);
    }
    public ItemData GetSelectedUI()
    {
        ItemData selectedUI = toolbarUI.selectedSlotUI.GetData().item;
        return selectedUI;
    }
    //得到SlotData，方便调用ReduceOne()
    public SlotData GetSelectedSlot()
    {
        return toolbarUI.selectedSlotUI.GetData();
    }
    //使用锄头
    public void useHoe()
    {
        //如果选择的工具不为空并且是锄头类型
        if (toolbarUI.GetSelectedSlotUI() != null && toolbarUI.selectedSlotUI.GetData().item.type == ItemType.Hoe && Input.GetKeyDown(KeyCode.Tab))
        {
            animator.SetTrigger("useHoe");
            fatigue += 3;
            PlantManager.Instance.HoeGround(transform.position);
        }
    }
    //挖矿
    public void Mining()
    {
        //如果选择的工具不为空并且是镐子类型
        if (toolbarUI.GetSelectedSlotUI() != null && toolbarUI.selectedSlotUI.GetData().item.type == ItemType.PickAxeGold && Input.GetKeyDown(KeyCode.Tab))
        {
            animator.SetTrigger("usePickAxe");
            fatigue += 3;
            if (fatigue >= 80)
            {
                Debug.LogWarning("今日不能再挖矿了");
            }
            else
            {
                PlantManager.Instance.Mineral(transform.position);
            }
        }
    }
    //使用种子
    public void useSeed(ItemData selected)
    {
        int countn = toolbarUI.selectedSlotUI.GetData().count;
        if (toolbarUI.GetSelectedSlotUI() != null && selected.prefab.CompareTag("seed") &&
            Input.GetKeyDown(KeyCode.Tab))
        {
            fatigue += 2;
            PlantManager.Instance.SowSeed(transform.position, selected);
            countn -= 1;
            toolbarUI.selectedSlotUI.GetData().ReduceOne();
            toolbarUI.selectedSlotUI.countText.text = countn.ToString();
            if (countn == 0)
            {
                Destroy(toolbarUI.selectedSlotUI);
            }
        }
    }
    //使用水壶
    public void useWater()
    {
        if (toolbarUI.GetSelectedSlotUI() != null && toolbarUI.selectedSlotUI.GetData().item.type == ItemType.Water &&
            Input.GetKeyDown(KeyCode.Tab))
        {
            fatigue += 1;
            animator.SetTrigger("useWater");
            TileBase tile = PlantManager.Instance.GetTile(transform.position);
            //胡萝卜
            if (tile.name == PlantManager.Instance.carrotlist[0].name || tile.name == PlantManager.Instance.carrotlist[1].name || tile.name == PlantManager.Instance.carrotlist[2].name || tile.name == PlantManager.Instance.carrotlist[3].name || tile.name == PlantManager.Instance.carrotlist[4].name || tile.name == PlantManager.Instance.carrotlist[5].name)
            {
                ItemData data = Resources.Load<ItemData>("Data/seed/carrotseed");
                int day = PlantManager.Instance.waterdayseed[0]++;
                //第几阶段
                int period = day / data.GrowthDay;
                PlantManager.Instance.Water(transform.position, PlantManager.Instance.carrotlist[period]);
                if (day >= data.MaturityDay)
                {
                    PlantManager.Instance.waterdayseed[0] = 0;
                    //增加物品到背包
                    GameObject prefab = Resources.Load<GameObject>("Prefabs/Item/fruit/carrot");
                    InventoryManager.Instance.AddToBackpack(prefab.GetComponent<Pickable>().type);
                    //将土地复原
                    PlantManager.Instance.recover(transform.position);
                }
            }
            //包菜
            if (tile.name == PlantManager.Instance.cabbagelist[0].name || tile.name == PlantManager.Instance.cabbagelist[1].name || tile.name == PlantManager.Instance.cabbagelist[2].name || tile.name == PlantManager.Instance.cabbagelist[3].name || tile.name == PlantManager.Instance.cabbagelist[4].name || tile.name == PlantManager.Instance.cabbagelist[5].name)
            {
                ItemData data = Resources.Load<ItemData>("Data/seed/cabbageseed");
                int day = PlantManager.Instance.waterdayseed[1]++;
                //第几阶段
                int period = day / data.GrowthDay;
                PlantManager.Instance.Water(transform.position, PlantManager.Instance.cabbagelist[period]);
                if (day >= data.MaturityDay)
                {
                    PlantManager.Instance.waterdayseed[1] = 0;
                    //增加物品到背包
                    GameObject prefab = Resources.Load<GameObject>("Prefabs/Item/fruit/cabbage");
                    InventoryManager.Instance.AddToBackpack(prefab.GetComponent<Pickable>().type);
                    //将土地复原
                    PlantManager.Instance.recover(transform.position);
                }
            }
            //玉米
            if (tile.name == PlantManager.Instance.cornlist[0].name || tile.name == PlantManager.Instance.cornlist[1].name || tile.name == PlantManager.Instance.cornlist[2].name || tile.name == PlantManager.Instance.cornlist[3].name || tile.name == PlantManager.Instance.cornlist[4].name || tile.name == PlantManager.Instance.cornlist[5].name)
            {
                ItemData data = Resources.Load<ItemData>("Data/seed/corn_seed");
                int day = PlantManager.Instance.waterdayseed[2]++;
                //第几阶段
                int period = day / data.GrowthDay;
                PlantManager.Instance.Water(transform.position, PlantManager.Instance.cornlist[period]);
                if (day >= data.MaturityDay)
                {
                    PlantManager.Instance.waterdayseed[2] = 0;
                    //增加物品到背包
                    GameObject prefab = Resources.Load<GameObject>("Prefabs/Item/fruit/corn");
                    InventoryManager.Instance.AddToBackpack(prefab.GetComponent<Pickable>().type);
                    //将土地复原
                    PlantManager.Instance.recover(transform.position);
                }
            }
            //茄子
            if (tile.name == PlantManager.Instance.eggplantlist[0].name || tile.name == PlantManager.Instance.eggplantlist[1].name || tile.name == PlantManager.Instance.eggplantlist[2].name || tile.name == PlantManager.Instance.eggplantlist[3].name || tile.name == PlantManager.Instance.eggplantlist[4].name || tile.name == PlantManager.Instance.eggplantlist[5].name)
            {
                ItemData data = Resources.Load<ItemData>("Data/seed/eggplantseed");
                int day = PlantManager.Instance.waterdayseed[3]++;
                //第几阶段
                int period = day / data.GrowthDay;
                PlantManager.Instance.Water(transform.position, PlantManager.Instance.eggplantlist[period]);
                if (day >= data.MaturityDay)
                {
                    PlantManager.Instance.waterdayseed[3] = 0;
                    //增加物品到背包
                    GameObject prefab = Resources.Load<GameObject>("Prefabs/Item/fruit/eggplant");
                    InventoryManager.Instance.AddToBackpack(prefab.GetComponent<Pickable>().type);
                    //将土地复原
                    PlantManager.Instance.recover(transform.position);
                }
            }
            //蘑菇
            if (tile.name == PlantManager.Instance.mushroomlist[0].name || tile.name == PlantManager.Instance.mushroomlist[1].name || tile.name == PlantManager.Instance.mushroomlist[2].name || tile.name == PlantManager.Instance.mushroomlist[3].name || tile.name == PlantManager.Instance.mushroomlist[4].name || tile.name == PlantManager.Instance.mushroomlist[5].name)
            {
                ItemData data = Resources.Load<ItemData>("Data/seed/mushroomseed");
                int day = PlantManager.Instance.waterdayseed[4]++;
                //第几阶段
                int period = day / data.GrowthDay;
                PlantManager.Instance.Water(transform.position, PlantManager.Instance.mushroomlist[period]);
                if (day >= data.MaturityDay)
                {
                    PlantManager.Instance.waterdayseed[4] = 0;
                    //增加物品到背包
                    GameObject prefab = Resources.Load<GameObject>("Prefabs/Item/fruit/mushroom");
                    InventoryManager.Instance.AddToBackpack(prefab.GetComponent<Pickable>().type);
                    //将土地复原
                    PlantManager.Instance.recover(transform.position);
                }
            }
            //土豆
            if (tile.name == PlantManager.Instance.potatolist[0].name || tile.name == PlantManager.Instance.potatolist[1].name || tile.name == PlantManager.Instance.potatolist[2].name || tile.name == PlantManager.Instance.potatolist[3].name || tile.name == PlantManager.Instance.potatolist[4].name || tile.name == PlantManager.Instance.potatolist[5].name)
            {
                ItemData data = Resources.Load<ItemData>("Data/seed/potatoseed");
                int day = PlantManager.Instance.waterdayseed[5]++;
                //第几阶段
                int period = day / data.GrowthDay;
                PlantManager.Instance.Water(transform.position, PlantManager.Instance.potatolist[period]);
                if (day >= data.MaturityDay)
                {
                    PlantManager.Instance.waterdayseed[5] = 0;
                    //增加物品到背包
                    GameObject prefab = Resources.Load<GameObject>("Prefabs/Item/fruit/potato");
                    InventoryManager.Instance.AddToBackpack(prefab.GetComponent<Pickable>().type);
                    //将土地复原
                    PlantManager.Instance.recover(transform.position);
                }
            }
            //南瓜
            if (tile.name == PlantManager.Instance.pumkinlist[0].name || tile.name == PlantManager.Instance.pumkinlist[1].name || tile.name == PlantManager.Instance.pumkinlist[2].name || tile.name == PlantManager.Instance.pumkinlist[3].name || tile.name == PlantManager.Instance.pumkinlist[4].name || tile.name == PlantManager.Instance.pumkinlist[5].name)
            {
                ItemData data = Resources.Load<ItemData>("Data/seed/pumpkinseed");
                int day = PlantManager.Instance.waterdayseed[6]++;
                //第几阶段
                int period = day / data.GrowthDay;
                PlantManager.Instance.Water(transform.position, PlantManager.Instance.pumkinlist[period]);
                if (day >= data.MaturityDay)
                {
                    PlantManager.Instance.waterdayseed[6] = 0;
                    //增加物品到背包
                    GameObject prefab = Resources.Load<GameObject>("Prefabs/Item/fruit/pumokin");
                    InventoryManager.Instance.AddToBackpack(prefab.GetComponent<Pickable>().type);
                    //将土地复原
                    PlantManager.Instance.recover(transform.position);
                }
            }
            //草莓
            if (tile.name == PlantManager.Instance.strawberrylist[0].name || tile.name == PlantManager.Instance.strawberrylist[1].name || tile.name == PlantManager.Instance.strawberrylist[2].name || tile.name == PlantManager.Instance.strawberrylist[3].name || tile.name == PlantManager.Instance.strawberrylist[4].name || tile.name == PlantManager.Instance.strawberrylist[5].name)
            {
                ItemData data = Resources.Load<ItemData>("Data/seed/strawberryseed");
                int day = PlantManager.Instance.waterdayseed[7]++;
                //第几阶段
                int period = day / data.GrowthDay;
                PlantManager.Instance.Water(transform.position, PlantManager.Instance.strawberrylist[period]);
                if (day >= data.MaturityDay)
                {
                    PlantManager.Instance.waterdayseed[7] = 0;
                    //增加物品到背包
                    GameObject prefab = Resources.Load<GameObject>("Prefabs/Item/fruit/strawberry");
                    InventoryManager.Instance.AddToBackpack(prefab.GetComponent<Pickable>().type);
                    //将土地复原
                    PlantManager.Instance.recover(transform.position);
                }
            }
            //西红柿
            if (tile.name == PlantManager.Instance.tomatolist[0].name || tile.name == PlantManager.Instance.tomatolist[1].name || tile.name == PlantManager.Instance.tomatolist[2].name || tile.name == PlantManager.Instance.tomatolist[3].name || tile.name == PlantManager.Instance.tomatolist[4].name || tile.name == PlantManager.Instance.tomatolist[5].name)
            {
                ItemData data = Resources.Load<ItemData>("Data/seed/tomatoseed");
                int day = PlantManager.Instance.waterdayseed[8]++;
                //第几阶段
                int period = day / data.GrowthDay;
                PlantManager.Instance.Water(transform.position, PlantManager.Instance.tomatolist[period]);
                if (day >= data.MaturityDay)
                {
                    PlantManager.Instance.waterdayseed[8] = 0;
                    //增加物品到背包
                    GameObject prefab = Resources.Load<GameObject>("Prefabs/Item/fruit/tomato");
                    InventoryManager.Instance.AddToBackpack(prefab.GetComponent<Pickable>().type);
                    //将土地复原
                    PlantManager.Instance.recover(transform.position);
                }
            }
            //西瓜
            if (tile.name == PlantManager.Instance.watermelonlist[0].name || tile.name == PlantManager.Instance.watermelonlist[1].name || tile.name == PlantManager.Instance.watermelonlist[2].name || tile.name == PlantManager.Instance.watermelonlist[3].name || tile.name == PlantManager.Instance.watermelonlist[4].name || tile.name == PlantManager.Instance.watermelonlist[5].name)
            {
                ItemData data = Resources.Load<ItemData>("Data/seed/watermelonseed");
                int day = PlantManager.Instance.waterdayseed[9]++;
                //第几阶段
                int period = day / data.GrowthDay;
                PlantManager.Instance.Water(transform.position, PlantManager.Instance.watermelonlist[period]);
                if (day >= data.MaturityDay)
                {
                    PlantManager.Instance.waterdayseed[9] = 0;
                    //增加物品到背包
                    GameObject prefab = Resources.Load<GameObject>("Prefabs/Item/fruit/watermalon");
                    InventoryManager.Instance.AddToBackpack(prefab.GetComponent<Pickable>().type);
                    //将土地复原
                    PlantManager.Instance.recover(transform.position);
                }
            }
            //白萝卜
            if (tile.name == PlantManager.Instance.whitecarrotlist[0].name || tile.name == PlantManager.Instance.whitecarrotlist[1].name || tile.name == PlantManager.Instance.whitecarrotlist[2].name || tile.name == PlantManager.Instance.whitecarrotlist[3].name || tile.name == PlantManager.Instance.whitecarrotlist[4].name || tile.name == PlantManager.Instance.whitecarrotlist[5].name)
            {
                ItemData data = Resources.Load<ItemData>("Data/seed/whitecarrotseed");
                int day = PlantManager.Instance.waterdayseed[10]++;
                //第几阶段
                int period = day / data.GrowthDay;
                PlantManager.Instance.Water(transform.position, PlantManager.Instance.whitecarrotlist[period]);
                if (day >= data.MaturityDay)
                {
                    PlantManager.Instance.waterdayseed[10] = 0;
                    //增加物品到背包
                    GameObject prefab = Resources.Load<GameObject>("Prefabs/Item/fruit/whitecarrot");
                    InventoryManager.Instance.AddToBackpack(prefab.GetComponent<Pickable>().type);
                    //将土地复原
                    PlantManager.Instance.recover(transform.position);
                }
            }
        }
    }
    //砍树
    public void getWood()
    {
        if (toolbarUI.GetSelectedSlotUI()!=null &&toolbarUI.selectedSlotUI.GetData().item.type == ItemType.AxeGold && Input.GetKeyDown(KeyCode.Tab)&&canChop==true)
        {
            fatigue += 3;
            Debug.Log(toolbarUI.selectedSlotUI.GetData().item.type);
            animator.SetTrigger("useAxe");
            toolbarUI.selectedSlotUI.GetData().item.chopCount++;
            int count = toolbarUI.selectedSlotUI.GetData().item.chopCount;
            if (count >= 5)
            {
                count = 0;
                toolbarUI.selectedSlotUI.GetData().item.chopCount = 0;
                canChop = false;
                ChopTreeManager.Instance.Chop(transform.position);
                InventoryManager.Instance.AddToBackpack(ItemType.Wood);
            }
        }

    }

    //喂食
    public bool sendFeed(ItemType itemType)
    {
        if (toolbarUI.GetSelectedSlotUI() != null && toolbarUI.selectedSlotUI.GetData().item.type.ToString() == itemType.ToString())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool sendMedicine(ItemType itemType)
    {
        if (toolbarUI.GetSelectedSlotUI() != null && toolbarUI.selectedSlotUI.GetData().item.type.ToString() == itemType.ToString())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void FixedUpdate()
    {

    }
    //???̿????ƶ?
    void Movement()
    {

        float InputSpeedX = Input.GetAxis("Horizontal");
        float InputSpeedY = Input.GetAxis("Vertical");

        //????ת??
        animator.SetFloat("Horizontal", InputSpeedX);
        animator.SetFloat("Vertical", InputSpeedY);
        animator.SetFloat("Speed", moveDirection.sqrMagnitude);

        //移动
        moveDirection = new Vector2(InputSpeedX, InputSpeedY).normalized;

        rb.velocity = moveDirection * Movespeed;



    }

    
    //
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ForRest"))
        {
            if (fatigue < 80)
            {
                animator.SetBool("Sleep1", true);
                //睡一会再醒
                Invoke("Rest", 7f);
                fatigue = (fatigue < 20) ? 0 : (fatigue - 20);

            }
            else
            {
                dialogFrame.SetActive(true);
                alert.text = "要去医院哦！";
            } 
            

        }
        
        if (collision.gameObject.CompareTag("ForSleep"))
        {
            if (fatigue < 80)
            {
                animator.SetBool("Sleep1", true);
                Invoke("Sleeping", 7f);
                fatigue = 0;

            }
            else
            {
                dialogFrame.SetActive(true);
                alert.text = "要去医院哦！";
            }
            
        }
        //if (collision.gameObject.name=="flower_spring_blue")
        if (collision.gameObject.tag == "Item" || collision.gameObject.tag == "Tool" || collision.gameObject.tag == "seed")
        {
            //增加某一类型物品进入背包
            InventoryManager.Instance.AddToBackpack(collision.GetComponent<Pickable>().type);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("wood"))
        {
            canChop = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("startFish", false);
        animator.SetBool("getFish", false);
        dialogFrame.SetActive(false);
        alert.text = "";
    }

    public void Sleeping()
    {
        animator.SetBool("Sleep1", false);
        //˯????????һ
        sleep++;
    }

    public void Rest()
    {
        animator.SetBool("Sleep1", false);
    }
    public void ThrowItem(GameObject itemPrefab, int count)
    {
        try
        {
            for (int i = 0; i < count; i++)
            {
                GameObject go = GameObject.Instantiate(itemPrefab);
                Vector2 direction = Random.insideUnitCircle.normalized * 1.4f;
                go.transform.position = transform.position + new Vector3(direction.x, direction.y, 0);
                go.GetComponent<Rigidbody2D>().AddForce(direction * 2);
            }
        }
        catch(Exception ex)
        {
            Debug.LogException(ex);
        }
        
    }

}

