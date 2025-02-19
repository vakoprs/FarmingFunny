using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
//时间表
public class Timee : MonoBehaviour
{
    //private string name = GameObject.FindGameObjectWithTag("Input").transform.GetChild(1).GetComponent<Text>().text;
    public float totalSeconds { get; set; }
    public int showMinute { get; set; }
    public int showSeconds { get; set; }
    public char seasonTime { get; set; }
    public int dayTime { get; set; }
    public int name { get; set; }

    public GameObject timeHelper;
    public GameObject player;
    public UnityEngine.UI.Button insertButton;

    private string connectionString = "server = 127.0.0.1;port = 3306;user= root;database = farm;password = 123456;charset=utf8";

    private void Start()
    {
        insertButton.onClick.AddListener(OnUpdateButtonClick);//更新
       
    }
    // 按钮点击事件，用于插入数据

    public void OnUpdateButtonClick()
    {

        UpdatePlayerToDatabase();
        //Debug.Log("anle");

    }
    //更新数据
    private async Task UpdatePlayerToDatabase()
    {
        //Debug.Log(name);
        float totalSeconds = timeHelper.GetComponent<TimeSystemController>().totalSeconds;
        int showMinute = timeHelper.GetComponent<TimeSystemController>().showMinute; 
        int showSeconds = timeHelper.GetComponent<TimeSystemController>().showSeconds; 
        int seansonTime = timeHelper.GetComponent<TimeSystemController>().seasonTime; 
        int dayTime  = timeHelper.GetComponent<TimeSystemController>().dayTime;
        string name = player.GetComponent<PlayerController>().name;

        // 构建连接
        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string query = "UPDATE time SET totalSeconds = @totalSeconds, showMinute = @showMinute, showSeconds = @showSeconds,seansonTime = @seansonTime,dayTime = @dayTime WHERE name = @name";
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@totalSeconds", totalSeconds);
                command.Parameters.AddWithValue("@showMinute", showMinute);
                command.Parameters.AddWithValue("@showSeconds", showSeconds);
                command.Parameters.AddWithValue("@seansonTime", seansonTime);
                command.Parameters.AddWithValue("@dayTime", dayTime);
                command.Parameters.AddWithValue("@name", name);


                int result = await command.ExecuteNonQueryAsync();
                if (result > 0)
                {
                    Debug.Log("Player data inserted successfully.");
                }
                else
                {
                    Debug.LogError("No data inserted.");
                }
            }
        }

    }
}
