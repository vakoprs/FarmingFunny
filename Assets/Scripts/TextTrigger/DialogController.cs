using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogController : MonoBehaviour
{
    public GameObject dialogFrame;
    public Text textLog;
    public TextAsset textFile;
    public int index;
    List<string> textList=new List<string>();
    public bool isShow;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        dialogFrame.SetActive(false);
        textLog.text = null;
        GetFromFile(textFile);
         
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (isShow && Input.GetKeyDown(KeyCode.C))
            {
                dialogFrame.SetActive(true);
                if (index == textList.Count + 1)//
                {
                    isShow = false;
                    dialogFrame.SetActive(false);
                    textLog.text = null;
                    index = 0;
                }
                textLog.text = textList[index];
                index++;
            }
            if (isShow != true)
            {
                textLog.text = null;
                dialogFrame.SetActive(false);
                index = 0;
                return;
            }
        }
        catch(IndexOutOfRangeException e)
        {
            //isShow= false;
            Debug.LogException(e);
        }
        
    }

    void GetFromFile(TextAsset textAsset)
    {
        try
        {
            textList.Clear();
            int index = 0;
            var logData = textAsset.text.Split('\n');
            foreach (var line in logData)
            {
                textList.Add(line);
            }

        }
        catch (NullReferenceException e)
        {

        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            isShow = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isShow = false;
        index = 0;
    }
}
