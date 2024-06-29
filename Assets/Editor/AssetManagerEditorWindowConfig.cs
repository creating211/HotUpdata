using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "AssetManagerWindowConfig", menuName = "AssetManager/AssetManagerWindowConfig", order = 1)]
public class AssetManagerEditorWindowConfig : ScriptableObject
{
    public GUIStyle TitleTextStyle; // 标题文字样式
    public GUIStyle VersionTextStyle; // 版本号样式

    public Texture2D LogoTexture; // Logo图片
    public GUIStyle LogoTextureStyle; // Logo样式
}

