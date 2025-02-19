using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;


public class TimeSystemController : MonoBehaviour
{
    public static TimeSystemController Instance { get; private set; }
    public Text TimeShow;// 时间显示  24分钟为一天
   
    public Text season;// 季节
    
    public Text day;//日期
    
    public float totalSeconds = 0;// 累加时间
    
    public int showMinute = 6;// 分钟,初始进游戏为06：00
    
    public int showSeconds = 55;// 秒钟
    
    public int seasonTime = 1;// 季节切换判断,1春天，2夏天，3秋天，4冬天
    
    public int dayTime;// 日期，一个月31，一个月一个季节

    GameObject player;//用于取主角的属性


    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("MainCharacter");
    }
    private void FixedUpdate()
    {
        // 累加时间到1秒，然后重置秒钟为0（每秒钟执行一次）
        totalSeconds += Time.fixedDeltaTime;
        PlayerController otherScript=player.GetComponent<PlayerController>();
        //如果睡着了则直接进入下一天
        if (otherScript != null && otherScript.sleep > 0)
        {
            dayTime += otherScript.sleep;
            otherScript.sleep = 0;
            ChopTreeManager.Instance.Recover(transform.position);
            dayTime -= 1;
            showMinute = 6;
            showSeconds = 0;
            
            UpdateDayAndSeason();
            UpdateTime();
            totalSeconds = 0;

        }else  if (totalSeconds >= 0.98)
        {            
            UpdateDayAndSeason();
            UpdateTime();
            totalSeconds = 0;
        }
    }

    // 更新时间
    public void UpdateTime()
    {
        // 判断分钟是否为23
        if (showMinute == 23)
        {
            // 判断秒钟是否为59
            if (showSeconds == 59)
            {
                // 重置分钟和秒钟为0
                showMinute = 6;
                showSeconds = 0;
                
                TimeShow.text = "06 : 00";
            }
            else
            {
                // 秒钟累加
                showSeconds += 1;
                //判断秒钟位数是否为2，为1在前面加零
                if (showSeconds.ToString().Length == 1)
                {
                    string seconds = "0" + showSeconds.ToString();
                    TimeShow.text = "23 :" + seconds;
                }
                else
                {
                    TimeShow.text = "23 :" + showSeconds.ToString();
                }
            }
        }
        else
        {
            
            // 当时间不为23分钟时
            //判断秒钟是否为59秒
            if (showSeconds == 59)
            {
                // 重置秒钟为0；
                showSeconds = 0;
                // 分钟+1；
                showMinute += 1;
                // 判断分钟是否为2位，1位补0
                if (showMinute.ToString().Length == 1)
                {
                    TimeShow.text = "0" + showMinute.ToString() + " : 00";
                }
                else
                {
                    TimeShow.text = showMinute.ToString() + " : 00";
                }


            }
            else
            {
                // 当秒钟小于59
                showSeconds += 1;
                //判断秒钟位数
                // 保存分钟
                string minutes = showMinute.ToString();
                // 保存秒钟
                string seconds = showSeconds.ToString();

                // 分钟一位的时候补零
                if (minutes.Length == 1)
                {
                    minutes = "0" + showMinute.ToString();
                }
                // 秒钟一位补0
                if (seconds.Length == 1)
                {
                    seconds = "0" + showSeconds.ToString();
                }

                TimeShow.text = minutes + " : " + seconds;
            }
        }

    }

    // 更新日期和季节
    public void UpdateDayAndSeason()
    {
        // 判断是否为06：00
        if (showMinute == 6 && showSeconds == 0&&dayTime>=1)
        {
            ChopTreeManager.Instance.Recover(transform.position);
            // 判断是否是春天
            if (seasonTime == 1)
            {
                // 判断日期是否是31
                if (dayTime == 31)
                {
                    // 重置日期，为了调用下一个季节代码准备
                    dayTime = 1;
                    // 调整季节为夏天
                    seasonTime = 2;
                    // 更新季节与日子显示到界面
                    ColorUtility.TryParseHtmlString("#EE3838", out Color nowColor);
                    season.text = "夏";
                    season.color = nowColor;
                   
                    day.text = dayTime.ToString()+" 日";
                }
                else
                {
                    // 日期++
                    dayTime += 1;
                    // 更新日期到界面
                    day.text = dayTime.ToString() + "日";
                }
            }

            // 判断是否是夏天
            if (seasonTime == 2)
            {
                // 判断日期是否是30
                if (dayTime == 31)
                {
                    // 重置日期
                    dayTime = 1;
                    // 调整季节为夏天
                    seasonTime = 3;
                    // 更新季节与日期显示到界面
                    season.text = "秋";
                    ColorUtility.TryParseHtmlString("#CD0000", out Color nowColor);
                    season.color = nowColor;
                   
                    day.text = "1日";
                }
                else
                {
                    // 日期++
                    dayTime += 1;
                    // 更新日期到界面
                    day.text = dayTime.ToString() + "日";
                }
            }

            // 判断是否是秋天
            if (seasonTime == 3)
            {
                // 判断日期是否是31
                if (dayTime == 31)
                {
                    // 重置日期
                    dayTime = 1;
                    // 调整季节为冬天
                    seasonTime = 4;
                    // 更新季节与日期显示到界面
                    season.text = "冬";
                    ColorUtility.TryParseHtmlString("#FFFFFF", out Color nowColor);
                    season.color = nowColor;
                    
                    day.text = "1日";
                }
                else
                {
                    // 日期++
                    dayTime += 1;
                    // 更新日期到界面
                    day.text = dayTime.ToString() + "日";
                }
            }

            // 判断是否是冬天
            if (seasonTime == 4)
            {
                // 判断日期是否是30
                if (dayTime == 31)
                {
                    // 重置日期
                    dayTime = 1;
                    // 调整季节为春天
                    seasonTime = 1;
                    // 更新季节与日期显示到界面
                    season.text = "春";
                    ColorUtility.TryParseHtmlString("#5CACEE", out Color nowColor);
                    season.color = nowColor;
                    
                    day.text = "1日";
                }
                else
                {
                    // 日期++
                    dayTime += 1;
                    // 更新日期到界面
                    day.text = dayTime.ToString() + "日";
                }
            }
        }
        // 随时更新季节
        if (seasonTime == 1)
        {
            season.text = "春";
            ColorUtility.TryParseHtmlString("#35B32A", out Color nowColor);
            season.color = nowColor;
        }
        if (seasonTime == 2)
        {
            season.text = "夏";
            ColorUtility.TryParseHtmlString("#EE3838", out Color nowColor);
            season.color = nowColor;
        }
        if (seasonTime == 3)
        {
            season.text = "秋";
            ColorUtility.TryParseHtmlString("#D96D22", out Color nowColor);
            season.color = nowColor;
        }
        if (seasonTime == 4)
        {
            season.text = "冬";
            ColorUtility.TryParseHtmlString("#2287B0", out Color nowColor);
            season.color = nowColor;
        }
    }

    
}

