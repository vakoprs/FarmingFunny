using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class ConnectSql : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        //数据库地址、端口、用户名、数据库名、密码
        string sqlSer = "server = 127.0.0.1;port = 3306;user= root;database = cu;password = 123456;charset=utf8";
        //建立连接
        MySqlConnection conn = new MySqlConnection(sqlSer);
        try
        {
            conn.Open();
            Debug.Log("------链接成功------");
            //sql语句
            string sqlQuary = "SELECT * FROM university;";

            Debug.Log(sqlQuary);

            MySqlCommand comd = new MySqlCommand(sqlQuary, conn);

            MySqlDataReader reader = comd.ExecuteReader();

            while (reader.Read())
            {
                //通过reader获得数据库信息
                Debug.Log(reader.GetString("University"));
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("Error:" + e.Message);
        }
        finally
        {
            conn.Close();
        }
    }
    void Update()
    {

    }

}
