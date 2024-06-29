using UnityEditor;
using UnityEngine;

// 只管理自身渲染部分的代码
public class AssetManagerEditorWindow : EditorWindow
{
    public string VersionString;

    public AssetManagerEditorWindowConfig WindowConfig;

    // Scriptable 本身的值是序列化的，但是在Editor类中声明的ScriptableObject变量是会
    // 随着编译而变成默认值(null)的
    public void Awake()
    {
        AssetManagerEditor.LoadAssetManagerConfig(this);
        AssetManagerEditor.LoadAssetManagerWindowConfig(this);
    }

    // 每当改变时，调用该方法
    private void OnValidate()
    {
        AssetManagerEditor.LoadAssetManagerConfig(this);
        AssetManagerEditor.LoadAssetManagerWindowConfig(this);

    }

    private void OnInspectorUpdate()
    {        
        AssetManagerEditor.LoadAssetManagerConfig(this);
        AssetManagerEditor.LoadAssetManagerWindowConfig(this);
    }


    // 所有的界面渲染都需要通过代码在这个方法中手动指定渲染方式，
    // 这也是早期Unity编写UI的方式，ONGUI方法会在每个渲染帧被调用，
    // 在Editor环境下也是如此，只不过换成Editor自身的渲染帧而非Game视窗的
    private void OnGUI()
    {
        // 默认情况下是垂直排版，GUI按照代码顺序进行渲染
        #region 标题图
        GUILayout.Space(20);// 固定尺寸的空白区域
        if(WindowConfig.LogoTexture != null)
        {
            GUILayout.Label(WindowConfig.LogoTexture, WindowConfig.LogoTextureStyle);
        }
        
        #endregion

        #region 标题
        GUILayout.Space(20); // GUILayout.Label()会将内容和样式显示在窗口中
        GUILayout.Label(nameof(AssetManagerEditor), WindowConfig.TitleTextStyle); // 自带排版功能的GUI方法
        #endregion

        #region 版本号
        GUILayout.Space(20);
        GUILayout.Label(VersionString, WindowConfig.VersionTextStyle);
        #endregion

        #region 打包模式选择
        GUILayout.Space(20);
        // 在窗口中创建一个打包模式的选择
        AssetManagerEditor.AssetManagerConfig.BuildingPattern = (AssetBundlePattern)EditorGUILayout.EnumPopup("打包模式",
            AssetManagerEditor.AssetManagerConfig.BuildingPattern);
        #endregion

        #region 压缩模式选择
        GUILayout.Space(20);
        AssetManagerEditor.AssetManagerConfig.CompressionPattern = (AssetBundleCompressionPattern)EditorGUILayout.EnumPopup("压缩格式",
            AssetManagerEditor.AssetManagerConfig.CompressionPattern);
        #endregion

        #region 增量打包选择
        GUILayout.Space(20);
        AssetManagerEditor.AssetManagerConfig.BuildMode = (IncrementalBuildMode)EditorGUILayout.EnumPopup("增量打包模式",
            AssetManagerEditor.AssetManagerConfig.BuildMode);
        #endregion

        #region 打包资源选择
        GUILayout.BeginVertical("frameBox");
        GUILayout.Space(10);
        for(int i=0;i< AssetManagerEditor.AssetManagerConfig.packageInfoEditors.Count; i++)
        {
            PackageEditorInfo packageInfo = AssetManagerEditor.AssetManagerConfig.packageInfoEditors[i];
            GUILayout.BeginVertical("frameBox");
            GUILayout.BeginHorizontal();
            packageInfo.PackageName = EditorGUILayout.TextField("PackageName", packageInfo.PackageName);

            if (GUILayout.Button("Remove"))
            {
                AssetManagerEditor.RemovePackageInfoEditors(packageInfo);
            }

            GUILayout.EndHorizontal();
            GUILayout.Space(10);
            for (int j = 0; j < packageInfo.AssetList.Count; j++)
            {
                GUILayout.BeginHorizontal();
                packageInfo.AssetList[j] = EditorGUILayout.ObjectField(packageInfo.AssetList[j], typeof(GameObject)) as GameObject;

                if (GUILayout.Button("Remove"))
                {
                    AssetManagerEditor.RemoveAsset(packageInfo,packageInfo.AssetList[j]);
                }
                GUILayout.EndHorizontal();
                GUILayout.Space(5);
            }
            if (GUILayout.Button("新增Asset"))
            {
                AssetManagerEditor.AddAsset(packageInfo);
            }
            GUILayout.EndVertical();
            GUILayout.Space(10);
        }
        GUILayout.Space(10);
        if (GUILayout.Button("新增Package"))
        {
            AssetManagerEditor.AddPackageInfoEditor();
        }
        GUILayout.EndVertical();
        #endregion

        #region 打包按钮
        GUILayout.Space(20);

        // 在窗口中创建一个Button，显示文字为：打包AssetBundle
        if (GUILayout.Button("打包AssetBundle"))
        {

            AssetManagerEditor.BuildAssetBundleFromDirectedGraph();
        }
        #endregion

        #region 保存、读取Config
        GUILayout.Space(10);
        if (GUILayout.Button("保存Config文件"))
        {
            AssetManagerEditor.SaveConfigToJSON();
        }

        GUILayout.Space(10);
        if (GUILayout.Button("读取Config文件"))
        {
            AssetManagerEditor.ReadConfigFromJSON();
        }
        #endregion   
    }
}
