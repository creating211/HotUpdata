using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMap : MonoBehaviour
{
    private Transform player; // 玩家Player
    private Vector3 offSet; // 可以增加一个偏移量
    void Start()
    {
        player = GameObject.Find("Ely").GetComponent<Transform>();
    }

    void Update()
    {
        // 更改位置和旋转
        transform.position = player.position + offSet;
        transform.eulerAngles = new Vector3(0, player.eulerAngles.y, 0);
    }
}
