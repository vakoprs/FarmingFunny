using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VolunmController : MonoBehaviour
{
    //控制的声音
    private AudioSource menuAudio;
    //滑动条
    private Slider audioSlider;

    private void Start()
    {
        menuAudio = GameObject.FindGameObjectWithTag("mainMenu").transform.GetComponent<AudioSource>();
        audioSlider=GetComponent<Slider>();
    }

    private void Update()
    {
        VolumeControl();
    }

    //控制的声音/音效
    public void VolumeControl()
    {
        //控制声音
        menuAudio.volume = audioSlider.value;
        //同时控制多个声音，把声音音量和滑动条挂钩
        
    }
    //关闭游戏设置画面
    public void CloseSetting()
    {
        //主菜单
        GameObject mainMenu = GameObject.FindGameObjectWithTag("mainMenu").transform.gameObject;
        //设置界面
        GameObject gameSetting = GameObject.FindGameObjectWithTag("gameSettingUi").transform.gameObject;
        //显示和隐藏
        mainMenu.transform.GetChild(0).gameObject.SetActive(true);
        gameSetting.transform.GetChild(0).gameObject.SetActive(false);
    }
}
