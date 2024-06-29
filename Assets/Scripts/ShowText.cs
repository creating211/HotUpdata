using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{
    public GameObject textObj; // 必须在unity赋值，因为用脚本查找不到隐藏的物体
    public string description;
    // Start is called before the first frame update
    void Start()
    {
        Text showText = textObj.GetComponent<Text>();
        showText.text = description; 
    }

    IEnumerator DisplayText()
    {
        textObj.SetActive(true);  // 物体用SetActive，组件用enable
        yield return new WaitForSeconds(3.0f); // 文字显示2秒
        textObj.SetActive(false); // 隐藏物体
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(DisplayText());
    }
}
