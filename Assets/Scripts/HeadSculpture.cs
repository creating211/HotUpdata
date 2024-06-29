using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class HeadSculpture : MonoBehaviour
{
    // 静态变量，可以在其他脚本中访问
    public static Text playerName;  // 游戏名称
    public static Text playerLevel; // 主角等级
    private Image headSculpture; // 头像图片
    
    
    void Start()
    {
        playerName = GameObject.Find("Name").GetComponent<Text>(); // 获取Text组件
        playerName.horizontalOverflow = HorizontalWrapMode.Overflow; // 横向允许超出文本框
        playerName.text = Login.Instance.HeadSculptureUserName; // 将登入界面的名字复制到游戏界面 

        playerLevel = GameObject.Find("Level").GetComponent<Text>(); // 获取Text组件
        playerLevel.text = "0";  // 初始等级为0
        string path = Application.dataPath + "/Json/Player.json";
        if (File.Exists(path))
        {
            StreamReader reader = File.OpenText(path);
            string json = reader.ReadToEnd();
            Player.PlayerData playerData = JsonConvert.DeserializeObject<Player.PlayerData>(json);
            if(playerName.text == null)
            {
                playerName.text = playerData.PlayerName;  // 将储存的玩家名称赋值给文本
                playerLevel.text = playerData.PlayerLevel;// 将储存的玩家等级赋值给文本
            }   
        }

        // 获取组件
        headSculpture = GameObject.Find("head_img").GetComponent<Image>();
        string index = Random.Range(1, 6).ToString(); // 随机产生一个数
        
        // 异步加载资源。它允许您在后台加载资源而不会阻塞主线程执行。
        // 在Resources 文件夹中，图片为Sprite，图片名称为1,2,3,4,5
        ResourceRequest resourceRequest = Resources.LoadAsync<Sprite>(index);
        headSculpture.sprite = resourceRequest.asset as Sprite; // 赋值
    }
}
