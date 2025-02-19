using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class menutest : MonoBehaviour
{
    public GameObject FirstScene;
    public GameObject[] PackTool;
    public GameObject LastScene;
    //开始游戏
    public void StartGame()
    {
        //加载场景下标
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        try
        {
            FirstScene.SetActive(true);
            LastScene.SetActive(false);
            if (PackTool != null)
            {
                foreach (var tool in PackTool)
                {
                    tool.SetActive(true);
                }
            }
        }
        catch(NullReferenceException e)
        {
            Debug.Log(e.ToString());
        }
        

    }
    //继续游戏（存档读档）
    public void ContinueGame()
    {
        try
        {
            //主菜单
            GameObject SceneManager = GameObject.FindGameObjectWithTag("SceneManger").transform.gameObject;
            //存档界面
            GameObject saveDataUi = GameObject.FindGameObjectWithTag("saveData").transform.gameObject;
            //显示和隐藏
            SceneManager.transform.GetChild(0).gameObject.SetActive(false);
            saveDataUi.transform.GetChild(0).gameObject.SetActive(true);

            LastScene.SetActive(false);

        }
        catch (NullReferenceException e)
        {
            Debug.Log(e.ToString());
        }
    }
    //游戏设置
    public void OpenGameSettingUI()
    {
        try
        {
            //主菜单
            GameObject SceneManager = GameObject.FindGameObjectWithTag("SceneManger").transform.gameObject;
            //设置界面
            //GameObject gameSetting = GameObject.FindGameObjectWithTag("gameSettingUi").transform.gameObject;
            //显示和隐藏
            SceneManager.transform.GetChild(0).gameObject.SetActive(false);
            SceneManager.transform.GetChild(3).GetChild(1).gameObject.SetActive(true);
            SceneManager.transform.GetChild(3).GetChild(1).GetChild(0).gameObject.SetActive(true);
            //gameSetting.transform.GetChild(0).gameObject.SetActive(true);

        }
        catch (NullReferenceException e)
        {
            Debug.Log(e.ToString());
        }
    }

    //退出游戏设置
    public void CloseGameSettingUI()
    {
        try
        {
            //主菜单
            GameObject SceneManager = GameObject.FindGameObjectWithTag("SceneManger").transform.gameObject;
            //设置界面
            GameObject gameSetting = GameObject.FindGameObjectWithTag("gameSettingUi").transform.gameObject;
            //显示和隐藏
            //mainMenu.transform.GetChild(0).gameObject.SetActive(true);
            gameSetting.transform.GetChild(0).gameObject.SetActive(false);
            SceneManager.transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
            SceneManager.transform.GetChild(3).GetChild(0).gameObject.SetActive(true);
        }
        catch (NullReferenceException e)
        {
            Debug.Log(e.ToString());
        }
        

    }
    //退出游戏
    public void EXitGame()
    {
        Application.Quit();
    }
}
