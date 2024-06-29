using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    private Character PlayerCS; // Player脚本
    private Animator DoorAnim; // 右门的动画控制器组件Animator
    public string Name; // 门的名字
    public bool isLeftDoor = true; // 是左门还是右门
    private float distance; // 主角与门Z轴的距离

    void Start()
    {
        PlayerCS = GameObject.Find("Ely").GetComponent<Character>(); // 获取脚本
        DoorAnim = transform.GetComponent<Animator>(); // 获取Animator组件
    }

    void Update()
    {
        // 判断主角与门的位置，从外往里走，还是从里往外走
        distance = PlayerCS.transform.position.z - transform.position.z;
        
        // 左门，右门播放不同的动画
        if (PlayerCS.isOpenDoor && PlayerCS.hitInfo.collider.name == Name && isLeftDoor && distance < 0)
        {
            DoorAnim.Play("OpenLeftDoor");
            StartCoroutine(CloseDoor());
        }

        if (PlayerCS.isOpenDoor && PlayerCS.hitInfo.collider.name == Name  && !isLeftDoor && distance < 0)
        {
            DoorAnim.Play("OpenRightDoor");
            StartCoroutine(CloseDoor());
        }

        if (PlayerCS.isOpenDoor && PlayerCS.hitInfo.collider.name == Name && isLeftDoor && distance > 0)
        {
            DoorAnim.Play("InOpenLeftDoor");
            StartCoroutine(CloseDoor());
        }

        if (PlayerCS.isOpenDoor && PlayerCS.hitInfo.collider.name == Name && !isLeftDoor && distance > 0)
        {
            DoorAnim.Play("InOpenRightDoor");
            StartCoroutine(CloseDoor());
        }
    }

    IEnumerator CloseDoor()
    {
        // 等待3s
        yield return new WaitForSeconds(5);
        if(distance < 0) // 从外往里走
        {
            if (isLeftDoor) // 左门
            {
                DoorAnim.Play("CloseLeftDoor");
            }
            else // 右门
            {
                DoorAnim.Play("CloseRightDoor");
            }
        }
        else // 从里往外走
        {
            if (isLeftDoor) // 左门
            {
                DoorAnim.Play("InCloseLeftDoor");
            }
            else // 右门
            {
                DoorAnim.Play("InCloseRightDoor");
            }
        }
    }
}
