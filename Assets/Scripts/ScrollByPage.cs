using System.Collections;
using System.Collections.Generic;
using UnityEngine;    // 通过鼠标滚动页数
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollByPage : MonoBehaviour,IEndDragHandler
{                                         // 调用接口,结束拖拽时的信息

    private ScrollRect scrollRect;  // 获取ScrollRect组件

    // 总共4页，每一页的位置，单位化
    private float[] PagePotision = new float[4] { 0, 0.3333f, 0.6666f, 1 };
    private float TargetPotision = 0;  // 目标位置
    private bool IsMove = false; // 是否滑动, 不滑动
    private float ScollSpeed = 7; // 滑动速度

    private Toggle[] PageToggles; // 保存所有的Toggle

    void Start()
    {
        // 创建一个Scoll View，将脚本挂在上面
        scrollRect = GetComponent<ScrollRect>(); // 获取组件，脚本挂在

        // 获取选择页面的所有Toggle组件
        PageToggles = new Toggle[4];
        for(int i = 0; i < 4; i++)
        {
            string name = "PageToggle" + (i + 1).ToString(); // 4个Toggle名字为PageToggle1 2 3 4
            PageToggles[i] = GameObject.Find(name).GetComponent<Toggle>(); // 获取Toggle组件
        }

        // 添加监听事件，点击Toggle移动到相应的页面
        PageToggles[0].onValueChanged.AddListener((isOn) => MoveToPage(isOn, 0)); // 第一页
        PageToggles[1].onValueChanged.AddListener((isOn) => MoveToPage(isOn, 1)); // 第二页
        PageToggles[2].onValueChanged.AddListener((isOn) => MoveToPage(isOn, 2)); // 第三页
        PageToggles[3].onValueChanged.AddListener((isOn) => MoveToPage(isOn, 3)); // 第四页
    }

    void Update()
    {
        if (IsMove) // isMove为真时，滑动
        {
            // 滑动动画，Mathf.Lerp的作用，将scrollRect.horizontalNormalizedPosition 
            // 与 TargetPotision 的长度，分成speed * Time.deltaTime 份
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(scrollRect.horizontalNormalizedPosition, 
                                             TargetPotision, ScollSpeed * Time.deltaTime);
            
            // 到达滑动目的地，停止滑动
            if(Mathf.Abs(scrollRect.horizontalNormalizedPosition - TargetPotision) < 0.001f)
            {
                IsMove = false; // 改为不滑动
                scrollRect.horizontalNormalizedPosition = TargetPotision; // 将位置改为对应页面位置
            }
        }
    }

    // 获取结束拖拽时的信息，并比较距离哪一页最近，滚动到最近的一页
    public void OnEndDrag(PointerEventData eventData)
    {
        // 将内容的水平方向的长度Width单位化
        float curPosition = scrollRect.horizontalNormalizedPosition;

        // 假设离第一页最近
        int index = 0; // 记录下标

        // 当前位置与第一页位置的距离
        float offset = curPosition - PagePotision[0]; 

        // 遍历每一页与当前位置的距离，记录离当前位置最近的页面
        for (int i = 1; i < 4; i++) // 从第二页开始
        {
            // 有更近的，则记录该页的信息以及最近的距离
            if (Mathf.Abs(curPosition - PagePotision[i]) < offset)
            {
                index = i; // 记录下标
                offset = Mathf.Abs(curPosition - PagePotision[i]); // 记录最近的记录
            }
        }

        // 将最近的距离赋值给目标为
        TargetPotision = PagePotision[index]; 
        IsMove = true; // 开始移动
        PageToggles[index].isOn = true; // 选中该toggle
    }

    // 通过点击Toggle，移动到指定的页数
    private void MoveToPage(bool isOn, int index)
    {
        if (isOn)
        {
            IsMove = true; // 移动
            TargetPotision = PagePotision[index]; // 移动到指定页面
        }
    }
}
