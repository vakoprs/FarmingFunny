using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimalGrowController : MonoBehaviour
{
    public GameObject mature;
    private GameObject now;
    public ItemData food;
    public ItemData medicine;
    public int matureNum;

    int dayCount;

    bool destroy;
    int Health;
    bool canFeed;
    private string day;
    private int id;
    // Start is called before the first frame update
    void Start()
    {
        now= GetComponent<GameObject>();
        dayCount = 0;
        canFeed = true;
        Health = 100;
        destroy = false;

        GameObject origin = GetComponent<GameObject>();
        id = origin.GetComponent<AnimalController>().AnimalID;

        Debug.Log(id);
        //day = TimeSystemController.Instance.season.text+TimeSystemController.Instance.day.text;
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
        ////};
        //if (destroy)
        //{
        //    Invoke("disappear", 2f);

        //}
        try
        {
            if (dayCount > matureNum)
            {
                destroy = true;
                Destroy(GetComponent<Animator>());
                Destroy(gameObject);
                Destroy(this);
            }
        }catch(NullReferenceException e)
        {
            Debug.Log(e.ToString());
        }
        

    }
    private async void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            if (dayCount >= matureNum)
            {
                destroy = true;
                Destroy(GetComponent<Animator>());
                Destroy(gameObject);
                Destroy(this);
            }
            if (collision.gameObject.CompareTag("Player") && collision.collider.GetComponent<PlayerController>().sendFeed(food.type))
            {
                SlotData food = collision.collider.GetComponent<PlayerController>().GetSelectedSlot();
                food.ReduceOne();
                dayCount++;
                canFeed = false;
                if (dayCount == matureNum)
                {

                    GameObject matureP = GameObject.Instantiate(mature);
                    GameObject parent = GameObject.Find(mature.GetComponent<AnimalController>().SceneName);
                    int id = UnityEngine.Random.Range(200, 400);
                    matureP.GetComponent<AnimalController>().AnimalID = id;
                    matureP.transform.SetParent(parent.transform, false);
                    matureP.transform.position = now.transform.position;
                    //Animal.InsertAnimalToDatabase(matureP);

                    print(matureP.GetComponent<AnimalController>().Type.ToString());
                    //dayCount = 100;
                }


            }
            if (collision.gameObject.CompareTag("Player") && collision.collider.GetComponent<PlayerController>().sendMedicine(medicine.type))
            {
                SlotData medicine = collision.collider.GetComponent<PlayerController>().GetSelectedSlot();
                medicine.ReduceOne();
                Health = 100;
                //Debug.Log(Health);
            }
        }catch(NullReferenceException e)
        {
            Debug.Log(e.ToString());
        }
        

    }

}
