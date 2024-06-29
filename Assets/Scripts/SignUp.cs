using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 声明一个用户类，用来保存用户对应的信息
public class UserInfos
{
    public string UserName; // 用户名称
    public string Account; // 账号
    public string Password; // 密码
}

public class SignUp : MonoBehaviour
{
    private Text TipsSignUp; // 注册提示
    private InputField InputUserName;  // 输入游戏名
    private InputField InputAccount; // 用户名
    private InputField InputPassword; // 输入密码

    private Button BtnSignUp; // 登入按钮
    private Button BtnReturnLogIn; // 返回登入界面按钮

    private UserInfos userInfo; // 用户信息
    private List<UserInfos> ListUser = new List<UserInfos>(); // 用户类列表

    void Start()
    {
        // 先加载文件中已存在的用户
        AddLoadUserInfos(); 

        // 获取Input组件
        InputUserName = GameObject.Find("InputUserName").GetComponent<InputField>();
        InputAccount = GameObject.Find("InputAccount").GetComponent<InputField>();
        InputPassword = GameObject.Find("InputPassword").GetComponent<InputField>();
        
        // 注册按钮
        BtnSignUp = GameObject.Find("BtnSignUp").GetComponent<Button>();
        BtnSignUp.onClick.AddListener(ClickSignUp);

        // 返回登入按钮
        BtnReturnLogIn = GameObject.Find("BtnReturnSignIn").GetComponent<Button>();
        BtnReturnLogIn.onClick.AddListener(ClickReturnBtn);

        // 注册提示
        TipsSignUp = GameObject.Find("SignUpTips").GetComponent<Text>();
    }

    // 注册
    void ClickSignUp()
    {
        // 新建一个用户信息，将input中输入的信息，保存到用户类中
        userInfo = new UserInfos();
        userInfo.UserName = InputUserName.text;
        userInfo.Account = InputAccount.text;
        userInfo.Password = InputPassword.text;

        // 将该用户的信息添加到用户类列表当中
        ListUser.Add(userInfo);

        // 保存用户信息
        SaveUserInfos();
    }

    // 保存用户信息
    void SaveUserInfos()
    {
        // 创建Json文件夹
        if (!Directory.Exists(Application.dataPath + "/Json"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Json");
        }

        // 文件保存的路径
        string userInfosPath = Application.dataPath + "/Json/UserInfos.Json";

        // 将 用户类列表 序列化成 字符串
        string userInfosString = JsonConvert.SerializeObject(ListUser);

        // 判断文件是否存在
        if (Directory.Exists(userInfosPath))
        {
            Directory.CreateDirectory(userInfosPath);
        }
        
        // 将内容写入文件当中
        File.WriteAllText(userInfosPath, userInfosString);
        Debug.Log($"UserInfos的打包路径为：{userInfosPath}");

        // 显示注册成功的提示
        StartCoroutine(DisplayText());
    }

    // 将文件中已有的信息添加到用户类列表ListUser当中
    void AddLoadUserInfos()
    {
        // UserInfos.Json 文件路径
        string userInfosPath = Application.dataPath + "/Json/UserInfos.Json";
        
        // File.ReadAllText 读出来的内容是字符串形式
        string userInfosString = File.ReadAllText(userInfosPath);
        
        // 将字符串反序列化为：List<UserInfos>类型
        List<UserInfos> listUser = JsonConvert.DeserializeObject<List<UserInfos>>(userInfosString);

        // 将文件中已存在的用户添加的用户列表ListUser中
        foreach(UserInfos userInfo in listUser)
        {
            ListUser.Add(userInfo); // 添加到用户列表当中
        }
    }

    // 点击返回主场景
    void ClickReturnBtn() 
    {
        SceneManager.LoadScene("LogIn");   // 加载登入场景
    }

    // 注册成功的提示，显示2秒后消失
    IEnumerator DisplayText()
    {
        TipsSignUp.text = "账号创建成功";
        yield return new WaitForSeconds(2.0f); // 文字显示2秒
        TipsSignUp.text = null;
    }
}
