using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;  
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public string HeadSculptureUserName; // 用户名
    private InputField AccountInput; // 账号
    private InputField PasswordInput; // 输入密码
    private Button LoginBtn; // 登入按钮
    private Text LogInTips; // 提示文本

    // 使其他脚本可以调用Login脚本中的变量
    private static Login _instance;
    public static Login Instance
    {
        get{ return _instance;}
    }

    private void Awake()  // 初始化
    {
        _instance = this;
    }

    void Start()
    {
        // 初始化
        AccountInput = GameObject.Find("InputAccount").GetComponent<InputField>();
        PasswordInput = GameObject.Find("InputPassword").GetComponent<InputField>();
        PasswordInput.contentType = InputField.ContentType.Password; // 隐藏密码
        
        LogInTips = GameObject.Find("SignUpTips").GetComponent<Text>();

        // 登入按钮
        LoginBtn = GameObject.Find("LoginBtn").GetComponent<Button>();
        LoginBtn.onClick.AddListener(ClickLogIn); // 绑定监听事件

        // 注册按钮
        LoginBtn = GameObject.Find("SignUpBtn").GetComponent<Button>();
        LoginBtn.onClick.AddListener(ClickSignUp); // 绑定监听事件
    }

    // 登入
    void ClickLogIn()
    {
        List<UserInfos> listUser = LoadUserInfos(); // 先加载文件中已经存在的用户
        bool userNoExist = true; // 记录用户是否存在
        bool passwordError = false; // 记录密码是否正确

        
        // 判断用户名、密码是否正确
        foreach (UserInfos userInfos in listUser)
        {
            // 文件中的账号等于输入的账号，则账号正确
            if (userInfos.Account == AccountInput.text) // 账号正确
            {
                userNoExist = false; // 账号正确，则说明用户存在

                if (userInfos.Password == PasswordInput.text) // 密码正确
                {
                    HeadSculptureUserName = userInfos.UserName; // 给游戏场景中的头像名称赋值
                    StartCoroutine(LoadMainScene()); // 加载游戏界面
                }
                else // 密码错误
                {
                    passwordError = true;
                } 
            }
        }

        // 用户不存在，输出对应提示
        if (userNoExist)
        {
            StartCoroutine(TipsUserNoExist());
        }

        // 密码错误，输出对应提示
        if (passwordError)
        {
            StartCoroutine(TipsPasswordError());
        }
    }  

    // 读取文件当中的用户信息，并返回
    List<UserInfos> LoadUserInfos()
    {
        
        // UserInfos.Json 文件路径
        string UserInfosPath = Application.dataPath + "/Json/UserInfos.Json";

        // File.ReadAllText 读出来的内容是字符串形式
        string UserInfosString = File.ReadAllText(UserInfosPath);

        // 将字符串反序列化为：List<UserInfos>类型
        List<UserInfos> listUser = JsonConvert.DeserializeObject<List<UserInfos>>(UserInfosString);

        return listUser;
    }

    // 点击注册，跳转注册场景
    void ClickSignUp()
    {
        SceneManager.LoadScene("SignUp"); // 加载登入界面
    }

    // 登入成功加载主场景
    IEnumerator LoadMainScene()
    {
        LogInTips.text = "登入成功！";
        yield return new WaitForSeconds(0.1f); // 文字显示1秒后消失
        SceneManager.LoadScene("SelectCharacter"); // 加载游戏场景
    }

    // 密码错误提示
    IEnumerator TipsPasswordError()
    {
        LogInTips.text = "密码输入错误！";
        yield return new WaitForSeconds(2f); // 文字显示2秒后消失
        LogInTips.text = null;
    }

    // 用户不存在提示
    IEnumerator TipsUserNoExist()
    {
        LogInTips.text = "用户不存在！";
        yield return new WaitForSeconds(2f); // 文字显示2秒后消失
        LogInTips.text = null;
    }
}
