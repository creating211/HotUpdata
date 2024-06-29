using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;



// fileName = 点击按钮之后创建的默认文件名，menuName = 在Create菜单中的菜单层级
// order = 规定了创建 ScriptableObject 数据资源文件的路径在菜单上显示的位置，order 越低，就显示在与上面
// 如果 order 相同，则是按照继承自 ScriptableObject 的脚本创建时间排序，新创建的排在上面

// 右键，创建ScriptableObject(可编程对象)
[CreateAssetMenu(fileName ="AssetManagerConfig",menuName ="AssetManager/AssetManagerConfig",order =1)]
public class AssetManagerConfigScriptableObject : ScriptableObject
{
   // 打包模式选择
    public AssetBundlePattern BuildingPattern;

    // 打包压缩模式选择
    public AssetBundleCompressionPattern CompressionPattern;

    // 打包模式选择，是否应用增量打包
    public IncrementalBuildMode BuildMode;

    // 之所以使用整数是因为方便递增
    public int AssetManagerVersion = 100; // 资源管理器工具的版本

    public int CurrentBuildVersion = 100; // AssetBundle包打包版本

    public string[] InvalidExtentionName = new string[] { ".meta", ".cs" };

    public List<PackageEditorInfo> packageInfoEditors = new List<PackageEditorInfo>();

}


