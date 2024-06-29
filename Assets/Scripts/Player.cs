using UnityEngine;
using Newtonsoft.Json;  // Json解析库
using System.IO;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public static float moveSpeed = 1f; // 角色移动速度
    private float rotateSpeed = 60f; // 角色旋转速度
    private List<float> speedList = new List<float>(); // 存储角色各级对应的速度
    private LayerMask DoorLayer;  // 层级，outSide
    public bool isOpenDoor; // 是否开门
    private float raycastDistance = 1f;  // 射线发射的距离
    public RaycastHit hitInfo; // 定义一个RaycastHit变量用来保存被撞物体的信息
    private Animator PlayerAnim; // 人物动画控制器

    void Start()
    {
        DoorLayer = LayerMask.GetMask("Door"); // 将门的层级改为Door
        PlayerAnim = transform.GetComponent<Animator>(); // 获取Animator组件
        
        // 读取json，设置主角的位置和旋转
        string pathJson = Application.dataPath + "/Json/Player.json";
        if (File.Exists(pathJson))
        {
            // 打开文件，并返回一个读取的文件流对象
            StreamReader reader = File.OpenText(pathJson);
            string json = reader.ReadToEnd(); // 读取文件的全部内容
            // 反序列化，将读取到的Json文本转化成数据类对象
            PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(json);
            
            // 更新主角的位置
            transform.position = new Vector3(playerData.Position[0], playerData.Position[1], playerData.Position[2]);
            
            // 将欧拉角以四元数的方式赋值给transform.rotation, 即将欧拉角转换成四元数
            transform.rotation = Quaternion.Euler(playerData.Rotation[0], playerData.Rotation[1], playerData.Rotation[2]);
            reader.Close(); // 关闭文件流，防止一直占用文件
            reader.Dispose(); // 销毁文件流对象，释放内存
        }

        // 读取CSV，设置主角的移动速度
        string pathCSV = Application.dataPath + "/Resources/PlayerPropertyData.csv";
        string csvString = File.ReadAllText(pathCSV); // 读取CSV文件
        string separateSign = ",";
        string lineFeedSign = "\r\n";

        string[] csvRowDatas = csvString.Split(lineFeedSign);
        
        for(int i = 1; i < csvRowDatas.Length; i++) // 遍历每一行数据
        {
            // 速度储存在第三列
            string[] csvRowData = csvRowDatas[i].Split(separateSign);
            speedList.Add(float.Parse(csvRowData[2]));
        }

    }

    void Update()
    {
        // 根据玩家等级调整移动速度
        for(int i = 0; i < speedList.Count; i++)
        {
            if(HeadSculpture.playerLevel.text == i.ToString())
            {
                moveSpeed = speedList[i]; // 修改移动速度
            }
        }
        // 输入
        float horizontal = Input.GetAxis("Horizontal"); // 返回1或-1
        float vertical = Input.GetAxis("Vertical");

        // 角色的移动和旋转
        transform.Translate(new Vector3(0, 0, vertical* moveSpeed) * Time.deltaTime);
        transform.Rotate(new Vector3(0, horizontal * rotateSpeed, 0) * Time.deltaTime);

        // 角色跑动起来时，切换跑步动画Running
        Vector3 move = new Vector3(0, 0, vertical);
        PlayerAnim.SetFloat("Speed", move.magnitude);

        // 发射射线
        Vector3 offSet = new Vector3(0, 1, 0); // 偏移量
        Ray ray = new Ray();
        ray.origin = transform.position + offSet;   //射线起点
        ray.direction = transform.forward; //射线方向
        isOpenDoor = Physics.Raycast(ray, out hitInfo, raycastDistance, DoorLayer);
        if (isOpenDoor) // 开门动画
        {
            PlayerAnim.Play("Open_Door_Outwards");
        }
        // 描绘出发射的射线
        //Debug.DrawRay(transform.position + offSet,
        //transform.forward * raycastDistance, Color.green);
    }

    private void OnTriggerEnter(Collider other) // 碰到这玩家等级+1
    {
        if(other.tag == "Upgrade")
        {
            int level = int.Parse(HeadSculpture.playerLevel.text) + 1;
            if(level < 10) // 最高9级
            {
                HeadSculpture.playerLevel.text = level.ToString();
            }  
        }
    }
    

    // 记录玩家退出游戏时的信息
    public class PlayerData {
        public float[] Position; // 存储主角的位置信息
        public float[] Rotation; // 存储主角的旋转信息
        public string PlayerName; // 储存游戏名称
        public string PlayerLevel; // 储存主角等级
    }

    // 每次退出游戏，都会保存一次信息，并且是覆盖式的保存
    private void OnDestroy()
    {
        // 主角的位置信息
        PlayerData playerData = new PlayerData();
        float[] position = new float[] { transform.position.x, transform.position.y, transform.position.z };
        playerData.Position = position; // 记录位置信息

        // Unity编辑器面板上显示的旋转值以欧拉角的形式呈现，通常为三个轴（X、Y、Z）上的角度值。
        // transform.rotation返回一个四元数，它是一种更复杂但更有效地表示旋转的方式。
        Vector3 eulerRotation = transform.rotation.eulerAngles; // 将四元数转换成欧拉角
        float[] rotation = new float[] { eulerRotation.x, eulerRotation.y, eulerRotation.z };
        playerData.Rotation = rotation; // 记录旋转信息

        // 储存文本信息
        playerData.PlayerName = HeadSculpture.playerName.text; // 玩家名称
        playerData.PlayerLevel = HeadSculpture.playerLevel.text;  // 玩家等级

        // 创建Json文件夹
        if (!Directory.Exists(Application.dataPath + "/Json"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Json");
        }
        // 写入数据
        // 序列化，把数据对象转化成Json文本
        string json = JsonConvert.SerializeObject(playerData);
        string path = Application.dataPath+ "/Json/Player.json"; // 文件位置
        
        // 创建一个文本文件，并返回一个写入的文件流对象
        StreamWriter writer = File.CreateText(path); // 创建Player.json文本
        writer.Write(json);  // 使文件流对象将Json格式的文本写入文件中
        writer.Close(); // 关闭文件流，防止一直占用文件
        writer.Dispose(); // 销毁文件流对象，释放内存

        //Dictionary<string, object> jsonData = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        ////Dictionary<string, object> newScore = new Dictionary<string, object>();
        //jsonData.Add("score", "75"); // 添加数据
        //jsonData["score"] = "hhh"; // 修改数据
        //string updatedJson = JsonConvert.SerializeObject(jsonData);
        //File.WriteAllText(pathJson, updatedJson);
    }
}
