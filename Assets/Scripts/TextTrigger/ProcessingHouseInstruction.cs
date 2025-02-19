using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessingHouseInstruction : MonoBehaviour
{
    public GameObject dialogFrame;
    public Text text;
    public bool isShow;
    private Collider2D collision;

    // Start is called before the first frame update
    void Start()
    {
        //collision = GetComponent<Collider2D>();
        //dialogFrame.SetActive(false);
        //text.text = null;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isShow && Input.GetKeyDown(KeyCode.C))
        {
            dialogFrame.SetActive(true);  
            text.text= "这里是加工屋。上午8：00——下午10：00。";
        }

        if (isShow != true)
        {
            dialogFrame.SetActive(false);
            text.text = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            if (!collision.gameObject.CompareTag("Player"))
            {
                throw new farm.ColliderException(collision.gameObject.name);
            }
            if (collision.gameObject.CompareTag("Player"))
            {
                isShow = true;
            }
        }
        catch (farm.ColliderException e)
        {
            Debug.Log(e.Message);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isShow=false;
    }

    
    
   
}
