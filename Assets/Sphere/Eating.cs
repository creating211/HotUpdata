using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eating : MonoBehaviour
{
    public Text TipsChara;
    private int HungerValue = 60;

    void Start()
    {

        StartCoroutine(EnegerIndece());  // 调用倒计时

    } 
    IEnumerator EnegerIndece() // 计时器
    {

        while (HungerValue > 0)
        {
            HungerValue--;
            yield return new WaitForSeconds(1f);
        }

        if(HungerValue == 0)
        {
            TipsChara.text = "太饿了，快去吃饭！";
        }
    }

    IEnumerator EnegerAdd()
    {
        if (HungerValue >= 60)
        {
            HungerValue = 60;
            TipsChara.text = "吃饱了！";
            yield return new WaitForSeconds(1f);
            TipsChara.text = null;

            StartCoroutine(EnegerIndece()); // 调用倒计时
        }
    }

    public void ClickEatFood()
    {
        HungerValue += 30;
        StartCoroutine(EnegerAdd()); 
    }  
}
