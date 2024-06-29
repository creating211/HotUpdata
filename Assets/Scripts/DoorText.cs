using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DoorText : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private Text text;
    void Start()
    {
        text = transform.Find("DoorText").GetComponent<Text>();
    }

    // 当把Canvas的Render Mode 改为 World Space时，z轴必须朝向屏幕，即朝向屏幕外
    public void OnPointerEnter(PointerEventData eventData)
    {
        text.enabled = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        text.enabled = false;
    }
}
