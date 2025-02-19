using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
using System.Security.Cryptography.X509Certificates;

public class DataPanelTest : MonoBehaviour
{
    Player dataPlayer = new Player();
    //判断是读档还是存档
    public int dataType = 1;
    public static DataPanelTest Instance { get; private set; }
    public TMPro.TextMeshProUGUI time1;
    public TMPro.TextMeshProUGUI address1;
    public TMPro.TextMeshProUGUI time2;
    public TMPro.TextMeshProUGUI address2;
    public TMPro.TextMeshProUGUI time3;
    public TMPro.TextMeshProUGUI address3;
    public TMPro.TextMeshProUGUI time4;
    public TMPro.TextMeshProUGUI address4;
    public TMPro.TextMeshProUGUI time5;
    public TMPro.TextMeshProUGUI address5;
    public TMPro.TextMeshProUGUI time6;
    public TMPro.TextMeshProUGUI address6;
    public List<GameObject> scene;
    public GameObject father;
    public List<GameObject> save;
    private void Start()
    {
        try
        {
            if (dataType == 1)
            {
                //获取data所有按钮
                Button data1Button = GameObject.FindGameObjectWithTag("saveData").transform.GetChild(0).GetChild(0)
                    .GetChild(0).GetChild(0).GetComponent<Button>();
                Button data2Button = GameObject.FindGameObjectWithTag("saveData").transform.GetChild(0).GetChild(0)
                    .GetChild(0).GetChild(1).GetComponent<Button>();
                Button data3Button = GameObject.FindGameObjectWithTag("saveData").transform.GetChild(0).GetChild(0)
                    .GetChild(0).GetChild(2).GetComponent<Button>();
                //先清除所有绑定方法
                data1Button.onClick.RemoveAllListeners();
                data2Button.onClick.RemoveAllListeners();
                data3Button.onClick.RemoveAllListeners();
                //绑定方法
                data1Button.onClick.AddListener(delegate { saveData1(); });
                data2Button.onClick.AddListener(delegate { saveData2(); });
                data3Button.onClick.AddListener(delegate { saveData3(); });

            }
            else
            {

                Button data1Button = GameObject.FindGameObjectWithTag("ReadData").transform.GetChild(0).GetChild(0)
                    .GetChild(0).GetChild(0).GetComponent<Button>();
                Button data2Button = GameObject.FindGameObjectWithTag("ReadData").transform.GetChild(0).GetChild(0)
                    .GetChild(0).GetChild(1).GetComponent<Button>();
                Button data3Button = GameObject.FindGameObjectWithTag("ReadData").transform.GetChild(0).GetChild(0)
                    .GetChild(0).GetChild(2).GetComponent<Button>();
                //先清除所有绑定方法
                data1Button.onClick.RemoveAllListeners();
                data2Button.onClick.RemoveAllListeners();
                data3Button.onClick.RemoveAllListeners();
                //绑定方法
                data1Button.onClick.AddListener(delegate { loadData1(); });
                data2Button.onClick.AddListener(delegate { loadData2(); });
                data3Button.onClick.AddListener(delegate { loadData3(); });
            }
        }
        catch (NullReferenceException e)
        {
            Debug.Log(e.ToString());
        }

    }
    //点击按钮打开存读档界面同时关闭系统界面
    //存档按钮
    public void SaveDataButton()
    {
        try
        {
            DataPanelTest dataPanelTest =
                GameObject.FindGameObjectWithTag("SceneManger").transform.GetComponent<DataPanelTest>();
            dataPanelTest.dataType = 1;
            //点击执行代码
            //关闭系统界面
            GameObject.FindGameObjectWithTag("SceneManger").transform.GetChild(0).gameObject.SetActive(false);
            //打开存档界面
            GameObject.FindGameObjectWithTag("saveData").transform.GetChild(0).gameObject.SetActive(true);

        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }

    }

    //读档按钮
    public void LoadDataButton()
    {
        try
        {
            //变更状态
            DataPanelTest dataPanelTest =
                GameObject.FindGameObjectWithTag("SceneManger").transform.GetComponent<DataPanelTest>();
            dataPanelTest.dataType = 2;
            //关闭系统界面
            GameObject.FindGameObjectWithTag("SceneManger").transform.GetChild(0).gameObject.SetActive(false);
            //打开存档界面
            GameObject.FindGameObjectWithTag("ReadData").transform.GetChild(0).gameObject.SetActive(true);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }

    }

    //回主菜单
    public void BackMenuButton()
    {

    }

    //返回
    public void SaveReturn()
    {
        try
        {
            //系统界面
            GameObject.FindGameObjectWithTag("SceneManger").transform.GetChild(0).gameObject.SetActive(true);
            //存档界面
            GameObject.FindGameObjectWithTag("saveData").transform.GetChild(0).gameObject.SetActive(false);

        }
        catch (NullReferenceException e)
        {
            Debug.LogError(e.ToString());
        }
    }

    public void ReadReturn()
    {
        try
        {
            //系统界面
            GameObject.FindGameObjectWithTag("SceneManger").transform.GetChild(0).gameObject.SetActive(true);
            //存档界面
            GameObject.FindGameObjectWithTag("ReadData").transform.GetChild(0).gameObject.SetActive(false);

        }
        catch (NullReferenceException e)
        {
            Debug.LogError(e.ToString());
        }
    }

    public void Setting()
    {

        try
        {
            //存档界面
            GameObject.FindGameObjectWithTag("SceneManger").transform.GetChild(0).gameObject.SetActive(true);

        }
        catch (NullReferenceException e)
        {
            Debug.LogError(e.ToString());
        }
    }

    public void MainReturn()
    {

        try
        {
            //存档界面
            GameObject.FindGameObjectWithTag("SceneManger").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("SceneManger").transform.GetChild(3).GetChild(0).gameObject.SetActive(true);

        }
        catch (NullReferenceException e)
        {
            Debug.LogError(e.ToString());
        }
    }

    //返回游戏
    public void ReturnGame()
    {
        try
        {
            GameObject.FindGameObjectWithTag("SceneManger").transform.GetChild(0).gameObject.SetActive(false);

        }
        catch (NullReferenceException e)
        {
            Debug.LogError(e.ToString());
        }
    }
    //点击不同数据进行不同操作
    public void saveData1()
    {
        try
        {
            string saveTime = DateTime.Now.ToString("yyyy-MM-dd: hh:mm:ss"); //目前存档时间
                                                                             //显示存档后的内容
                                                                             //查找需要变更文本的物体
            time1.text = saveTime;
            time4.text = saveTime;
            string addr = null;
            foreach (var i in scene)
            {
                if (i.activeSelf == true)
                {
                    addr = i.name;
                    save.Add(i);
                    GameObject.FindGameObjectWithTag("ReadData").transform.GetChild(0).gameObject.SetActive(false);
                    break;
                }
            }
            address1.text = addr;
            address4.text = addr;
        }
        catch (NullReferenceException e)
        {
            Debug.LogError(e.ToString());
        }
        
    }

    public void loadData1()
    {
        try
        {
            string add = address4.text;
            foreach (var i in scene)
            {
                if (i.activeSelf == true)
                {
                    i.SetActive(false);
                }
            }
            foreach (var i in save)
            {
                if (i.name == add)
                {
                    i.SetActive(true);
                    GameObject.FindGameObjectWithTag("ReadData").transform.GetChild(0).gameObject.SetActive(false);
                    break;
                }
            }
        }
        catch (NullReferenceException e)
        {
            Debug.LogError(e.ToString());
        }
        
    }

    public void saveData2()
    {
        try
        {
            string saveTime = DateTime.Now.ToString("yyyy-MM-dd: hh:mm:ss"); //目前存档时间
                                                                             //显示存档后的内容
                                                                             //查找需要变更文本的物体
            time2.text = saveTime;
            time5.text = saveTime;
            string addr = null;
            foreach (var i in scene)
            {
                if (i.activeSelf == true)
                {
                    addr = i.name;
                    save.Add(i);
                    GameObject.FindGameObjectWithTag("ReadData").transform.GetChild(0).gameObject.SetActive(false);
                    break;
                }
            }
            address2.text = addr;
            address5.text = addr;
        }
        catch (NullReferenceException e)
        {
            Debug.LogError(e.ToString());
        }
  
    }

    public void loadData2()
    {
        try
        {
            string add = address2.text;
            foreach (var i in scene)
            {
                if (i.activeSelf == true)
                {
                    i.SetActive(false);
                }
            }
            foreach (var i in save)
            {
                if (i.name == add)
                {
                    i.SetActive(true);
                    GameObject.FindGameObjectWithTag("ReadData").transform.GetChild(0).gameObject.SetActive(false);
                    break;
                }
            }
        }
        catch (NullReferenceException e)
        {
            Debug.LogError(e.ToString());
        }

        
    }

    public void saveData3()
    {
        try
        {
            string saveTime = DateTime.Now.ToString("yyyy-MM-dd: hh:mm:ss"); //目前存档时间
                                                                             //显示存档后的内容
                                                                             //查找需要变更文本的物体
            time3.text = saveTime;
            time6.text = saveTime;
            string addr = null;
            foreach (var i in scene)
            {
                if (i.activeSelf == true)
                {
                    addr = i.name;
                    save.Add(i);
                    GameObject.FindGameObjectWithTag("ReadData").transform.GetChild(0).gameObject.SetActive(false);
                    break;
                }
            }
            address3.text = addr;
            address6.text = addr;
        }
        catch (NullReferenceException e)
        {
            Debug.LogError(e.ToString());
        }
        
    }

    public void loadData3()
    {
        try
        {
            string add = address3.text;
            foreach (var i in scene)
            {
                if (i.activeSelf == true)
                {
                    i.SetActive(false);
                }
            }
            foreach (var i in save)
            {
                if (i.name == add)
                {
                    i.SetActive(true);
                    GameObject.FindGameObjectWithTag("ReadData").transform.GetChild(0).gameObject.SetActive(false);
                    break;
                }
            }
        }
        catch (NullReferenceException e)
        {
            Debug.LogError(e.ToString());
        }

    }
}