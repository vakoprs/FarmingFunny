using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataTest : MonoBehaviour
{
    public void returnMain()
    {
        GameObject mainMenu = GameObject.FindGameObjectWithTag("mainMenu").transform.gameObject;
        //存档界面
        GameObject saveDataUi = GameObject.FindGameObjectWithTag("saveData").transform.gameObject;
        //显示和隐藏
        mainMenu.transform.GetChild(0).gameObject.SetActive(true);
        saveDataUi.transform.GetChild(0).gameObject.SetActive(false);
    }
}
