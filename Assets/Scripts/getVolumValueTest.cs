using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class getVolumValueTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //加载新场景不会销毁物体
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGameObjectValue();
        GetGameObjectValue();
    }
    
    //
    public void UpdateGameObjectValue()
    {
        //获取激活的场景
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name == "MainScene")
        {
            GameObject audioSlider = GameObject.FindGameObjectWithTag("gameSettingUi").transform.GetChild(0).GetChild(1)
                .GetChild(1).GameObject();
            PlayerPrefs.SetFloat("audioVolumeValue", audioSlider.GetComponent<Slider>().value);
        }
    }
    //赋值
    public void GetGameObjectValue()
    {
        //获取激活的场景
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name == "Scene1")
        {
            GameObject scene1Audio = GameObject.Find("audioPlayTest").transform.gameObject;
            //float,int,string
            scene1Audio.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("audioVolumeValue");
        }
    }
}
