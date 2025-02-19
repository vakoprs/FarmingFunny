using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicHouseInstruction : MonoBehaviour
{
    public GameObject dialogFrame;
    public Text text;
    public bool isShow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isShow && Input.GetKeyDown(KeyCode.C))
        {
            dialogFrame.SetActive(true);
            text.text = "这里是神秘小屋。上午8：00——下午10：00。";
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
            if (collision.gameObject.CompareTag("Player"))
            {
                isShow = true;
            }
            if (collision.gameObject.CompareTag("Player")!)
            {
                throw new farm.ColliderException("碰撞不合理");
            }
        }
        catch(farm.ColliderException e)
        {
            Debug.Log(e.ToString());
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            isShow = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isShow = false;
    }
}
