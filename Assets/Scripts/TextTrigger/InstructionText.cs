using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionText : MonoBehaviour
{
    public GameObject dialogFrame;
    public GameObject ProcessingHouse;
    public GameObject MineralHouse;
    public Text text;
    public bool isShow;
    public Text textModel;
    // Start is called before the first frame update
    void Start()
    {
        
        ProcessingHouse.SetActive(true);
        MineralHouse.SetActive(true);
        dialogFrame.SetActive(false);
        isShow = false;
        text.text = null;
        textModel = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(isShow)
        {
            dialogFrame.SetActive(true);
            text.text = textModel.text;
            
        }
        if (isShow == false)
        {
            dialogFrame.SetActive(false);
            text.text = null;
            textModel.text = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            if (collision.gameObject != ProcessingHouse && collision.gameObject != MineralHouse)
            {
                throw new farm.ColliderException("");
            }
            if (collision.gameObject == ProcessingHouse && Input.GetKeyDown(KeyCode.C))
            {
                isShow = true;
                textModel.text = "这里是加工屋！";
            }
            if (collision.gameObject == MineralHouse && Input.GetKeyDown(KeyCode.C))
            {
                isShow = true;
                textModel.text = "这里是挖矿屋！";
            }
        }
        catch(farm.ColliderException e)
        {
            Debug.LogException(e);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isShow=false;
    }
    
}
