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
using Unity.VisualScripting;

//player表
public class Player : MonoBehaviour
{
      //private string name = GameObject.FindGameObjectWithTag("Input").transform.GetChild(1).GetComponent<Text>().text;
    public string name { get; set; }
    public int moveSpeed { get; set; }
    public int fatigue { get; set; }
    public int health { get; set; }
    public int sleep { get; set; }
    public bool flag { get; set; }
    public bool canChop { get; set; }
    public float x { get; set; }
    public float y { get; set; }
    public string sceneName { get; set; }

    public InputField inputField;
    public GameObject player;
    public UnityEngine.UI.Button insertButton;

    private string connectionString = "server = 127.0.0.1;port = 3306;user= root;database = farm;password = 123456;charset=utf8";

    private void Start()
    {
      
       //insertButton.onClick.AddListener(OnUpdateButtonClick);//更新
        insertButton.onClick.AddListener(OnInsertButtonClicked);//注册
    }
    // 按钮点击事件，用于插入数据
    public void OnInsertButtonClicked()
    {
        try
        {
            string playerName = inputField.text;
            player.GetComponent<PlayerController>().name = playerName;
            InsertPlayerToDatabase(playerName);
            Debug.Log("gggg");
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        
    }
    public void OnUpdateButtonClick()
    {
        try
        {
            UpdatePlayerToDatabase();
            //Debug.Log("anle");
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        

    }
    //更新数据
    public async Task UpdatePlayerToDatabase()
    {
        string name =player.GetComponent<PlayerController>().name;
        
        int moveSeed =(int)player.GetComponent<PlayerController>().Movespeed; 
        int fatigue = player.GetComponent<PlayerController>().fatigue; 
        int health =100-fatigue; 
        int sleep = player.GetComponent<PlayerController>().sleep;
        bool flag = player.GetComponent<PlayerController>().flag; 
        bool canChop = player.GetComponent<PlayerController>().canChop;
        float x=player.transform.position.x;
        float y = player.transform.position.y;
        string sceneName = SceneManager.GetActiveScene().name;//当前场景
        Debug.Log(name);
        // 构建连接
        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string query = "UPDATE player SET moveSeed = @moveSeed, fatigue = @fatigue, health = @health, sleep = @sleep, flag = @flag, canChop = @canChop,x=@x,y=@y,sceneName = @scnenName WHERE name = @name";//x = @x WHERE name = @name,y = @y WHERE name = @name,sceneName = @scnenName WHERE name = @name
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@moveSeed", moveSeed);
                command.Parameters.AddWithValue("@fatigue", fatigue);
                command.Parameters.AddWithValue("@health", health);
                command.Parameters.AddWithValue("@sleep", sleep);
                command.Parameters.AddWithValue("@flag", flag);
                command.Parameters.AddWithValue("@canChop", canChop);
                command.Parameters.AddWithValue("@x", x);
                command.Parameters.AddWithValue("@y", y);
                command.Parameters.AddWithValue("@sceneName", sceneName);

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
    // 异步插入玩家数据到数据库
    private async Task InsertPlayerToDatabase(string playerName)
    {
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogError("Input field is empty.");
            return;
        }
        //Debug.Log(name);
        name = playerName;
        int moveSeed = 0; // 请根据实际情况设置
        int fatigue = 0; // 请根据实际情况设置
        int health = 100; // 假设默认健康值为100
        int sleep = 0; // 请根据实际情况设置
        bool flag = false; // 假设默认值为false
        bool canChop = false; // 假设默认值为false
        float x = 0;
        float y = 0;
        string sceneName = null;//当前场景

       /* int totalSeconds = 0;
        int showMinute = 0;
        int showSeconds = 0;
        char seansonTime = '1';
        int dayTime = 0;*/

        // 构建连接
        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string query = "INSERT INTO player (name, moveSeed, fatigue, health, sleep, flag, canChop,x,y,sceneName) VALUES (@name, @moveSeed, @fatigue, @health, @sleep, @flag, @canChop,@x,@y,@sceneName)";//
            //string query2 = "INSERT INTO time  (totalSeconds, showMinute, showSeconds, seansonTime, dayTime) VALUES (@totalSeconds, @showMinute, @showSeconds, @seansonTime, @dayTime)";
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@moveSeed", moveSeed);
                command.Parameters.AddWithValue("@fatigue", fatigue);
                command.Parameters.AddWithValue("@health", health);
                command.Parameters.AddWithValue("@sleep", sleep);
                command.Parameters.AddWithValue("@flag", flag);
                command.Parameters.AddWithValue("@canChop", canChop);
                command.Parameters.AddWithValue("@x", x);
                command.Parameters.AddWithValue("@y", y);
                command.Parameters.AddWithValue("@scnenName", sceneName);


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
            /*using (var command = new MySqlCommand(query2, connection))
            {
                command.Parameters.AddWithValue("@totalSeconds", totalSeconds);
                command.Parameters.AddWithValue("@showMinute", showMinute);
                command.Parameters.AddWithValue("@showSeconds", showSeconds);
                command.Parameters.AddWithValue("@seansonTime", seansonTime);
                command.Parameters.AddWithValue("@ dayTime", dayTime);
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
            }*/
        }

    }
}

//Animal表
public class Animal : MonoBehaviour
{
    public int AnimalID { get; set; }
    public string Type { get; set; }
    public int BuyMoney { get; set; }
    public int sellMoney { get; set; }
    public int Feed { get; set; }
    public string  productType { get; set; }
    public int Health { get; set; }
    public int Month { get; set; }
   
    public GameObject player;
    public GameObject animal;
    public UnityEngine.UI.Button insertButton;

    private static string connectionString = "server = 127.0.0.1;port = 3306;user= root;database = farm;password = 123456;charset=utf8";

    private void Start()
    {
        insertButton.onClick.AddListener(OnUpdateButtonClick);
        insertButton.onClick.AddListener(OnInsertButtonClicked);
    }
    // 按钮点击事件，用于插入数据
    public void OnInsertButtonClicked()
    {
        try
        {
            InsertAnimalToDatabase(animal);
            //Debug.Log("gggg");
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        
    }
    public void OnUpdateButtonClick()
    {
        try
        {
            UpdateAnimalToDatabase(animal);
            //Debug.Log("anle");
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
       

    }
    //更新数据
    public static async Task UpdateAnimalToDatabase(GameObject animal)
    {
        AnimalProducingController animalProducingController = animal.GetComponent<AnimalProducingController>();
        int AnimalID = animal.GetComponent<AnimalController>().AnimalID;
        int Feed = animalProducingController.getFeed(); // 请根据实际情况设置
        int Health = animalProducingController.getHealth(); // 请根据实际情况设置
        int Month = animalProducingController.getMonth(); // 假设默认健康值为100


        // 构建连接
        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string query = "UPDATE Animal SET Feed = @Feed, Health = @Health, Month = @Month WHERE AnimalID = @AnimalID";//x = @x WHERE name = @name,y = @y WHERE name = @name,sceneName = @scnenName WHERE name = @name
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@AnimalID", AnimalID);
                command.Parameters.AddWithValue("@Feed", Feed);
                command.Parameters.AddWithValue("@Health", Health);
                command.Parameters.AddWithValue("@Month", Month);

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
    // 异步插入玩家数据到数据库
    public static async Task InsertAnimalToDatabase(GameObject animal)
    {
        AnimalController animalController = animal.GetComponent<AnimalController>();
        Debug.Log(animalController.AnimalID);
        //Debug.Log(name);
        int AnimalID=animalController.AnimalID;
        string AnimalType = animalController.Type;
        int BuyMoney=0;
        int sellMoney=0;
        string productType="Default";
        switch (AnimalType)
        {
            case "Chicken":
                BuyMoney = 50;
                sellMoney = 30;
                productType = "egg";
                break;
            case "ChickenBaby":
                BuyMoney = 50;
                sellMoney = 30;
                productType = "null";
                break;
            case "Sheep":
                BuyMoney = 100;
                sellMoney = 60;
                productType = "sheepMilk";
                break;
            case "Cow":
                BuyMoney = 150;
                sellMoney = 90;
                productType = "milk";
                break;
            case "Pig":
                BuyMoney = 90;
                sellMoney = 50;
                break;
            case "PigBaby":
                BuyMoney = 90;
                sellMoney = 50;
                break;

        }

        int Feed = 0; // 请根据实际情况设置
        int Health = 100; // 请根据实际情况设置
        int Month = 0;

        AnimalProducingController animalProducingController=animal.GetComponent<AnimalProducingController>();
        if(animalProducingController != null)
        {
            Feed = animalProducingController.getFeed(); // 请根据实际情况设置
            Health = animalProducingController.getHealth(); // 请根据实际情况设置
            Month = animalProducingController.getMonth(); // 假设默认健康值为100
        }
        

        // 构建连接
        using (var connection = new MySqlConnection(Animal.connectionString))
        {
            await connection.OpenAsync();

            string query = "INSERT INTO Animal (AnimalID, Type, BuyMoney, sellMoney, Feed, productType, Health,Month) VALUES (@AnimalID, @Type, @BuyMoney, @sellMoney, @Feed, @productType, @Health,@Month)";
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@AnimalID", AnimalID);
                command.Parameters.AddWithValue("@Type", AnimalType);
                command.Parameters.AddWithValue("@BuyMoney", BuyMoney);
                command.Parameters.AddWithValue("@sellMoney", sellMoney);
                command.Parameters.AddWithValue("@Feed", Feed);
                command.Parameters.AddWithValue("@productType", productType);
                command.Parameters.AddWithValue("@Health", Health);
                command.Parameters.AddWithValue("@Month", Month);


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

//NPC表
public class NPC : MonoBehaviour
{
    //private string name = GameObject.FindGameObjectWithTag("Input").transform.GetChild(1).GetComponent<Text>().text;
    public string lname { get; set; }
    public int age { get; set; }
    public string work { get; set; }
    public int love { get; set; }
    public string like { get; set; }
    public string sex { get; set; }

    public GameObject player;
    public GameObject npc;
    public UnityEngine.UI.Button insertButton;

    private static string connectionString = "server = 127.0.0.1;port = 3306;user= root;database = farm;password = 123456;charset=utf8";

    private void Start()
    {
        insertButton.onClick.AddListener(OnUpdateButtonClick);//更新
    }
    public void OnUpdateButtonClick()
    {

        //UpdatePlayerToDatabase();
        //Debug.Log("anle");
        InsertNPCToDatabase();

    }
    //更新数据
    public async Task UpdatePlayerToDatabase()
    {
        string name=null;
        int love=0;
          try
        {
            // 构建连接
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE NPC SET love = @love WHERE name = @name";//x = @x WHERE name = @name,y = @y WHERE name = @name,sceneName = @scnenName WHERE name = @name
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@love", love);

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
        catch (Exception e)
        {
            Debug.LogException(e);
        }


    }
    // 异步插入玩家数据到数据库
    public static async Task InsertNPCToDatabase()/*GameObject NPCGameObject*/
    {
        //NPCInformation NPCinfo=NPCGameObject.GetComponent<NPCInformation>();
        //string name = NPCinfo.name;
        //int age=NPCinfo.age;
        //string work=NPCinfo.work;
        //int love=NPCinfo.getLove();
        //string like = NPCinfo.getLike().type.ToString();
        //string sex = NPCinfo.sex;
        string name = "hhh";
        int age = 2;
        string work = "abab";
        int love = 5;
        string like ="egg";
        string sex ="male";

        try
        {
            // 构建连接
            using (var connection = new MySqlConnection(NPC.connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO NPC (name, age, work, love, like, sex) VALUES (@name, @age, @work, @love, @like, @sex)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@age", age);
                    command.Parameters.AddWithValue("@work", work);
                    command.Parameters.AddWithValue("@love", love);
                    command.Parameters.AddWithValue("@like", like);
                    command.Parameters.AddWithValue("@sex", sex);


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
        catch (Exception e)
        {
            Debug.LogException(e);
        }

    }
}














