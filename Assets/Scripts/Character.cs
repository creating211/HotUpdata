using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private float MoveSpeed = 1.8f; // 移动速度
    private float RunSpeed = 5.0f;  // 跑步速度
    private Animator ErikaAnimator; // 动画控制器
    private bool isRuning; // 是否跑
    private float currentSpeed; // 当前速度
    public bool isOpenDoor; // 是否开门
    private LayerMask DoorLayer;  // 层级，outSide
    private float raycastDistance = 1f;  // 射线发射的距离
    public RaycastHit hitInfo; // 定义一个RaycastHit变量用来保存被撞物体的信息


    void Start()
    {
        DoorLayer = LayerMask.GetMask("Door"); // 将门的层级改为Door
        ErikaAnimator = transform.GetComponent<Animator>(); // 获取Animator组件
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 角色的旋转
        //transform.Rotate(new Vector3(0, horizontal * RotateSpeed, 0) * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftShift)) // 按下Shift键
        {
            isRuning = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift)) // 抬起
        {
            isRuning = false;
        }

        // 走路、跑步
        if (!isRuning)
        {
            transform.Translate(new Vector3(horizontal, 0, vertical * MoveSpeed) * Time.deltaTime);
            currentSpeed = new Vector3(horizontal, 0, vertical).magnitude * MoveSpeed;
        }
        else
        {
            transform.Translate(new Vector3(horizontal, 0, vertical * RunSpeed) * Time.deltaTime);
            currentSpeed = new Vector3(horizontal, 0, vertical).magnitude * RunSpeed;
        }

        // 角色移动时，开启移动动画
        Vector3 move = new Vector3(0, 0, vertical);
        ErikaAnimator.SetFloat("Speed", currentSpeed);

        // 跳跃
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ErikaAnimator.SetTrigger("IsJump");
        }

        // 发射射线
        Vector3 offSet = new Vector3(0, 1, 0); // 偏移量
        Ray ray = new Ray();
        ray.origin = transform.position + offSet;   //射线起点
        ray.direction = transform.forward; //射线方向
        isOpenDoor = Physics.Raycast(ray, out hitInfo, raycastDistance, DoorLayer);
        if (isOpenDoor) // 开门动画
        {
            ErikaAnimator.Play("OpenDoor");
        }
    }
}
