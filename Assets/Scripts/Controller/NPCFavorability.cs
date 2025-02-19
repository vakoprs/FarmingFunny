using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFavorability : MonoBehaviour
{
    private GameObject NPC;

    public GameObject happyImage;
    public GameObject normalImage;

    public ItemData like;
    int Favorability;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            NPC = GetComponent<GameObject>();
            Favorability = 0;
            happyImage.SetActive(false);
            normalImage.SetActive(false);
        }
        catch(NullReferenceException e)
        {
            Debug.Log(e);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getFavor()
    {
        return Favorability;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SlotData slotData = collision.collider.GetComponent<PlayerController>().GetSelectedSlot();
            if (slotData.item== like)
            {
                Favorability += slotData.item.favor;
                slotData.ReduceOne();
                happyImage.SetActive(true);
                Invoke("disappear", 2f);

            }else 
            {
                Favorability += slotData.item.favor;
                slotData.ReduceOne();
                normalImage.SetActive(true);
                Invoke("disappear",2f);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(Favorability);
    }
    public void disappear()
    {
        happyImage.SetActive(false);
        normalImage.SetActive(false);
    }

}
