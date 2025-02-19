using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalProducingController : MonoBehaviour
{
    //public static AnimalProducingController Instance { get; private set; }
    public GameObject productPrefab;
    private GameObject producerPrefab;
    public ItemData food;
    public ItemData medicine;
    public ItemData product;

    int Feed;
    int Month;
    public bool canFeed;
    int Health;

    public int sellm1,sellm2;
    private string day;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            GameObject self = GetComponent<GameObject>();
            product.SellMoney = sellm1;
            producerPrefab = GetComponent<GameObject>();
            Feed = 0;
            Health = 100;
            Month = 0;
            canFeed = true;
            //self.GetComponent<AnimalController>().AnimalID = UnityEngine.Random.Range(0, 200);
            //day = TimeSystemController.Instance.season.text+TimeSystemController.Instance.day.text;

        }
        catch (NullReferenceException e)
        {
            Debug.Log(e.ToString());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (day != TimeSystemController.Instance.season.text + TimeSystemController.Instance.day.text)
        //{
        //    refreshTime();
        //    Health--;
        //    Debug.Log(day);
        //    canFeed = true;
        //};

        if (Feed == 3)
        {
            GameObject product = GameObject.Instantiate(productPrefab);
            float x = UnityEngine.Random.Range(-6f, 0f);
            float y = UnityEngine.Random.Range(-4f, -1f);
            product.transform.position = new Vector3(x, y, 0);
            Feed = 0;
            Debug.Log("已生成");
            Month++;
        }
        if (Month >= 3)
        {
            product.SellMoney = sellm2;
        }
    }

    //刷新时间
    public void refreshTime()
    {
        //day = TimeSystemController.Instance.season.text + TimeSystemController.Instance.day.text;
    }

    public int getFeed()
    {
        return Feed;
    }

    public int getHealth()
    {
        return Health;
    }

    public int getMonth()
    {
        return Month;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            if (collision.gameObject.CompareTag("Player") && collision.collider.GetComponent<PlayerController>().sendFeed(food.type))
            {
                Debug.Log("colll");
                if (canFeed == true)
                {
                    SlotData food = collision.collider.GetComponent<PlayerController>().GetSelectedSlot();
                    food.ReduceOne();
                    Feed++;
                    Debug.Log("喂食！" + Feed);
                    //canFeed = false;
                }

                GameObject animalObject = GetComponent<GameObject>();
                Animal.UpdateAnimalToDatabase(animalObject);

            }
            else Debug.Log("不是食物");

            if (collision.gameObject.CompareTag("Player") && collision.collider.GetComponent<PlayerController>().sendMedicine(medicine.type))
            {
                SlotData medicine = collision.collider.GetComponent<PlayerController>().GetSelectedSlot();
                medicine.ReduceOne();
                Health = 100;
                Debug.Log(Health);
            }
        }
        catch (NullReferenceException e)
        {
            Debug.Log(e.ToString());
        }catch(Exception e)
        {
            Debug.Log(e.ToString());
        }

    }
}
