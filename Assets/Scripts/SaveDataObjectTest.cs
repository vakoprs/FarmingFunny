using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveDataObjectTest : MonoBehaviour
{
    //玩家
    public class DataForPlayer
    {
        public float Movespeed;//移动速度
        public int sleep=0;//睡眠
        public bool flag = false;
        public ToolbarUI toolbarUI;
        public int fatigue=0;//疲劳度
        //玩家坐标
        public float playerX;
        public float playerY;
        public float playerZ;
        //存档时间
        public string dataSaveTime;
        //当前场景
        public string currentSceneName;
    }
    //时间系统
    public class DataForTime
    {
        public Text TimeShow;// 时间显示  24分钟为一天
   
        public Text season;// 季节
    
        public Text day;//日期
    
        public float totalSeconds = 0;// 累加时间
    
        public int showMinute = 6;// 分钟,初始进游戏为06：00
    
        public int showSeconds = 1;// 秒钟
    
        public int seasonTime = 1;// 季节切换判断,1春天，2夏天，3秋天，4冬天
    
        public int dayTime;// 日期，一个月31，一个月一个季节
    }
    //NPC数据
    //其他数据
}
