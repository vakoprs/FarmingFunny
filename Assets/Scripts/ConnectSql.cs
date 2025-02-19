using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class ConnectSql : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        //���ݿ��ַ���˿ڡ��û��������ݿ���������
        string sqlSer = "server = 127.0.0.1;port = 3306;user= root;database = cu;password = 123456;charset=utf8";
        //��������
        MySqlConnection conn = new MySqlConnection(sqlSer);
        try
        {
            conn.Open();
            Debug.Log("------���ӳɹ�------");
            //sql���
            string sqlQuary = "SELECT * FROM university;";

            Debug.Log(sqlQuary);

            MySqlCommand comd = new MySqlCommand(sqlQuary, conn);

            MySqlDataReader reader = comd.ExecuteReader();

            while (reader.Read())
            {
                //ͨ��reader������ݿ���Ϣ
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
